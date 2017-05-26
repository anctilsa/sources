/***********************************************************************************************************\
| Program name	: block.cpp																					|
| Creator name	: Mike Girard																				|
| Creation date	: 30-11-14																					|
| Description	: Definition's of block methods																|
\***********************************************************************************************************/

#pragma once
#include "block.h"

// Builder
block::block(Texture &texture, int posX, int posY)
{
	init(posX, posY, texture);
}

// Copy builder
block::block(block &copiedBlock)
{
	_textureBlock = copiedBlock._textureBlock;
	_spriteBlock = copiedBlock._spriteBlock;

	_blow = copiedBlock._blow;

	_posX = copiedBlock._posX;
	_posY = copiedBlock._posY;
	_blowedState = copiedBlock._blowedState;
	_destroyState = copiedBlock._destroyState;
}

// Initialise the block
void block::init(int posX, int posY, Texture &texture)
{
	_posX = posX;
	_posY = posY + 80;
	_textureBlock = &texture;
	_blow = false;
	_blowedState = 0;
	_destroyState = 25;

	_spriteBlock.setTexture(*_textureBlock);
	_spriteBlock.setPosition(_posX, _posY);
}

// Return posX
int block::getPosX()
{
	return _posX;
}

// Return posY
int block::getPosY()
{
	return _posY;
}

// Change blow state
void block::blow()
{
	_blow = true;
}

// Return if the block if blowed
bool block::isBlowed()
{
	return (_blowedState == _destroyState);
}

// Draw the block
void block::draw(RenderWindow &window)
{
	if (_blow)									// Check if the block is blowing
	{
		_spriteBlock.setTextureRect(IntRect(40 * (_blowedState / 5), 40, 40, 40));
		_blowedState++;
	}
	else
		_spriteBlock.setTextureRect(IntRect(80, 0, 40, 40));

	if (_blowedState == _destroyState)			// Check if the block is done blowing
		_blow = false;

	window.draw(_spriteBlock);
}

// Destroyer
block::~block()
{
	_posX = 0;
	_posY = 0;
	_blow = false;
	_blowedState = 0;
}