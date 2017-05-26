/***********************************************************************************************************\
| Program name		: BomberMan																				|
| Creation date		: 27-11-2014																			|
| Progammor's names	: Mike Girard																			|
| Description		: Bomberman game: You play a man that put bombs in a map in order to kill enemy's and	|
|						destroy block that block it's way in order to find the portal for the next level.	|
\***********************************************************************************************************/

// Preprocessor directives
#include <SFML\Graphics.hpp>
#include "menu.h"
#include "button.h"
#include "game.h"
#include "man.h"
//setFrameLimit
using namespace sf;

// Main program
int main()
{
	// Declaration of the variables
	Event event;								// Event
	RenderWindow window;						// The window

	game game;									// Allow to make the gestion of the game

	window.create(sf::VideoMode(600, 600), "BomberMan", sf::Style::Default);

	game.play(window);

	return 0;
}