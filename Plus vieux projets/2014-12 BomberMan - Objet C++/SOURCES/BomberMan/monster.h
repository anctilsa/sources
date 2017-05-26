/***********************************************************************************************************\
|File	: monster.h																							|
|Autor	: Samuel Anctil & Mike Girard																		|
|Date	: 2014-11-27																						|
|Goal	: Monster that move randomly and return a tally when killed.										|
\***********************************************************************************************************/

// Preprocessor directives
#pragma once
#include "character.h"
#include <SFML\Graphics.hpp>
#include <assert.h>

using namespace std;
using namespace sf;

class monster : public character
{
private:
	int _tally,								// Tally get when the monster die
		_nbColision,						// Nb of colision (for changeDirection())
		_nbTry,								// Nb of try (for changeDirection())
		_interDep;							// Inter dep (for changeDirection())

	virtual void init(int=0);				// Init the param

public:
	monster();											// Builder
	monster(char*, int=0);								// Builder using a file name
	monster(char*, const IntRect&, int=0);				// Builder using a file name & rect
	monster(Texture&, int=0);							// Builder using a texture
	monster(Texture&, const IntRect&, int=0);			// Builder using a texture & rect

	virtual void init(char*, int=0);					// Init using a file name
	virtual void init(char*, const IntRect&, int=0);	// Init using a file name & rect
	virtual void init(Texture&, int=0);					// Init using a texture
	virtual void init(Texture&, const IntRect&, int=0);	// Init using a texture & rect

	int getTally() const;					// Return the tally value

	void changeDirection();					// Change the dir randomly depending of the dir
	void hit();								// Hit
	bool dead();							// The monster is dead

	~monster();								// Destroyer
};