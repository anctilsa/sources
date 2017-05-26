/***********************************************************************************************************\
|File	: bomberMan.h																						|
|Autor	: Samuel Anctil	& Mike Girard																		|
|Date	: 2014-11-27																						|
|Goal	: Robot that can place and make explode bombs.														|
\***********************************************************************************************************/

// Preprocessor directives
#pragma once
#include "character.h"
#include <assert.h>

using namespace std;
using namespace sf;

class man : public character
{
	private:
		int _nbBomb,							// Number of bomb
			_nbPoint;							// Number of point

		virtual void init(int = 3, int = 1);	// Init the param

	public:
		man();										// Builder
		man(char*, int=3, int=1);					// Builder using a file name
		man(char*, const IntRect&, int=3, int=1);	// Builder using a file name & rect
		man(Texture, int=3, int=1);					// Builder using a texture
		man(Texture, const IntRect&, int=3, int=1);	// Builder using a texture & rect

		virtual void init(char*, int=3, int=1);						// Init using a file name
		virtual void init(char*, const IntRect&, int=3, int=1);		// Init using a file name & rect
		virtual void init(Texture&, int=3, int=1);					// Init using a texture
		virtual void init(Texture&, const IntRect&, int=3, int=1);	// Init using a texture & rect

		int getNbPoint() const;					// Return the nb of point
		int getNbBomb() const;					// Return the nb of bomb
		int getNbLife() const;					// Return the nb of life
		
		void hit();								// Make die bomber man
		void relive();							// Make relive bomber man

		bool canPlaceBomb();					// Return true if bomber man can place a bomb
		void placeBomb();						// Place a bomb
		void bombExploded();					// A bomb exploded

		void update();							// Change the frame depending of the statut
};