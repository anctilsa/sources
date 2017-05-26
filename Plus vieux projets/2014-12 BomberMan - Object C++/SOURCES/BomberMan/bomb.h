/***********************************************************************************************************\
|File	: bomb.h																							|
|Autor	: Samuel Anctil																						|
|Date	: 2014-12-09																						|
|Goal	: Is composed of a bomb that can explode with a range.												|
\***********************************************************************************************************/

// Preprocessor directives
#pragma once
#include "animatedGif.h"
#include <assert.h>

using namespace std;
using namespace sf;

class bomb
{
	private:
		bool _explode;							// State of the bomb - explosing or not

		int _range,								// General range of the explosion
			_sideRange[4];						// Individual range (for each side)

		animatedGif _bomb,						// The bomb
					_explosion[3],				// Containt all the parts of the explosion
					_end[4],					// The 4 ends
					*_side[4],					// Tab contening 4 dynamic tab for the sides
					_center;					// The center

		int _timeRemaining;						// Time remaining before the explosion

		Time _timer;							// Timer to check the time remaining
		Clock _chrono;							// Chronometer for the timer

		void init();							// Init the variables

		void setExplosion();					// Init the parameters of each part
		void initEnds();						// Init the ends
		void updateSides();						// Alocate the sides

		void setPositionEnd(int);				// Set a end depending of his nb
		void setPositionSide(int, int);			// Set a side depending of his nb and of the depth range
		void setPositions();					// Set all the positions

	public:
		bomb();										// Builder
		bomb(char*, int, int);						// Builder using a file name
		bomb(char*, const IntRect&, int, int);		// Builder using a file name & rect
		bomb(Texture&, int, int);					// Builder using a texture
		bomb(Texture&, const IntRect&, int, int);	// Builder using a texture & rect
		bomb(bomb&);								// Copy builder

		void init(char*, int, int);						// Init using a file name
		void init(char*, const IntRect&, int, int);		// Init using a file name & rect
		void init(Texture&, int, int);					// Init using a texture
		void init(Texture&, const IntRect&, int, int);	// Init using a texture & rect

		void setExplosion(char*, int, int);						// Set the explosion using a file name
		void setExplosion(char*, const IntRect&, int, int);		// Set the explosion using a file name & rect
		void setExplosion(Texture&, int, int);					// Set the explosion using a texture
		void setExplosion(Texture&, const IntRect&, int, int);	// Set the explosion using a texture & rect

		void setRange(int);						// Set the range
		void setRange(int, int, int, int);		// Set the range limit of each sides (for obstacles)
		void setPosition(int, int);				// Set the position

		int getPosX() const;					// Get the pos x
		int getPosY() const;					// Get the pos y
		int getWidth() const;					// Get the width
		int getHeight() const;					// Get the height
		int getRange() const;					// Get the range
		int getRange(int) const;				// Get the range of a side

		bool timeOut() const;					// Return true if the time is end

		void checkTime(int);					// Check the time elapsed
		void draw(RenderWindow&);				// Drop the bomb and make it explode

		bool explode();							// The bomb is explosing
		bool isBlowed();						// Return if the bomb is blowed

		~bomb();								// Detroyer
};