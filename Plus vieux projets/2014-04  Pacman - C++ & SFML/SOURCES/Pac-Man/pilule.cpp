/************************************************************************************************************\
| Nom de la d�finition: pillule.cpp																			 |
| Date de cr�ation: 29-04-2014																				 |
| Nom du cr�ateur: Samuel Anctil																			 |
| Description: D�finition de l'objet pillule																 |
\************************************************************************************************************/

// Directive au pr�-processeur
#pragma once
#include "pilule.h"

// Constructeur
pilule::pilule()
{
	Texture texture;

	texture.loadFromFile("pilule.png");

	setPasMange(true);

	setCharset(texture);
	setPosition();

	setPoint(50);
}

// Destructeur
pilule::~pilule()
{
}

// Modifie le nombre de point
void pilule::setPoint(int nbPoint)
{
	_nbPoint = nbPoint;
}

// Renvoie le nombre de point
int pilule::getPoint() const
{
	return _nbPoint;
}