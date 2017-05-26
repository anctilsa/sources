/***********************************************************************************************************\
|File	: monster.cpp																						|
|Autor	: Samuel Anctil & Mike Girard																		|
|Date	: 2014-11-27																						|
|Goal	: Ressource for the class monster.																	|
\***********************************************************************************************************/

// Preprocessor directives
#include "monster.h"

// Builder
monster::monster()
{
	_tally = _nbColision = _nbTry = _interDep = 0;
	_move = true;
}
// Builder using a file name
monster::monster(char *fileName, int tally)
{
	init(fileName, tally);
}

// Builder using a file name & rect
monster::monster(char *fileName, const IntRect &rect, int tally)
{
	init(fileName, rect, tally);
}

// Builder using a texture
monster::monster(Texture &texture, int tally)
{
	init(texture, tally);
}

// Builder using a texture & rect
monster::monster(Texture &texture, const IntRect &rect, int tally)
{
	init(texture, rect, tally);
}

// Init using a file name
void monster::init(char *fileName, int tally)
{
	character::init(fileName, 3, 4);

	init(tally);
}

// Init using a file name & rect
void monster::init(char *fileName, const IntRect &rect, int tally)
{
	character::init(fileName, 3, 4, rect);

	init(tally);
}

// Init using a texture
void monster::init(Texture &texture, int tally)
{
	character::init(texture, 3, 4);

	init(tally);
}

// Init using a texture & rect
void monster::init(Texture &texture, const IntRect &rect, int tally)
{
	character::init(texture, 3, 4, rect);

	init(tally);
}

// Init the param
void monster::init(int tally)
{
	_move = true;

	_tally = tally;
	_nbColision = _nbTry = _interDep = 0;

	_gif.setFrame(0, 2);
}

// Return the tally value
int monster::getTally() const
{
	return _tally;
}

// Change the dir randomly depending of the dir
void monster::changeDirection()
{
	_nbColision++;

	if (_nbColision == 2)					// If the nb of try is 2
	{
		switch (_dir)						// Depending of the dir
		{
		case 0:								// Up
			if (rand() % 2 == 0)
				setDirection(3);
			else
				setDirection(1);
			break;

		case 1:								// Right
			if (rand() % 2 == 0)
				setDirection(0);
			else
				setDirection(2);
			break;

		case 2:								// Down
			if (rand() % 2 == 0)
				setDirection(1);
			else
				setDirection(3);
			break;

		case 3:								// Left
			if (rand() % 2 == 0)
				setDirection(2);
			else
				setDirection(0);
			break;
		}

		_nbColision = 0;
	}
	else
	{
		switch (_dir)						// Depending of the dir
		{
		case 0:								// Up
			setDirection(2);
			break;

		case 1:								// Right
			setDirection(3);
			break;

		case 2:								// Down
			setDirection(0);
			break;

		case 3:								// Left
			setDirection(1);
			break;
		}
	}
}

// Hit
void monster::hit()
{
	_dir = 0;
	_dep = 0;

	_gif.change(3, 1, IntRect(0, 320, 120, 40));
	_gif.setFrame(_dep, _dir);
	_gif.setRepeat(false);
	_gif.setInterDep(10);

	_nbLife--;
}

// The monster is dead
bool monster::dead()
{
	return (!_alive && _gif.isLastFrame());
}

// Destroyer
monster::~monster()
{
	_tally = 0;
}