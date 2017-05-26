/************************************************************************************************************\
| Nom de la librairie: pacman.cpp																			 |
| Date de création: 29-04-2014																				 |
| Nom du créateur: Mike Girard																				 |
| Description: Définition de l'objet pacman																	 |
\************************************************************************************************************/

// Directive au pré-processeur
#pragma once
#include "pacman.h"
#include <SFML/Graphics.hpp>
#include <assert.h>

// Constructeur
pacman::pacman()
{
	init();
}

// Destructeur
pacman::~pacman()
{
}

// Initialise de le pac-man
void pacman::init()
{
	setNbVie(3);
	setLargeur(30);
	setHauteur(30);
	setVitesse(10);
	setPointage(0);
	reinit();
}

// Le ressucite
void pacman::reinit()
{
	Texture texture;

	texture.loadFromFile("pacmanCharset.png");

	setVivant(true);
	setPeutBouger(true);
	setPosX(285);
	setPosY(375);
	setNbDep(9);
	setDep(4);
	setInterDep(0);
	setDir(3);
	setVitesse(1);

	setCharset(texture);
	setPosition();
}

// Saisi si le pacman est vivant ou non
void pacman::setVivant(bool vivant)
{
	_vivant = vivant;
}

// Saisi le nombre de vie
void pacman::setNbVie(int nbVie)
{
	_nbVie = nbVie;
}

// Saisi le nombre de point
void pacman::setPointage(int pointage)
{
	_pointage = pointage;
}

// Met la dernière direction choisie en mémoire
void pacman::setKeyMemory(int keyMemory)
{
	_keyMemory = keyMemory;
}

// Renvoie si le pacman est vivant ou non
bool pacman::getVivant()
{
	return _vivant;
}

// Renvoie le nombre de vie
int pacman::getNbVie() const
{
	return _nbVie;
}

// Renvoie le nombre de point
int pacman::getPointage()
{
	return _pointage;
}

// Garde la dernière direction choisie en mémoire
int pacman::getKetMemory()
{
	return _keyMemory;
}

// Mange un objet
void pacman::mange(int point)
{
	_pointage += point;
}

// Se fait manger par un fantôme
void pacman::meurt()
{
	Texture texture;

	texture.loadFromFile("pacmanMortCharset.png");

	setCharset(texture);
	setPosition();

	setNbDep(18);
	setDep(0);
	setDir(0);
	setNbVie(getNbVie() -1);
	setVitesse(0);
	setVivant(false);
}

// Change de direction lorsque possible
void pacman::changeDir(bool murHaut, bool murDroite, bool murBas, bool murGauche)
{
	switch(getKetMemory())
	{
		case 0:
			if(!murHaut)
				setDir(0);
		break;

		case 1:
			if(!murDroite)
				setDir(1);
		break;

		case 2:
			if(!murBas)
				setDir(2);
		break;

		case 3:
			if(!murGauche)
				setDir(3);
		break;
	}
}