/***********************************************************************************************************\
| Library name	: block.h																					|
| Creator name	: Mike Girard																				|
| Creation date	: 30-11-14																					|
| Description	: A block that can be blowed																|
\***********************************************************************************************************/

#pragma once
#include <SFML\Graphics.hpp>

using namespace sf;

// Object block
class block
{
private:
	Sprite _spriteBlock;						// Block sprite

	Texture *_textureBlock;						// Block texture

	bool _blow;									// The block explode

	int _posX,									// Block position on X axis
		_posY,									// Block position on Y axis
		_blowedState,							// The block "blowing state"
		_destroyState;							// At this point the wall is destroyed

public:
	block(Texture&, int = 0, int = 0);			// Builder
	block(block &);								// Copy builder

	void init(int, int, Texture&);				// Initialise the block

	int getPosX();								// Return posX
	int getPosY();								// Return posY

	void blow();								// Change blow state

	bool isBlowed();							// Return if the block if blowed
	
	void draw(RenderWindow&);					// Draw the block
	
	~block();									// Destroyer
};