/***********************************************************************************************************\
|File	: man.cpp																							|
|Autor	: Samuel Anctil																						|
|Date	: 2014-11-27																						|
|Goal	: Ressource for the class bomberMan.																|
\***********************************************************************************************************/

// Preprocessor directives
#include "man.h"

// Builder
man::man()
{
	_nbLife = 3;
	_nbBomb = 1;
	_nbPoint = 0;
}

// Builder using a file name
man::man(char *fileName, int nbLife, int nbBomb)
{
	init(fileName, nbLife, nbBomb);
}

// Builder using a file name & rect
man::man(char *fileName, const IntRect &rect, int nbLife, int nbBomb)
{
	init(fileName, rect, nbLife, nbBomb);
}

// Builder using a texture
man::man(Texture texture, int nbLife, int nbBomb)
{
	init(texture, nbLife, nbBomb);
}

// Builder using a texture & rect
man::man(Texture texture, const IntRect &rect, int nbLife, int nbBomb)
{
	init(texture, rect, nbLife, nbBomb);
}

// Init using a file name
void man::init(char *fileName, int nbLife, int nbBomb)
{
	character::init(fileName, 3, 4);

	init(nbLife, nbBomb);
}

// Init using a file name & rect
void man::init(char *fileName, const IntRect &rect, int nbLife, int nbBomb)
{
	character::init(fileName, 3, 4, rect);

	init(nbLife, nbBomb);
}

// Init using a texture
void man::init(Texture &texture, int nbLife, int nbBomb)
{
	character::init(texture, 3, 4);

	init(nbLife, nbBomb);
}

// Init using a texture & rect
void man::init(Texture &texture, const IntRect &rect, int nbLife, int nbBomb)
{
	character::init(texture, 3, 4, rect);

	init(nbLife, nbBomb);
}

// Init the param
void man::init(int nbLife, int nbBomb)
{
	_nbBomb = nbBomb;
	_nbLife = nbLife;

	_gif.setFrame(0, 2);
	setDirection(2);
}

// Return the nb of point
int man::getNbPoint() const
{
	return _nbPoint;
}

// Return the nb of bomb
int man::getNbBomb() const
{
	return _nbBomb;
}

// Return the nb of life
int man::getNbLife() const
{
	return _nbLife;
}

// Make die bomber man
void man::hit()
{
	_alive = false;
	_dir = 0;
	_dep = 0;

	_gif.change(3, 1, IntRect(0, 320, 120, 40));
	_gif.setFrame(_dep, _dir);
	_gif.setRepeat(false);
	_gif.setInterDep(10);

	_nbLife--;
}

// Make relive bomber man
void man::relive()
{
	_alive = true;
	_dep = 0;
	_dir = 2;

	_gif.change(3, 4, IntRect(0, 160, 120, 160));
	_gif.setFrame(_dep, _dir);
	_gif.setRepeat(true);
	_gif.setInterDep(5);
}

// Return true if bomber man can place a bomb
bool man::canPlaceBomb()
{
	return _nbBomb != 0;
}

// Place a bomb
void man::placeBomb()
{
	_nbBomb--;
}

// A bomb exploded
void man::bombExploded()
{
	_nbBomb++;
}

// Change the frame depending of the statut
void man::update()
{
	move();

	if (!alive())								// Is die
	{
		_gif.nextFrame();

		if (_gif.isLastFrame())					// If we are at the end of the gif
			relive();
	}
}