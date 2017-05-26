/***********************************************************************************************************\
|File	: bomb.cpp																							|
|Autor	: Samuel Ancil																						|
|Date	: 2014-12-09																						|
|Goal	: Ressource for the class bomb.																		|
\***********************************************************************************************************/

// Preprocessor directives
#include "bomb.h"

// Builder
bomb::bomb()
{
	init();
}

// Builder using a file name
bomb::bomb(char *fileName, int nbCol, int nbRow)
{
	init(fileName, nbCol, nbRow);
}

// Builder using a file name & rect
bomb::bomb(char *fileName, const IntRect &rect, int nbCol, int nbRow)
{
	init(fileName, rect, nbCol, nbRow);
}

// Builder using a texture
bomb::bomb(Texture &texture, int nbCol, int nbRow)
{
	init(texture, nbCol, nbRow);
}

// Builder using a texture & rect
bomb::bomb(Texture &texture, const IntRect &rect, int nbCol, int nbRow)
{
	init(texture, rect, nbCol, nbRow);
}

// Copy builder
bomb::bomb(bomb &bomb)
{
	_bomb = bomb._bomb;
	_bomb.init(_bomb.getTexture(), _bomb.getNbCol(), _bomb.getNbLn());
	_explode = bomb._explode;
	_range = bomb._range;

	for (int i = 0; i < 4; i++)					// For each side
		_sideRange[i] = bomb._sideRange[i];

	for (int i = 0; i < 3; i++)					// For each gif of the explosion
		_explosion[i] = bomb._explosion[i];

	_timeRemaining = bomb._timeRemaining;

	init();
}

// Init the character using a file name
void bomb::init(char *fileName, int nbCol, int nbRow)
{
	_bomb.init(fileName, nbCol, nbRow);

	init();
}

// Init using a file name & rect
void bomb::init(char *fileName, const IntRect &rect, int nbCol, int nbRow)
{
	_bomb.init(fileName, nbCol, nbRow);

	init();
}

// Init using a texture
void bomb::init(Texture &texture, int nbCol, int nbRow)
{
	_bomb.init(texture, nbCol, nbRow);

	init();
}

// Init using a texture & rect
void bomb::init(Texture &texture, const IntRect &rect, int nbCol, int nbRow)
{
	_bomb.init(texture, rect, nbCol, nbRow);

	init();
}

// Init the variables
void bomb::init()
{
	_explode = false;

	_timeRemaining = 30000;

	setRange(1, 1, 1, 1);
	setRange(1);
}

// Set the explosion using a file name
void bomb::setExplosion(char *fileName, int nbCol, int nbRow)
{
	for (int i = 0; i < 3; i++)					// Create the 3 gif needed for the explosion
	{
		_explosion[i].init(fileName, nbCol, nbRow);
		_explosion[i].change(nbCol, nbRow / 3, _explosion[i].getRectCol(i));
	}

	setExplosion();
}

// Set the explosion using a file name & rect
void bomb::setExplosion(char *fileName, const IntRect &rect, int nbCol, int nbRow)
{
	for (int i = 0; i < 3; i++)					// Create the 3 gif needed for the explosion
	{
		_explosion[i].init(fileName, rect, nbCol, nbRow);
		_explosion[i].change(nbCol, nbRow / 3, _explosion[i].getRectCol(i));
	}

	setExplosion();
}

// Set the explosion using a texture
void bomb::setExplosion(Texture &texture, int nbCol, int nbRow)
{
	for (int i = 0; i < 3; i++)					// Create the 3 gif needed for the explosion
	{
		_explosion[i].init(texture, nbCol, nbRow);
		_explosion[i].change(nbCol, nbRow / 3, _explosion[i].getRectCol(i));
	}

	setExplosion();
}

// Set the explosion using a texture & rect
void bomb::setExplosion(Texture &texture, const IntRect &rect, int nbCol, int nbRow)
{
	for (int i = 0; i < 3; i++)					// Create the 3 gif needed for the explosion
	{
		_explosion[i].init(texture, rect, nbCol, nbRow);
		_explosion[i].change(nbCol, nbRow / 3, _explosion[i].getRectCol(i));
	}

	setExplosion();
}

// Init the parameters of each gif
void bomb::setExplosion()
{
	for (int i = 0; i < 3; i++)					// For each part of the explosion
		_explosion[i].setRepeat(false);

	initEnds();
	updateSides();

	_center = _explosion[2];
	_center.setRepeat(false);
}

// Init the ends
void bomb::initEnds()
{
	for (int i = 0; i < 4; i++)					// To init the ends
	{
		_end[i] = _explosion[0];
		_end[i].setRotation(i * 90);
		_end[i].setRepeat(false);
	}
}

// Alocate the sides
void bomb::updateSides()
{
	for (int i = 0; i < 4; i++)					// For each side
	{
		animatedGif side = _explosion[1];

		_side[i] = new animatedGif[_sideRange[i]];

		for (int j = 0; j < _range && j < _sideRange[i]; j++)	// For each range depth
		{
			_side[i][j] = side;

			if (i == 1 || i == 3)				// If right or left side
				_side[i][j].setRotation(270);
			
			_side[i][j].setRepeat(false);
		}
	}
}

// Set the range
void bomb::setRange(int range)
{
	_range = range;

	updateSides();

	setPositions();
}

// Set the range limit of each sides (for obstacles)
void bomb::setRange(int topRange, int rightRange, int bottomRange, int leftRange)
{
	_sideRange[0] = topRange;
	_sideRange[1] = rightRange;
	_sideRange[2] = bottomRange;
	_sideRange[3] = leftRange;

	updateSides();

	setPositions();
}

// Set all the positions
void  bomb::setPositions()
{
	for (int i = 0; i < 4; i++)					// For each side
	{
		for (int j = 0; j < _sideRange[i]; j++)	// For each range depth
		{
			if (_sideRange[i] > 1)				// Must be more than 1
				setPositionSide(i, j);
		}

		setPositionEnd(i);
	}
}

// Set a end depending of his nb and of the depth range
void bomb::setPositionEnd(int sideNb)
{
	IntRect rect = _center.getRectImage();

	switch (sideNb)								// Depending of the side number
	{
	case 0:										// Up
		_end[sideNb].setPosition(rect.left, rect.top - (rect.height * _sideRange[sideNb]));
		break;

	case 1:										// Right
		_end[sideNb].setPosition(rect.left + (rect.width * (_sideRange[sideNb] + 1)), rect.top);
		break;

	case 2:										// Down
		_end[sideNb].setPosition(rect.left + rect.width, rect.top + (rect.height * (_sideRange[sideNb] + 1)));
		break;

	case 3:										// Left
		_end[sideNb].setPosition(rect.left - (rect.width * _sideRange[sideNb]), rect.top + rect.height);
		break;
	}
}

// Set a side depending of his nb and of the depth range
void bomb::setPositionSide(int sideNb, int rangeNb)
{
	IntRect rect = _center.getRectImage();

	switch (sideNb)								// Depending of the side number
	{
		case 0:									// Up
			_side[sideNb][rangeNb].setPosition(rect.left, rect.top - (rect.height * rangeNb));
			break;

		case 1:									// Right
			_side[sideNb][rangeNb].setPosition(rect.left + (rect.width * rangeNb), rect.top + rect.height);
			break;

		case 2:									// Down
			_side[sideNb][rangeNb].setPosition(rect.left, rect.top + (rect.height * rangeNb));
			break;

		case 3:									// Left
			_side[sideNb][rangeNb].setPosition(rect.left - (rect.width * rangeNb), rect.top + rect.height);
			break;
	}
}

// Set the position
void bomb::setPosition(int posX, int posY)
{
	_center.setPosition(posX, posY);
	_bomb.setPosition(posX, posY);

	setPositions();
}

// Return the pos x
int bomb::getPosX() const
{
	return _bomb.getPosX();
}

// Return the pos y
int bomb::getPosY() const
{
	return _bomb.getPosY();
}

// Get the width
int bomb::getWidth() const
{
	return _bomb.getWidth();
}

// Get the height
int bomb::getHeight() const
{
	return _bomb.getHeight();
}

// Return the range
int bomb::getRange() const
{
	return _range;
}

// Get the range of a side
int bomb::getRange(int sideNb) const
{
	return _sideRange[sideNb];
}

// Return true if the time is end
bool bomb::timeOut() const
{
	return _bomb.isLastFrame();
}

// Check the time elapsed
void bomb::checkTime(int time)
{
	_timeRemaining -= time;

	if (_timeRemaining <= 0)
		_explode = true;
}

// The bomb is explosing
bool bomb::explode()
{
	return _explode;
}

// Draw the bomb
void bomb::draw(RenderWindow &window)
{
	_timer = _chrono.getElapsedTime();

	_timeRemaining -= _timer.asMilliseconds();

	if (_explode)								// If the bomb is explosing
	{
		if (_timeRemaining <= 0)				// Check if it is time to change frame
		{
			for (int i = 0; i < 4; i++)			// Draw the ends
			{
				_end[i].draw(window, true);

				for (int j = 1; j < _sideRange[i]; j++)	// Draw the sides (if the range is more than 0)
					_side[i][j].draw(window, true);
			}

			_center.draw(window, true);

			_timeRemaining = 10000;
		}
		else
		{
			for (int i = 0; i < 4; i++)					// Draw the ends
			{
				_end[i].draw(window);

				for (int j = 1; j < _sideRange[i]; j++)	// Draw the sides (if the range is more than 0)
					_side[i][j].draw(window);
			}

			_center.draw(window);
		}
	}
	else
	{
		if (_timeRemaining <= 0)				// Check if it is time to change frame
		{
			_explode = _bomb.isLastFrame();
			_bomb.draw(window, true);
			_timeRemaining = 30000;
		}
		else
			_bomb.draw(window);
	}
}

// Return if the bomb is blowed
bool bomb::isBlowed()
{
	return (_explode && _center.isLastFrame());
}

// Detroyer
bomb::~bomb()
{
	for (int i = 0; i < 4; i++)					// For each side
	{
		delete[] _side[i];
		_side[i] = nullptr;
	}
}