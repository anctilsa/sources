/************************************************************************************************************\
| Nom de la définition: pastille.cpp																		 |
| Date de création: 29-04-2014																				 |
| Nom du créateur: Samuel Anctil																			 |
| Description: Définition de l'objet pastille																 |
\************************************************************************************************************/

// Directive au pré-processeur
#pragma once
#include "pastille.h"

// Constructeur
pastille::pastille()
{
	Texture texture;

	texture.loadFromFile("pastille.png");

	setPasMange(true);

	setCharset(texture);
	setPosition();

	setPoint(10);
}

// Destructeur
pastille::~pastille()
{
}

// Modifie le nombre de point
void pastille::setPoint(int nbPoint)
{
	_nbPoint = nbPoint;
}

// Renvoie le nombre de point
int pastille::getPoint() const
{
	return _nbPoint;
}