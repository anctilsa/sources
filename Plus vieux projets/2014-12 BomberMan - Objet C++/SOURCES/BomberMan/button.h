/***********************************************************************************************************\
| Library name	: button.h																					|
| Creator name	: Mike Girard																				|
| Creation date	: 27-11-2014																				|
| Description	: A button that can be clicked and highlighted												|
\***********************************************************************************************************/

// Preprocessor directives
#pragma once
#include <SFML\Graphics.hpp>
#include <string>
#include <assert.h>
#include "messageBox.h"

using namespace sf;
using namespace std;

// Library button
class button
{
private:
	int _posX,									// Button position on X axis
		_posY;									// Button position on Y axis

	Sprite _spriteButton;						// Button image

	Texture _textureButton;						// Button texture

public:
	button(char* = "default.png", int = 0, int = 0);// Builder

	void init(char*, int, int);					// initialize the values
	void init(Texture&, int, int, int, int);	// initialize the values

	int getPosX();								// Return position on X axis
	int getPosY();								// Return position on Y axis
	Sprite getSprite();							// Return the image
	Texture getTexture();						// Return the texture

	void loadTexture(char*);					// Load a texture

	bool isOver(Vector2i&);						// Check if the mouse or selection is over
	bool isOver(Vector2i&, int, int);			// Check if the mouse or selection is over

	void draw(RenderWindow&);					// Show the button				

	bool isClicked(Vector2i&, Event&);			// Check if the button is clicked

	~button();									// Destroyer
};