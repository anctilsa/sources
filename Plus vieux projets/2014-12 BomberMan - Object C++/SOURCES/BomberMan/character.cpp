/***********************************************************************************************************\
|File	: character.cpp																						|
|Autor	: Samuel Anctil																						|
|Date	: 2014-11-27																						|
|Goal	: Ressource for the class character.																|
\***********************************************************************************************************/

// Preprocessor directives
#include "character.h"

// Builder
character::character()
{
	_posX = _posY = _dir = _dep = 0;
	_speed = _nbLife = 1;

	_move = false;
	_alive = true;
}

// Builder using a file name
character::character(char *fileName, int nbCol, int nbRow)
{
	init(fileName, nbCol, nbRow);
}

// Builder using a file name & rect
character::character(char *fileName, int nbCol, int nbRow, const IntRect &rect)
{
	init(fileName, nbCol, nbRow, rect);
}

// Builder using a texture
character::character(Texture &texture, int nbCol, int nbRow)
{
	init(texture, nbCol, nbRow);
}

// Builder using a texture & rect
character::character(Texture &texture, int nbCol, int nbRow, const IntRect &rect)
{
	init(texture, nbCol, nbRow, rect);
}

// Init the character using a file name
void character::init(char *fileName, int nbCol, int nbRow)
{
	_gif.init(fileName, nbCol, nbRow);
}

// Init using a file name & rect
void character::init(char *fileName, int nbCol, int nbRow, const IntRect &rect)
{
	_gif.init(fileName, rect, nbCol, nbRow);
}

// Init using a texture
void character::init(Texture &texture, int nbCol, int nbRow)
{
	_gif.init(texture, nbCol, nbRow);
}

// Init using a texture & rect
void character::init(Texture &texture, int nbCol, int nbRow, const IntRect &rect)
{
	_gif.init(texture, rect, nbCol, nbRow);
}

// Return the position x
int character::posX() const
{
	return _posX;
}

// Return the position y
int character::posY() const
{
	return _posY;
}

// Return the width of the character
int character::width() const
{
	return _gif.getWidth();
}

// Return the heigth of the character
int character::height() const
{
	return _gif.getHeight();
}

// Return the direction
int character::dir() const
{
	return _dir;
}

// Return the statut
bool character::alive() const
{
	return _alive;
}

// Return the rect of the window contening the charact
IntRect character::getRect() const
{
	return _gif.getRectImage();
}

// Set the position
void character::setPosition(int posX, int posY)
{
	assert(posX >= 0 && posY >= 0);
	
	_posX = posX;
	_posY = posY;

	_gif.setPosition(_posX, _posY);
}

// Set the direction
void character::setDirection(int dir)
{
	if (alive())								// If the character is alive
	{
		assert(dir == 0 || dir == 1 || dir == 2 || dir == 3);
	
		_dir = dir;

		_gif.setFrame(_dep, _dir);
	}
}

// Set the speed
void character::setSpeed(float speed)
{
	_speed = speed;
}

// Set the limit for the pos X and Y
void character::setLimit(const IntRect &limit)
{
	_limit = limit;
}

// Set move to true
void character::enableMove()
{
	if (alive())								// If the character is moving
		_move = true;
}

// Set move to false
void character::stopMove()
{
	_move = false;
}

// Return true if the character is moving
bool character::isMoving() const
{
	return _move;
}

// Change the posX and the posY depending of the direction
void character::move()
{
	if (isMoving())								// If the character is moving
	{
		switch (_dir)							// Depending of the direction
		{
			case 0:								// Up
				if (_posY > _limit.top)			// If posY is lower than the top limit
					_posY--;
				break;

			case 1:								// Right 
				if (_posX <= _limit.left + _limit.width) // If posX is lower than the right limit
					_posX++;
				break;

			case 2:								// Down
				if (_posY <= _limit.top + _limit.height) // If posY is lower than the bottom limit
					_posY++;
				break;

			case 3:								// Left
				if (_posX > _limit.left)		// If posX is upper than the left limit
					_posX--;
				break;
		}

		_dep = _gif.nextFrame();
		_gif.setPosition(_posX, _posY);
	}
}

// The character slide
void character::slide(int dir)
{
	if (isMoving())								// If the character is moving
	{
		switch (dir)
		{
		case 0:									// Up
			_posY++;
			break;

		case 1:									// Right
			_posX--;
			break;

		case 2:									// Down
			_posY--;
			break;

		case 3:									// Left
			_posX++;
			break;
		}

		if (dir == 0 || dir == 1 || dir == 2 || dir == 3)
			_dep = _gif.nextFrame();

		_gif.setPosition(_posX, _posY);
	}
}

// The character slide up
void character::slideUp()
{
	_posY++;
}

// The character slide left
void character::slideLeft()
{
	_posX--;
}

// The character slide down
void character::slideDown()
{
	_posY--;
}

// The character slide right
void character::slideRight()
{
	_posX++;
}

// Draw the character
void character::draw(RenderWindow &window)
{
	_gif.draw(window);
}