/************************************************************************************************************\
| Nom de la d�finition: fruit.cpp																		     |
| Date de cr�ation: 29-04-2014																				 |
| Nom du cr�ateur: Samuel Anctil																			 |
| Description: D�finition de l'objet fruit																	 |
\************************************************************************************************************/

// Directive au pr�-processeur
#pragma once
#include "fruit.h"

// Constructeur
fruit::fruit()
{
	Texture texture;

	texture.loadFromFile("fruitCharset.bmp");

	setPosX(285);
	setPosY(375);

	for(int i = 0; i < 4; i++)					// Pour tous les fruits
		_point[i] = (i * 200) + 100;

	setPasMange(false);

	setCharset(texture);
	setPosition();
}

// Destructeur
fruit::~fruit()
{
}

// Modifie les point selon le fruit
void fruit::setPoint(int point[4])
{
	for(int i = 0; i < 4; i++)					// Pour tous les fruits
		_point[i] = point[i];
}

// Renvoie le nombre de point selon le fruit
int fruit::getPoint()
{
	return _point[getDep()];
}