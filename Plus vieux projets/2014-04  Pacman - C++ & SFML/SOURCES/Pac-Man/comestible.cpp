/************************************************************************************************************\
| Nom de la définition: comestible.cpp																	     |
| Date de création: 29-04-2014																				 |
| Nom du créateur: Samuel Anctil																			 |
| Description: Définition de l'objet comestible																 |
\************************************************************************************************************/

// Directive au pré-processeur
#pragma once
#include "comestible.h"

using namespace sf;

// Constructeur
comestible::comestible()
{
	_hauteur = 30;
	_largeur = 30;
	_posX = 0;
	_posY = 0;
	_dep = 0;
}

// Destructeur
comestible::~comestible()
{
}

// Modifie la position X
void comestible::setPosX(int posX)
{
	_posX = posX;

	setPosition();
}

// Modifie la position Y
void comestible::setPosY(int posY)
{
	_posY = posY;

	setPosition();
}

// Modifie les positions X et Y
void comestible::setPos(int x, int y)
{
	setPosX(x);
	setPosY(y);

	setPosition();
}

// Modifie la largeur
void comestible::setLargeur(int largeur)
{
	_largeur = largeur;
}

// Modifie la hauteur
void comestible::setHauteur(int hauteur)
{
	_hauteur = hauteur;
}

// Modifie le numéro d'image
void comestible::setDep(int dep)
{
	_dep = dep;
}

// Modifie l'état de visibilité de l'objet comestible
void comestible::setPasMange(bool pasMange)
{
	_pasMange = pasMange;
}

// Fait la modification de la position de l'image
void comestible::setPosition()
{
	_image.setPosition(getPosX(), getPosY());
}

// Modifie le charset
void comestible::setCharset(Texture texture)
{
	_texture = texture;
	_image.setTexture(_texture);
}

// Renvoie la position X
int comestible::getPosX() const
{
	return _posX;
}

// Renvoie la position Y
int comestible::getPosY() const
{
	return _posY;
}

// Renvoie la largeur
int comestible::getLargeur() const
{
	return _largeur;
}

// Renvoie la hauteur
int comestible::getHauteur() const
{
	return _hauteur;
}

// Renvoie le numéro d'image
int comestible::getDep() const
{
	return _dep;
}

// Renvois l'état de visibilité de l'objet comestible
bool comestible::getPasMange() const
{
	return _pasMange;
}

// Renvoie le charset
Texture comestible::getCharset() const
{
	return _texture;
}

// Renvoie l'image
Sprite comestible::getImage()
{
	_image.setTextureRect(IntRect(getDep() * getLargeur(), 0, getLargeur(), getHauteur()));

	return _image;
}