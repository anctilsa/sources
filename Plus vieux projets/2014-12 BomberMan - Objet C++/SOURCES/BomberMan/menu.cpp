/***********************************************************************************************************\
| Program name	: menu.cpp																					|
| Creator name	: Mike Girard																				|
| Creation date	: 28-11-2014																				|
| Description	: Definition's of menu methods																|
\***********************************************************************************************************/

// Preprocessor directives
#pragma once
#include "menu.h"

// Builder without parameters
menu::menu()
{
}

// Builder with parameters
menu::menu(Texture &texture)
{
	loadTexture("menuBackground.png", texture);
}

// Load the background texture
void menu::loadTexture(char *fileName, Texture &texture)
{
	if (!(_textureBackground.loadFromFile(fileName)))// The file is not found
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
		_spriteBackground.setTexture(_textureBackground);

	_textureName = &texture;
	_spriteName.setTexture(*_textureName);
	_spriteName.setTextureRect(IntRect(Vector2i(200, 0), Vector2i(600, 100)));
}

// Show the menu following what is in the boolean (enable menu change) and return a boolean
bool menu::show(RenderWindow &window, bool defaultMenu)
{
	Time timer;									// Timer for frame rate
	Clock chrono;								// Chrono for time elapsed
	Event even;									// Event container

	float tempRafraichissement = 1 / (float)60;	// Frame limit

	if (defaultMenu)							// The default menu is to show
	{
		_menuButton = new button[3];
		(_menuButton + 0)->init(*_textureName, 100, 150, 400, 100);
		(_menuButton + 1)->init(*_textureName, 100, 300, 400, 300);
		(_menuButton + 2)->init(*_textureName, 100, 450, 400, 500);

		animation(window, timer, chrono, 3);

		while (true)							// Turn until a choice is made
		{
			timer = chrono.getElapsedTime();

			while (window.pollEvent(even))		// Check events
			{
				switch (even.type)				// Chose the type of event
				{
				case Event::KeyPressed:			// In the case a keyboard was pressed
					switch (even.key.code)		// Find the pressed key
					{
					case Keyboard::Q:			// Enter is pressed
						return false;
						break;
					}
					break;
				}

				if ((_menuButton + 0)->isClicked(Mouse::getPosition(window), even))
				{
					delete[] _menuButton;
					return true;
				}

				if ((_menuButton + 2)->isClicked(Mouse::getPosition(window), even))
				{
					delete[] _menuButton;
					return false;
				}
			}

			timer = chrono.getElapsedTime();

			if (timer.asSeconds() >= tempRafraichissement)// Limit frame rate
			{
				window.clear(Color(255, 255, 255));

				window.draw(_spriteBackground);
				window.draw(_spriteName);

				for (int i = 0; i < 3; i++)
				{
					(_menuButton + i)->isOver(Mouse::getPosition(window), 400, (i * 200) + 100);
					(_menuButton + i)->draw(window);
				}

				window.display();

				timer = chrono.restart();
			}
		}
	}
	else
	{
	}

	return true;
}

// Animation of the menu
void menu::animation(RenderWindow &window, Time &timer, Clock &chrono, int nbImage)
{
	int imageAppeared = 0;
	float tempRafraichissement = 1 / (float)60;	// Frame limit

	_spriteName.setPosition(0, 600);

	while (_spriteName.getPosition().y > 0)
	{
		timer = chrono.getElapsedTime();

		if (timer.asSeconds() >= tempRafraichissement)// Limit frame rate
		{
			_spriteName.setPosition(_spriteName.getPosition().x, _spriteName.getPosition().y - 3);

			window.clear(Color(255, 255, 255));

			window.draw(_spriteBackground);
			window.draw(_spriteName);

			window.display();

			timer = chrono.restart();
		}
	}

	timer = chrono.restart();

	while (imageAppeared < nbImage)
	{
		timer = chrono.getElapsedTime();

		if (timer.asMilliseconds() >= 400)// Limit frame rate
		{
			imageAppeared++;

			window.clear(Color(255, 255, 255));

			window.draw(_spriteBackground);
			window.draw(_spriteName);

			for (int i = 0; i < imageAppeared; i++)
				(_menuButton + i)->draw(window);

			window.display();

			timer = chrono.restart();
		}
	}
}

// Destroyer
menu::~menu()
{
}