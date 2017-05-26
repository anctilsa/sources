/***********************************************************************************************************\
| Library name		: messageBox.h																			|
| Creation date		: 28-11-2014																			|
| Progammor's names	: Mike Girard																			|
| Description		:
\***********************************************************************************************************/

// Preprocessor directives
#pragma once
#include <SFML\Graphics.hpp>
#include <string>
#include <assert.h>

using namespace sf;
using namespace std;

// Object messageBox
class messageBox
{
private:
	string	_title,								// Title of the messageBox
			_message;							// Message in the box

	Font _font;									// Text font
	Text _text;									// Sfml text to show

public:
	messageBox(char* = "default", char* = "default");// Builder

	void init(char*, char*);					// Initialise the title and the text

	void show();								// Show the messageBox on the screen

	~messageBox();								// Destroyer
};