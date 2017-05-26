/***********************************************************************************************************\
| Program name	: messageBox.cpp																			|
| Creator name	: Mike Girard																				|
| Creation date	: 28-11-2014																				|
| Description	: Definition's of messageBox methods														|
\***********************************************************************************************************/

// Preprocessor directives
#pragma once
#include "messageBox.h"

// Builder
messageBox::messageBox(char *title, char *inputText)
{
	string text = inputText;					// Text to put in the box

	_title = title;

	_font.loadFromFile("arial.ttf");

	_text.setCharacterSize(18);
	_text.setFont(_font);
	_text.setPosition(10, 0);
	_text.setColor(sf::Color(0, 0, 0));

	_text.setString(text);

}

// Initialise the title and the text
void messageBox::init(char *title, char *inputText)
{
	string text = inputText;					// Text to put in the box

	_title = title;
	_text.setString(text);
}

// Show the messageBox on the screen
void messageBox::show()
{
	RenderWindow errorWindow(sf::VideoMode(350, 80), _title, sf::Style::Close);// The messageBox (window)
	Event even;									// Make it able to close the window

	errorWindow.clear(Color(255, 255, 255));

	errorWindow.draw(_text);

	errorWindow.display();

	while (errorWindow.waitEvent(even)
			&& even.type != sf::Event::Closed);	// Wait for window's closing
}

// Destroyer
messageBox::~messageBox()
{
}