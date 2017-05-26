/************************************************************************************************************\
| Nom de la librairie: fantome.h																			 |
| Date de cr�ation: 29-04-2014																				 |
| Nom du cr�ateur: Mike Girard																				 |
| Description:  D�finition de l'objet fantome																 |
\************************************************************************************************************/

// Directive au pr�-processeur
#pragma once
#include "fantome.h"
#include <SFML/Graphics.hpp>
#include <assert.h>

// Constructeur
fantome::fantome()
{
	setDep(0);
	setInterDep(0);
	setDir(3);
	setNbDep(3);
	setHauteur(30);
	setLargeur(30);
	setVitesse(1);
	_statut = 0;
}

// Destructeur
fantome::~fantome()
{
}


void fantome::init()
{
	setDep(0);
	setDir(3);
	setNbDep(3);
	setHauteur(30);
	setLargeur(30);
	setVitesse(1);
	setStatut(0);
}

// Saisi le statut
void fantome::setStatut(int statut)
{
	_statut = statut;
}
	
// Renvoie le statut
int fantome::getStatut()
{
	return _statut;
}

// V�rifie si le fant�me est au centre ou non
void fantome::verifiePosition()
{
	if(getPosX() >= 225 && getPosX() <=374 && getPosY() >= 285 && getPosY() <=374 && getStatut() != 2)
		setStatut(3);
	else if(getStatut() != 1 && getStatut() != 2)
		setStatut(0);
}

// La fantome meurt
void fantome::meurt()
{
	Texture texture;

	texture.loadFromFile("fantomeMortCharset.png");

	setCharset(texture);

	setStatut(2);
}

// Augmentation de la vitesse
void fantome::lvlUP() const
{
}

// Pac-Man mange une grosse pastille
void fantome::vulerable()
{
	Texture texture;

	texture.loadFromFile("fantomePeurCharset.png");

	setCharset(texture);
}

// D�but de la fin de l'effet de pastille
void fantome::finVulnerable()
{
	Texture texture;

	texture.loadFromFile("fantomePeurFinCharset.png");

	setCharset(texture);
}

void fantome::ressucite()
{
	setStatut(0);
	reinit();
	setPosX(285);
	setPosY(315);
}