/***********************************************************************************************************\
|File	: animatedGif.cpp																					|
|Autor	: Samuel Anctil																						|
|Date	: 2014-11-27																						|
|Goal	: Ressource for the class animatedGif.																|
\***********************************************************************************************************/

// Preprocessor directives
#include "animatedGif.h"

// Builder using a str
animatedGif::animatedGif()
{
	_actualInterDep = _col = _ln = _posX = _posY = _frameNbLimit = 0;
	_nbCol = _nbLn = 1;
	_interDep = 5;

	_repeat = true;
	_usingRow = false;

	_rect.left = _rect.top = _rect.width = _rect.height = 0;
}

// Builder using a file name
animatedGif::animatedGif(char *fileName, int nbCol, int nbRow)
{
	init(fileName, nbCol, nbRow);
}

// Builder using a file name & rect
animatedGif::animatedGif(char *fileName, const IntRect &rect, int nbCol, int nbRow)
{
	init(fileName, rect, nbCol, nbRow);
}

// Builder using an existing texture
animatedGif::animatedGif(Texture &texture, int nbCol, int nbRow)
{
	init(texture, nbCol, nbRow);
}

// Builder using an existing texture & rect
animatedGif::animatedGif(Texture &texture, const IntRect &rect, int nbCol, int nbRow)
{
	init(texture, rect, nbCol, nbRow);
}

// Init the gif using a file name
void animatedGif::init(char *fileName, int nbCol, int nbRow)
{
	_texture->loadFromFile(fileName);
	_sprite.setTexture(*_texture);

	change(nbCol, nbRow);
}

// Init the gif using a file name
void animatedGif::init(char *fileName, const IntRect &rect, int nbCol, int nbRow)
{
	_texture->loadFromFile(fileName);
	_sprite.setTexture(*_texture);

	change(nbCol, nbRow, rect);
}

// Init the gif using a texture
void animatedGif::init(Texture &texture, int nbCol, int nbRow)
{
	_texture = &texture;
	_sprite.setTexture(*_texture);

	change(nbCol, nbRow);
}

// Init the gif using a texture
void animatedGif::init(Texture &texture, const IntRect &rect, int nbCol, int nbRow)
{
	_texture = &texture;
	_sprite.setTexture(*_texture);

	change(nbCol, nbRow, rect);
}

// Create the gif from the texture ant the param
void animatedGif::change(int nbCol, int nbRow)
{
	setParam(nbCol, nbRow);

	Vector2u dim = _texture->getSize();

	_rect.width = dim.x / _nbCol;
	_rect.height = dim.y / _nbLn;

	setFrame(0, 0);
}

// Create the gif from the texture ant the param & rect
void animatedGif::change(int nbCol, int nbRow, const IntRect &rect)
{
	setParam(nbCol, nbRow);

	_rect = rect;
	_rect.width /= _nbCol;
	_rect.height /= _nbLn;

	setFrame(0, 0);
}

// Set the param of the gif (protected)
void animatedGif::setParam(int nbCol, int nbRow)
{
	assert(nbCol > 0 && nbRow > 0);

	_nbCol = nbCol;
	_nbLn = nbRow;
	_frameNbLimit = nbCol * nbRow;
}

// Set the image using frame number
void animatedGif::setFrame(int frameNb)
{
	assert(frameNb >= 0);

	_col = getCol(frameNb);
	_ln = getLn(frameNb);

	_sprite.setTextureRect(_rect);
}

// Set the frame depending using the col and row of a frame
void animatedGif::setFrame(int col, int row)
{
	assert(col >= 0 && col <= _nbCol - 1 &&
		   row >= 0 && row <= _nbLn - 1);

	_col = col;
	_ln = row;
}

// Set the interval between each images
void animatedGif::setInterDep(int interDep)
{
	_interDep = interDep;
}

// Set the repeat state
void animatedGif::setRepeat(bool repeat)
{
	_repeat = repeat;
}

// Set if using row
void animatedGif::setUsingRow(bool usingRow)
{
	_usingRow = usingRow;
}

// Set the last frame
void animatedGif::setFrameNbLimit(int frameNbLimit)
{
	assert(frameNbLimit > 0 && frameNbLimit <= _nbCol * _nbLn);

	if ((isRepeated() && !_usingRow) && frameNbLimit >= _nbCol) // Using row and repetition
		assert(frameNbLimit <= _nbCol);

	_frameNbLimit = frameNbLimit;
}

// Return if the animation is being repeated
bool animatedGif::isRepeated() const
{
	return _repeat;
}

// Set the pos x
void animatedGif::setPosX(int posX)
{
	setPosition(posX, _posY);
}

// Set the pos y
void animatedGif::setPosY(int posY)
{
	setPosition(_posX, posY);
}

// Set the pos
void animatedGif::setPosition(int posX, int posY)
{
	//assert(posX >= 0 && posY >= 0);

	_posX = posX;
	_posY = posY;

	_sprite.setPosition(_posX, _posY);
}

// Set the angle of the gif
void animatedGif::setRotation(int angle)
{
	_sprite.setRotation(angle);
}

// Get the nb of col	
int animatedGif::getNbCol() const
{
	return _nbCol;
}

// Get the nb of line
int animatedGif::getNbLn() const
{
	return _nbLn;
}

// Get the pos x
int animatedGif::getPosX() const
{
	return _posX;
}

// Get the pos y
int animatedGif::getPosY() const
{
	return _posY;
}

// Get the img width
int animatedGif::getWidth() const
{
	return _rect.width;
}

// Get the img height
int animatedGif::getHeight() const
{
	return _rect.height;
}

// Get the col of a frame nb
int animatedGif::getCol(int frameNb) const
{
	assert(frameNb >= 0);

	return (frameNb - 1) % _nbCol;
}

// Get the line of a frame nb
int animatedGif::getLn(int frameNb) const
{
	int col = getCol(frameNb);					// The col

	return (_nbCol - ((frameNb - col) - 2 % _nbLn)) % _nbLn;
}

// Get the pos and the dim of the gif
IntRect animatedGif::getRectImage() const
{
	IntRect rect;

	rect.left = _posX;
	rect.top = _posY;
	rect.width = _rect.width;
	rect.height = _rect.height;

	return rect;
}

// Get the pos and the dim of a col
IntRect animatedGif::getRectCol(int colNb) const
{
	IntRect rect;

	rect.left = _posX;
	rect.top = _posY + (colNb * _rect.height);
	rect.width = _rect.width * _nbCol;
	rect.height = _rect.height;

	return rect;
}

// Get the pos and the dim of a line
IntRect animatedGif::getRectLn(int lnNb) const
{
	IntRect rect;

	rect.left = _posX + (lnNb * _rect.width);;
	rect.top = _posY;
	rect.width = _rect.width;
	rect.height = _rect.height * lnNb;

	return rect;
}

// Check the texture
Texture& animatedGif::getTexture() const
{
	return *_texture;
}

// Check if the actual frame is the last
bool animatedGif::isLastFrame() const
{
	if (isRepeated() && _usingRow)				// Using repetition and row
		return ((_col == getCol(_frameNbLimit) && _ln == getLn(_frameNbLimit)) ||
		(_col == _nbCol && _ln == _nbLn));	// Last frame
	else
		return (_col == getCol(_frameNbLimit)); // Last frame
}

// Place the frame to the next one
int animatedGif::nextFrame()
{
	static int initialCol = _col,
		initialRow = _ln;
	static bool end = false;					// False is the next frame is out

	_actualInterDep++;

	if (_actualInterDep == _interDep)			// If the intervale is 5
	{
		_actualInterDep = 0;

		if (isRepeated())						// Using repetition
		{
			if (end)							// Last frame
				_col = initialCol;
			else
				_col = ++_col % _nbCol;

			if (_usingRow)						// Using row and repetition
			{
				if (end)						// Last frame
					_ln = initialRow;
				else if (_col == 0)				// If col is 0, the row change	
					_ln = ++_ln % _nbLn;
			}
		}
		else if (_usingRow)						// Using row whitout repetition
		{
			if (!end)							// Not last frame
			{
				if (_col == 0)					// If col is 0, the row change
					_ln = ++_ln % _nbLn;
			}
		}
		else									// Not using row or repetition
		{
			if (!end)							// Not last frame
				_col = ++_col % _nbCol;
		}
	}

	end = isLastFrame();

	return _col;
}

// Draw the actual frame
void animatedGif::draw(RenderWindow &window, bool doNextFrame)
{
	_sprite.setTextureRect(IntRect((_col * _rect.width) + _rect.left, (_ln * _rect.height) + _rect.top, _rect.width, _rect.height));

	window.draw(_sprite);

	if (doNextFrame)							// If we want to change the frame
		nextFrame();
}