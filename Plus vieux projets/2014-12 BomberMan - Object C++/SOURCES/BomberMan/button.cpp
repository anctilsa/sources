/***********************************************************************************************************\
| Program name	: button.cpp																				|
| Creator name	: Mike Girard																				|
| Creation date	: 27-11-2014																				|
| Description	: Definition's of button methods															|
\***********************************************************************************************************/

// Preprocessor directives
#pragma once
#include "button.h"

// Builder
button::button(char *fileName, int posX, int posY)
{
	init(fileName, posX, posY);
}

// initialize the values
void button::init(char *fileName, int posX, int posY)
{
	if (posX < 0 || posY < 0)					// Position is too small
	{
		messageBox errorBox;

		if (posX<0)								// X position is too small
			errorBox.init("Erreur de position", "Position de X trop petite");
		else
			errorBox.init("Erreur de position", "Position de Y trop petite");

		errorBox.show();

		exit(0);
	}

	_posX = posX;
	_posY = posY;

	loadTexture(fileName);
	_spriteButton.setPosition(_posX, _posY);
}

// initialize the values
void button::init(Texture &texture, int posX, int posY, int pointLeft, int pointUp)
{
	_posX = posX;
	_posY = posY;

	_textureButton = texture;
	_spriteButton.setTexture(_textureButton);
	_spriteButton.setTextureRect(IntRect(Vector2i(pointLeft, pointUp), Vector2i(400, 100)));
	_spriteButton.setPosition(_posX, _posY);
}

// Return position on X axis
int button::getPosX()
{
	return _posX;
}

// Return position on Y axis
int button::getPosY()
{
	return _posY;
}

// Return the image
Sprite button::getSprite()
{
	return _spriteButton;
}

// Return the texture
Texture button::getTexture()
{
	return _textureButton;
}

// Load a texture
void button::loadTexture(char *fileName)
{
	if (!_textureButton.loadFromFile(fileName))	// The file is not found
	{
		string	message = "Le fichier d'image :\n";// The message to show
		message += fileName;
		message += "\nn'a pas été trouvé";

		char *errorMessage;						// The message to show
		errorMessage = new char[message.size() + 1];

		for (int i = 0; i < message.size() + 1; i++)// Put the string in the char
			errorMessage[i] = message[i];

		messageBox errorBox("Erreur de chargement de l'image", errorMessage);// Error box

		errorBox.show();

		exit(0);
	}
	else
	{
		_spriteButton.setTextureRect(IntRect(0, 0, 400, 100));
		_spriteButton.setTexture(_textureButton);
	}
}

// Check if the mouse is over
bool button::isOver(Vector2i &position)
{
	if (position.x >= _posX && position.x <= (_posX + _spriteButton.getGlobalBounds().width) &&
		position.y >= _posY && position.y <= (_posY + _spriteButton.getGlobalBounds().height))
	{											// Check if the mouse is over
		_spriteButton.setTextureRect(IntRect(0, 100, 400, 100));
		return true;
	}

	_spriteButton.setTextureRect(IntRect(0, 0, 400, 100));
	return false;
}

// Check if the mouse or selection is over
bool button::isOver(Vector2i &position, int pointLeft, int pointUp)
{
	if (position.x >= _posX && position.x <= (_posX + _spriteButton.getGlobalBounds().width) &&
		position.y >= _posY && position.y <= (_posY + _spriteButton.getGlobalBounds().height))
	{											// Check if he mouse is over
		_spriteButton.setTextureRect(IntRect(pointLeft, pointUp + 100, 400, 100));
		return true;
	}

	_spriteButton.setTextureRect(IntRect(pointLeft, pointUp, 400, 100));
	return false;
}

// Show the button
void button::draw(RenderWindow &window)
{
	window.draw(_spriteButton);
}

// Check if the button is clicked
bool button::isClicked(Vector2i &position, Event &even)
{
	if (even.type == Event::MouseButtonPressed &&
		even.mouseButton.button == Mouse::Left)	// Check if there is a left click
		if (isOver(position))					// Return if the mouse is over
			return true;

	return false;
}

// Destroyer
button::~button()
{
}