/***********************************************************************************************************\
|File	: character.h																						|
|Autor	: Samuel Anctil	& Mike Girard																		|
|Date	: 2014-11-27																						|
|Goal	: Generic class using as model for creating characters of any kinds.								|
\***********************************************************************************************************/

// Preprocessor directives
#pragma once
#include "animatedGif.h"
#include <SFML\Graphics.hpp>
#include <assert.h>

using namespace std;
using namespace sf;

class character
{
	protected:
		int _posX,								// Position x
			_posY,								// Position y
			_dir,								// Direction
			_dep,								// Pos of the character
			_nbLife;							// Number of life or health point
			
		float _speed;							// Speed

		bool _move,								// Make the character move
			 _alive;							// Statut of the character (Die or alive)

		IntRect _limit;							// The limits for the pos X and Y
		animatedGif _gif;						// Used to manage the sprites of the caracter from a texture

	public:
		character();							// Builder
		character(char*, int, int);						// Builder using a file name
		character(char*, int, int, const IntRect&);		// Builder using a file name & rect
		character(Texture&, int, int);					// Builder using a texture
		character(Texture&, int, int, const IntRect&);	// Builder using a texture & rect

		virtual void init(char*, int, int);						// Init using a file name
		virtual void init(char*, int, int, const IntRect&);		// Init using a file name & rect
		virtual void init(Texture&, int, int);					// Init using a texture
		virtual void init(Texture&, int, int, const IntRect&);	// Init using a texture & rect

		int posX() const;						// Return the position x
		int posY() const;						// Return the position y
		int width() const;						// Return the width of the character
		int height() const;						// Return the heigth of the character
		int dir() const;						// Return the direction
		IntRect getRect() const;				// Return the rect of the window contening the charact
		
		bool alive() const;						// Return the statut

		void setPosition(int, int);				// Set the position
		void setDirection(int);					// Set the direction
		void setSpeed(float);					// Set the speed
		void setLimit(const IntRect&);			// Set the limit for the pos X and Y

		void enableMove();						// Set move to true
		void stopMove();						// Set move to false

		bool isMoving() const;					// Return true if the character is moving

		void move();							// Change the posX and the posY depending of the direction
		void slide(int);						// The character slide
		void slideUp();							// The character slide up
		void slideLeft();						// The character slide left
		void slideDown();						// The character slide down
		void slideRight();						// The character slide right

		virtual void hit() = 0;

		void draw(RenderWindow&);				// Draw the character
};