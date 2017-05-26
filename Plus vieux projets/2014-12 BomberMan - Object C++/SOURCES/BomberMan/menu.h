/***********************************************************************************************************\
| Library name	: menu.h																					|
| Creator name	: Mike Girard																				|
| Creation date	: 27-11-2014																				|
| Description	: Show a menu for a game with a background and buttons										|
\***********************************************************************************************************/

// Preprocessor directives
#pragma once
#include <SFML\Graphics.hpp>
#include <string>
#include <assert.h>
#include "button.h"

using namespace sf;
using namespace std;

// Library menu
class menu
{
private:
	Sprite _spriteBackground,					// Background image
		   _spriteName;							// Menu name

	Texture _textureBackground,					// Background texture
		    *_textureName;						// Menu texture

	button *_menuButton;						// Menu's buttons

public:
	menu();										// Builder without parameters
	menu(Texture&);								// Builder with parameters
	
	void loadTexture(char*, Texture&);			// Load the background texture

	bool show(RenderWindow&, bool = true);		// Show the menu following what is in the boolean
												// (enable menu change) and return a boolean
	void animation(RenderWindow&, Time&, Clock&, int);// Animation of the menu

	~menu();									// Destroyer
};