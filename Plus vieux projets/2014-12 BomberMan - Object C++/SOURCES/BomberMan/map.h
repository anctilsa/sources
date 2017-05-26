/***********************************************************************************************************\
| Program name	: Map.h																						|
| Creator name	: Julien-Dany Hamel & Mike Girard															|
| Creation date	: 20-10-14																					|
| Description	: Initialize and load values of a Map with a stream.										|
\***********************************************************************************************************/

#pragma once
#include <iostream>
#include <assert.h>
#include <fstream>
#include <iostream>
#include <SFML\Graphics.hpp>

using namespace std;
using namespace sf;

class Map
{
	private:
		int _nbCol,									// Number of columns
			_nbLn;									// Number of lines

		char **_map;								// Map grid

		Texture *_texture;							// Map textures
		Sprite _picture;							// Map picture
		Vector2i _caseSize;							// Size of a case

		Map(Map &);									// Copy Builder

		bool Map::collision(int, float, float) const; // Check if there is a collision

	public:
		Map();										// Builder without parameters
		Map(Texture&, int=0, int=0, int=0, int=0);	// Builder with parameters

		void init(Texture&, int, int, int, int);	// Initalise the map

		void setColomn(int);						// Set the number of column
		void setLine(int);							// Set the number of lines
		void setElement(char, int, int);			// Set a element in the table

		char getElement(int, int) const;			// Return a element of the table
		int getNbCol() const;						// Return the nb of col
		int getNbLn() const;						// Return the nb of ln

		Vector2i adjustPosition(IntRect&, int=0, int=0); // Adjust the position
		bool collision(const IntRect&, int, int=0, int=0) const; // Check if there is a collision

		void load(ifstream &);						// Load a Map from a file

		ostream& print(ostream &) const;			// Print the Map in the console
		void draw(RenderWindow &, int=0, int=0);	// Print the Map in SFML

		~Map();										// Destroyer
};

ostream& operator<<(ostream &, const Map &);	// Operator << overloading