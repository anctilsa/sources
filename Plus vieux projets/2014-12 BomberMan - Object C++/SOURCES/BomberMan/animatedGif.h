/***********************************************************************************************************\
|File	: animatedGif.h																						|
|Autor	: Samuel Anctil																						|
|Date	: 2014-11-27																						|
|Goal	: Generic class using as model for the management of sprites from a texture. Allow to create		|
|		  animated animated gif easily with SFML. Is composed of one texture containing one or more charset |
|		  that can be divided with row and col to get an image. Only one charset which is creating from a 	|
|		  sprite of the texture can be set at a time.														|
\***********************************************************************************************************/

// Preprocessor directives
#pragma once
#include <SFML\Graphics.hpp>
#include <SFML\System\Vector2.hpp>
#include <math.h>
#include <assert.h>

using namespace std;
using namespace sf;

class animatedGif
{
protected:
	int _nbCol,								// Nb of col
		_nbLn,								// Nb of lines
		_col,								// Relative and actual col
		_ln,								// Relative and actual line
		_interDep,							// Interval between each images
		_actualInterDep,					// Actual inter dep
		_posX,								// Position x
		_posY,								// Position y
		_frameNbLimit;						// Frame limit (the last one)

	bool _repeat,							// If it calls the next img after at the end of a line of sprite
		 _usingRow;							// If it uses the line (as one big col)

	Texture *_texture;						// The texture
	Sprite _sprite;							// The sprite (part of the texture contening the gif)		
	IntRect _rect;							// Rect of the image

	int getCol(int) const;					// Return the col nb of a frame nb
	int getLn(int) const;					// Return the line nb of a frame nb

	void setParam(int, int);				// Set the param of the gif (protected)

public:
	animatedGif();											// Builder
	animatedGif(char*, int = 0, int = 0);					// Builder using a file name	
	animatedGif(char*, const IntRect&, int = 0, int = 0);	// Builder using a file name & rect
	animatedGif(Texture&, int = 0, int = 0);				// Builder using an existing texture
	animatedGif(Texture&, const IntRect&, int = 0, int = 0);// Builder using an existing texture & rect

	void init(char*, int = 0, int = 0);						// Init the gif using a file name
	void init(char*, const IntRect&, int = 0, int = 0);		// Init the gif using a file name
	void init(Texture&, int = 0, int = 0);					// Init the gif using an existing texture
	void init(Texture&, const IntRect&, int = 0, int = 0);	// Init the gif using an existing texture & rect

	void change(int, int);					// Change the gif from the texture and the param
	void change(int, int, const IntRect&);	// Change the gif from the texture and the param & rect

	void setFrame(int = 0);					// Set the frame using frame number
	void setFrame(int, int);				// Set the frame using the col and row of a frame

	void setInterDep(int);					// Set the interval between each images
	void setRepeat(bool);					// Set the repeat state
	void setUsingRow(bool);					// Set if using row
	void setFrameNbLimit(int);				// Set the last frame

	void setPosX(int);						// Set the pos x
	void setPosY(int);						// Set the pos y
	void setPosition(int, int);				// Set the pos
	void setRotation(int);					// Set the angle of the gif

	int getNbCol() const;					// Get the nb of col				
	int getNbLn() const;					// Get the nb of line
	int getPosX() const;					// Get the pos x
	int getPosY() const;					// Get the pos y
	int getWidth() const;					// Get the img width
	int getHeight() const;					// Get the img heigth

	IntRect getRectImage() const;			// Get the pos and the dim of the gif
	IntRect getRectCol(int) const;			// Get the pos and the dim of a col
	IntRect getRectLn(int) const;			// Get the pos and the dim of a line

	Texture& getTexture() const;		    // Get the texture

	bool isLastFrame() const;				// Check if the actual frame is the last
	bool isRepeated() const;				// Check if the animation is being repeated

	int nextFrame();						// Place the frame to the next one

	void draw(RenderWindow&, bool = false);	// Draw the actual frame
};