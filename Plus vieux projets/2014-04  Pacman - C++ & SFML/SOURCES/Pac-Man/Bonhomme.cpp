/************************************************************************************************************\
| Nom de la définition: bonhomme.cpp																		 |
| Date de création: 29-04-2014																				 |
| Nom du créateur: Mike Girard																				 |
| Description: Définition de l'objet bonhomme																 |
\************************************************************************************************************/

// Directive au pré-processeur
#pragma once
#include "bonhomme.h"
#include <SFML/Graphics.hpp>
#include <assert.h>

// Constructeur
bonhomme::bonhomme()
{
}

// Destructeur
bonhomme::~bonhomme()
{
	_posX = 0,
	_posY = 0,
	_vitesse = 0,
	_dep = 0,
	_interDep = 0,
	_dir = 0;
}

// Saisi la position X
void bonhomme::setPosX(int posX)
{
	_posX = posX;
}

// Saisi la position Y
void bonhomme::setPosY(int posY)
{
	_posY = posY;
}

// Saisi la largeur
void bonhomme::setLargeur(int largeur)
{
	_largeur = largeur;
}

// Saisi la hauteur
void bonhomme::setHauteur(int hauteur)
{
	_hauteur = hauteur;
}

// Saisi la position du déplacement
void bonhomme::setVitesse(int vitesse)
{
	_vitesse = vitesse;
}


void bonhomme::setInterDep(int interDep)
{
	_interDep = interDep;
}

// Saisi la position du déplacement
void bonhomme::setDep(int dep)
{
	_dep = dep;
}

// Saisi la direction
void bonhomme::setDir(int dir)
{
	assert(dir >= 0 && dir <= 3);
	_dir = dir;
}

// Saisi le nombre de "déplacement"
void bonhomme::setNbDep(int nbDep)
{
	_nbDep = nbDep;
}

// Saisi le charset
void bonhomme::setCharset(Texture texture)
{
	_texture = texture;
	_image.setTexture(_texture);
}

// Fait la saisi de la position de l'image
void bonhomme::setPosition()
{
	_image.setPosition(getPosX(), getPosY());
}


void bonhomme::setPeutBouger(bool peutBouger)
{
	_peutBouger = peutBouger;
}

// Renvoie la position X
int bonhomme::getPosX() const
{
	return _posX;
}

// Renvoie la position X
int bonhomme::getPosY() const
{
	return _posY;
}

// Renvoie la largeur
int bonhomme::getLargeur() const
{
	return _largeur;
}

// Renvoie la hauteur
int bonhomme::getHauteur() const
{
	return _hauteur;
}

// Renvoie la position du déplacement
int bonhomme::getVitesse() const
{
	return _vitesse;
}

// Renvoie la position du déplacement
int bonhomme::getDep() const
{
	return _dep;
}


int bonhomme::getInterDep() const
{
	return _interDep;
}

// Renvoie la direction
int bonhomme::getDir() const
{
	return _dir;
}

// Renvoie le nombre de "déplacement"
int bonhomme::getNbDep() const
{
	return _nbDep;
}


bool bonhomme::getPeutBouger() const
{
	return _peutBouger;
}

// Renvoie le charset
Texture bonhomme::getCharset() const
{
	return _texture;
}

// Renvoie l'image
Sprite bonhomme::getImage()
{
	_image.setTextureRect(IntRect(getDep() * getLargeur(), getDir() * getHauteur(), getLargeur(), getHauteur()));
	return _image;
}

// Permet de tourner vers le haut
void bonhomme::tourneHaut()
{
	setDir(0);
}

// Permet de tourner vers la droite
void bonhomme::tourneDroite()
{
	setDir(1);
}

// Permet de tourner vers le bas
void bonhomme::tourneBas()
{
	setDir(2);
}

// Permet de tourner vers la gauche
void bonhomme::tourneGauche()
{
	setDir(3);
}

// Permet d'avancer
void bonhomme::avance()
{
	if(getPeutBouger())
		switch(getDir())
		{
			case 0:
				setPosY(getPosY() - getVitesse());

				if(getInterDep() < 4)
					setInterDep(getInterDep() + 1);
				else
				{
					if(getDep() < getNbDep())
						setDep(getDep() + 1);
					else
						setDep(0);

					setInterDep(0);
				}
			break;

			case 1:
				setPosX(getPosX() + getVitesse());

				if(getInterDep() < 4)
					setInterDep(getInterDep() + 1);
				else
				{
					if(getDep() < getNbDep())
						setDep(getDep() + 1);
					else
						setDep(0);

					setInterDep(0);
				}
			break;

			case 2:
				setPosY(getPosY() + getVitesse());

				if(getInterDep() < 4)
					setInterDep(getInterDep() + 1);
				else
				{
					if(getDep() < getNbDep())
						setDep(getDep() + 1);
					else
						setDep(0);

					setInterDep(0);
				}
			break;

			case 3:
				setPosX(getPosX() - getVitesse());

				if(getInterDep() < 4)
					setInterDep(getInterDep() + 1);
				else
				{
					if(getDep() < getNbDep())
						setDep(getDep() + 1);
					else
						setDep(0);

					setInterDep(0);
				}
			break;
		}

	teleport();

	setPosition();
}

// Permet de se téléporter
void bonhomme::teleport()
{
	if(getPosX() == -30)
		setPosX(600);
	else if(getPosX() == 600)
		setPosX(-30);
}