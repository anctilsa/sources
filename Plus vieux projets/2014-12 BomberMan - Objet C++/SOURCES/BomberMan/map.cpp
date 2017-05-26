/***********************************************************************************************************\
| Program name	: Map.cpp																					|
| Creator name	: Julien-Dany Hamel & Mike Girard															|
| Creation date	: 20-10-14																					|
| Description	: Map's methods description																	|
\***********************************************************************************************************/

#pragma once
#include "Map.h"

// Builder without parameters
Map::Map()
{
	_nbCol = _nbLn = 0;

	_map = nullptr;
	_texture = nullptr;
}

// Builder with parameters
Map::Map(Texture &texture, int nbCol, int nbRow, int caseWidth, int caseHeight)
{
	init(texture, nbCol, nbRow, caseWidth, caseHeight);
}

// Initalise the map
void Map::init(Texture &texture, int nbCol, int nbRow, int caseWidth, int caseHeight)
{
	_nbCol = nbCol;
	_nbLn = nbRow;

	for (int i = 0; i < nbRow; i++)				// Init the lines
	{
		for (int j = 0; j < nbCol; j++)			// init the columns
			*(*(_map + i) + j) = ' ';
	}

	_texture = &texture;

	_picture.setTexture(*_texture);

	_caseSize.x = caseWidth;
	_caseSize.y = caseHeight;
}

// Set the number of column
void Map::setColomn(int nb)
{
	assert(nb >= 0);

	_nbCol = nb;
}

// Set the number of lines
void Map::setLine(int nb)
{
	assert(nb >= 0);

	_nbLn = nb;
}

// Set a element in the table
void Map::setElement(char e, int col, int row)
{
	assert(col >= 0 && row >= 0);
	assert(col <= _nbCol && row <= _nbLn);

	*(*(_map + row) + col) = e;
}


// Get a element from the table
char Map::getElement(const int col, const int row)const
{
	assert(col >= 0 && row >= 0);
	assert(col <= _nbCol && row <= _nbLn);

	return *(*(_map + row) + col);
}

// Return the nb of col
int Map::getNbCol() const
{
	return _nbCol;
}

// Return the nb of ln
int Map::getNbLn() const
{
	return _nbLn;
}

// Adjust the position
Vector2i Map::adjustPosition(IntRect &r, int width, int height)
{
	Vector2i pos;

	if ((r.left % r.width) + (width - r.width) != 0)
	{											// Check if the bomb is aligned on x axis
		if ((r.left % width) < (width / 2))		// A bit left
			pos.x = r.left - (r.left % width);
		else
			pos.x = r.left + (width - (r.left % width));
	}

	if ((r.top % r.height) + (height - r.height) != 0)
	{											// Check if the bomb is aligned on y axis
		if ((r.top % height) < (height / 2))	// A bit Up
			pos.y = r.top - (r.top % height);
		else
			pos.y = r.top + (height - (r.top % height));
	}

	return pos;
}

// Return if there is a collision
bool Map::collision(const IntRect &r, int dir, int x, int y) const
{
	assert(_caseSize.x <= r.width && _caseSize.y <= r.height); // The rect may be smaller than the case

	float col = (r.left - x) / _caseSize.x,
		  ln = (r.top - y) / _caseSize.y;
	
	switch (dir)								// Check the collisions following the direction
	{
	case 0:										// Up
		return collision(_caseSize.y, col, ln - 1);
		break;

	case 1:										// Right
		return collision(_caseSize.x, col + 1, ln);
		break;

	case 2:										// Down
		return collision(_caseSize.y, col, ln + 1);
		break;

	case 3:										// Left
		return collision(_caseSize.x, col - 1, ln);
		break;
	}

	return false;
}

// Check if there is a collision
bool Map::collision(int lengthCase, float col, float ln) const
{
	if (col < (int)col)		// A side of the rect is colapse to a case
	{
		if (getElement((int)col, ln) != '0' ||
			getElement((int)col + 1, ln) != '0')			// If the element is not 0
			return true;
	}

	return false;
}

// Charger les éléments de la Map
void Map::load(ifstream &input)
{
	assert(input.is_open());

	char temp;									// File for unusefull chacter in the input stream

	input >> _nbLn >> _nbCol;

	_map = new char*[_nbLn];

	for (int i = 0; i < _nbLn; i++)				// Create the rows
	{
		for (int j = 0; j < _nbCol; j++)		// Create the columns
			*(_map + i) = new char[_nbCol];
	}

	for (int i = 0; i < _nbLn; i++)				// Load the element in the rows
	{
		for (int j = 0; j < _nbCol; j++)		// Load the element in the columns
			input >> *(*(_map + i) + j);
	}
}

// Print the Map in console
ostream& Map::print(ostream &output)const
{
	for (int i = 0; i < _nbLn; i++)				// Print the lines in the console
	{
		for (int j = 0; j < _nbCol; j++)		// Print the columns in the console
			output << *(*(_map + i) + j);

		output << endl;
	}

	return output;
}

// Print the Map in SFML
void Map::draw(RenderWindow &window, int posX, int posY)
{
	for (int i = 0; i < _nbLn; i++)				// Draw the tiles in the lines
	{
		for (int j = 0; j < _nbCol; j++)		// Draw the tiles in the columns
		{
			switch (*(*(_map + i) + j))			// Depending on what char it is
			{
			case '1':							// Wall
				_picture.setPosition(j * _caseSize.x + posX, i * _caseSize.y + posY);
				_picture.setTextureRect(IntRect(Vector2i(0, 0), _caseSize));
				break;

			default:
				_picture.setPosition(j * _caseSize.x + posX, i * _caseSize.y + posY);
				_picture.setTextureRect(IntRect(Vector2i(40, 0), _caseSize));
				break;
			}

			window.draw(_picture);
		}
	}
}

// Destroyer
Map::~Map()
{
	for (int i = 0; i < _nbLn; i++)				// Destroy the dynamic table
		delete[] * (_map + i);

	delete[] _map;

	_nbLn = _nbCol = 0;
}

// Operator << overloading
ostream& operator<<(ostream &sortie, const Map &m)
{
	m.print(sortie);

	return (sortie);
}