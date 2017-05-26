/************************************************************************************************************\
| Nom de la définition: pillule.cpp																			 |
| Date de création: 29-04-2014																				 |
| Nom du créateur: Samuel Anctil																			 |
| Description: Définition de l'objet pillule																 |
\************************************************************************************************************/

// Directive au pré-processeur
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