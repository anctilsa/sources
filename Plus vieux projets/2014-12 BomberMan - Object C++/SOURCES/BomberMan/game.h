/***********************************************************************************************************\
|File	: game.h																							|
|Autor	:																									|
|Date	: 2014-11-27																						|
|Goal	: Allow the gestion of the game bomber man.															|
\***********************************************************************************************************/

// Preprocessor directives
#pragma once
#include <SFML\Graphics.hpp>
#include <assert.h>
#include <list>
//include "vector.h"
//include "list.h"
#include "map.h"
#include "man.h"
#include "monster.h"
#include "bomb.h"
#include "menu.h"
#include "button.h"
#include "game.h"
#include "block.h"

using namespace std;
using namespace sf;

class game
{
	protected:
		bool _continue,							// Specifie if the game is running
			 _openMenu;							// Used to open the menu
		 
		float _fps;								// Frame per seconds

		Image _image;							// Image for the game
		Texture _texture;						// Texture for the game

		Time _timer;							// Timer
		Clock _chrono;							// Chrono 

		Event _event;							// User events

		Map _bomberMap;							// The map of the game

		menu _mainMenu;							// The main menu

		list<block>	_bomberBlock;				// A list of blocks
		list<block>::iterator _itBlock;			// Iterator of the block list

		//map _map;								// The map
		man _man;								// Bomber man
		list<bomb> _bomb;						// Dynamic tab contening the bombs
		list<bomb>::iterator _itBomb;			// Iteratorof bombs
		list<monster> _monster;					// List of monsters
		list<monster>::iterator _itMonster;		// Iterator of monsters
		//vector<monsters> _typeMonters;		// Vector containing all the types of monster
		
		bomb *_bombs;

		void initMap();							// Init the map
		void initBlock();						// Init the blocks
		void initManAndBomb();					// Init the man and the bombs
		void initMonster();						// Init the monsters

		void updateBlock(RenderWindow&);		// Update the blocks
		void updateManAndBomb(RenderWindow&);	// Update the man and the bombs
		void updateMonster(RenderWindow&);		// Update the monsters

	public:
		game();									// Builder

		void init();							// Init the game

		void play(RenderWindow&);
		void catchEvent(const Event&);			// Catch an event
		void update(RenderWindow&);				// Update the game state

		void changeDirection(int=0);			// Change the direction of a monster
		//bool collision(const character&) const; // Check if there is a collision in front of the character
		int canSlide(const character&) const;	// Check if the character can slide

		void placeBomb();						// The bomber man place a bomb

		void setBombRange(bomb &);				// Set the range of the explosion
		int findBombRange(bomb &, int, int = 0);// Find the range of each side for the explosion
		
		void bombExplose(bomb &);				// Make a block explose
		void killCharacter(bomb &, character &);// Kill or not a character

		~game();								// Destroyer
};
