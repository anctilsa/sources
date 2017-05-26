/************************************************************************************************************\
| Nom de la librairie: fantome1.cpp																			 |
| Date de création: 29-04-2014																				 |
| Nom du créateur: Mike Girard																				 |
| Description: Définition de l'objet fantome1																 |
\************************************************************************************************************/

// Directive au pré-processeur
#pragma once
#include "fantome1.h"
#include <assert.h>
#include <math.h>

// Constructeur
fantome1::fantome1()
{
	reinit();
}

// Destructeur
fantome1::~fantome1()
{
}


void fantome1::reinit()
{
	Texture texture;

	texture.loadFromFile("fantome1Charset.png");

	setDir(3);
	setPosX(285);
	setPosY(255);
	setCharset(texture);
	setPosition();
}

// Permet de suivre PacMan
void fantome1::choixDirection(int posX, int posY, bool murHaut, bool murDroite, bool murBas, bool murGauche)
{
	float distX = getPosX() - posX,
		  distY = getPosY() - posY;

	distX = sqrt(distX * distX);
	distY = sqrt(distY * distY);

	if(getPosX() == 285 && getPosY() == 315 && getStatut() == 2)
		ressucite();

	if(getStatut() == 0 || getStatut() == 2)
	{
		if(getDir() == 0 && (!murGauche || !murDroite))
		{
			if(distX > distY)
			{
				if(getPosX() <= posX && !murDroite)
					tourneDroite();
				else if(getPosX() >= posX && !murGauche)
					tourneGauche();
				else if(getPosY() <= posY && !murBas)
					tourneBas();
				else if(getPosY() >= posY && !murHaut)
					tourneHaut();
				else
				{
					if(!murGauche)
						tourneGauche();
					else if(!murDroite)
						tourneDroite();
				}
			}

			else
			{
				if(getPosY() <= posY && !murBas)
					tourneBas();
				else if(getPosY() >= posY && !murHaut)
					tourneHaut();
				else if(getPosX() >= posX && !murGauche)
					tourneGauche();
				else if(getPosX() <= posX && !murDroite)
					tourneDroite();
				else
				{
					if(!murGauche)
						tourneGauche();
					else if(!murDroite)
						tourneDroite();
				}
			}
		}

		else if(getDir() == 1 && (!murHaut || !murBas))
		{
			if(distX > distY)
			{
				if(getPosX() <= posX && !murDroite)
					tourneDroite();
				else if(getPosX() >= posX && !murGauche)
					tourneGauche();
				else if(getPosY() <= posY && !murBas)
					tourneBas();
				else if(getPosY() >= posY && !murHaut)
					tourneHaut();
				else
				{
					if(!murHaut)
						tourneHaut();
					else if(!murBas)
						tourneBas();
				}
			}

			else
			{
				if(getPosY() <= posY && !murBas)
					tourneBas();
				else if(getPosY() >= posY && !murHaut)
					tourneHaut();
				else if(getPosX() >= posX && !murGauche)
					tourneGauche();
				else if(getPosX() <= posX && !murDroite)
					tourneDroite();
				else
				{
					if(!murHaut)
						tourneHaut();
					else if(!murBas)
						tourneBas();
				}
			}
		}

		else if(getDir() == 2 && (!murGauche || !murDroite))
		{
			if(distX > distY)
			{
				if(getPosX() <= posX && !murDroite)
					tourneDroite();
				else if(getPosX() >= posX && !murGauche)
					tourneGauche();
				else if(getPosY() <= posY && !murBas)
					tourneBas();
				else if(getPosY() >= posY && !murHaut)
					tourneHaut();
				else
				{
					if(!murGauche)
						tourneGauche();
					else if(!murDroite)
						tourneDroite();
				}
			}

			else
			{
				if(getPosY() <= posY && !murBas)
					tourneBas();
				else if(getPosY() >= posY && !murHaut)
					tourneHaut();
				else if(getPosX() >= posX && !murGauche)
					tourneGauche();
				else if(getPosX() <= posX && !murDroite)
					tourneDroite();
				else
				{
					if(!murGauche)
						tourneGauche();
					else if(!murDroite)
						tourneDroite();
				}
			}
		}

		else if(getDir() == 3 && (!murHaut || !murBas))
		{
			if(distX > distY)
			{
				if(getPosX() <= posX && !murDroite)
					tourneDroite();
				else if(getPosX() >= posX && !murGauche)
					tourneGauche();
				else if(getPosY() <= posY && !murBas)
					tourneBas();
				else if(getPosY() >= posY && !murHaut)
					tourneHaut();
				else
				{
					if(!murHaut)
						tourneHaut();
					else if(!murBas)
						tourneBas();
				}
			}

			else
			{
				if(getPosY() <= posY && !murBas)
					tourneBas();
				else if(getPosY() >= posY && !murHaut)
					tourneHaut();
				else if(getPosX() >= posX && !murGauche)
					tourneGauche();
				else if(getPosX() <= posX && !murDroite)
					tourneDroite();
				else
				{
					if(!murHaut)
						tourneHaut();
					else if(!murBas)
						tourneBas();
				}
			}
		}
	}

	else if(getStatut() == 1)
	{
		if(getDir() == 0 && (!murGauche || !murDroite))
		{
			if(distX < distY)
			{
				if(getPosX() >= posX && !murDroite)
					tourneDroite();
				else if(getPosX() <= posX && !murGauche)
					tourneGauche();
				else if(getPosY() >= posY && !murBas)
					tourneBas();
				else if(getPosY() <= posY && !murHaut)
					tourneHaut();
				else
				{
					if(!murGauche)
						tourneGauche();
					else if(!murDroite)
						tourneDroite();
				}
			}

			else
			{
				if(getPosY() >= posY && !murBas)
					tourneBas();
				else if(getPosY() <= posY && !murHaut)
					tourneHaut();
				else if(getPosX() <= posX && !murGauche)
					tourneGauche();
				else if(getPosX() >= posX && !murDroite)
					tourneDroite();
				else
				{
					if(!murGauche)
						tourneGauche();
					else if(!murDroite)
						tourneDroite();
				}
			}
		}

		else if(getDir() == 1 && (!murHaut || !murBas))
		{
			if(distX > distY)
			{
				if(getPosX() >= posX && !murDroite)
					tourneDroite();
				else if(getPosX() <= posX && !murGauche)
					tourneGauche();
				else if(getPosY() >= posY && !murBas)
					tourneBas();
				else if(getPosY() <= posY && !murHaut)
					tourneHaut();
				else
				{
					if(!murHaut)
						tourneHaut();
					else if(!murBas)
						tourneBas();
				}
			}

			else
			{
				if(getPosY() >= posY && !murBas)
					tourneBas();
				else if(getPosY() <= posY && !murHaut)
					tourneHaut();
				else if(getPosX() <= posX && !murGauche)
					tourneGauche();
				else if(getPosX() >= posX && !murDroite)
					tourneDroite();
				else
				{
					if(!murHaut)
						tourneHaut();
					else if(!murBas)
						tourneBas();
				}
			}
		}

		else if(getDir() == 2 && (!murGauche || !murDroite))
		{
			if(distX > distY)
			{
				if(getPosX() >= posX && !murDroite)
					tourneDroite();
				else if(getPosX() <= posX && !murGauche)
					tourneGauche();
				else if(getPosY() >= posY && !murBas)
					tourneBas();
				else if(getPosY() <= posY && !murHaut)
					tourneHaut();
				else
				{
					if(!murGauche)
						tourneGauche();
					else if(!murDroite)
						tourneDroite();
				}
			}

			else
			{
				if(getPosY() >= posY && !murBas)
					tourneBas();
				else if(getPosY() <= posY && !murHaut)
					tourneHaut();
				else if(getPosX() <= posX && !murGauche)
					tourneGauche();
				else if(getPosX() >= posX && !murDroite)
					tourneDroite();
				else
				{
					if(!murGauche)
						tourneGauche();
					else if(!murDroite)
						tourneDroite();
				}
			}
		}

		else if(getDir() == 3 && (!murHaut || !murBas))
		{
			if(distX > distY)
			{
				if(getPosX() >= posX && !murDroite)
					tourneDroite();
				else if(getPosX() <= posX && !murGauche)
					tourneGauche();
				else if(getPosY() >= posY && !murBas)
					tourneBas();
				else if(getPosY() <= posY && !murHaut)
					tourneHaut();
				else
				{
					if(!murHaut)
						tourneHaut();
					else if(!murBas)
						tourneBas();
				}
			}

			else
			{
				if(getPosY() >= posY && !murBas)
					tourneBas();
				else if(getPosY() <= posY && !murHaut)
					tourneHaut();
				else if(getPosX() <= posX && !murGauche)
					tourneGauche();
				else if(getPosX() >= posX && !murDroite)
					tourneDroite();
				else
				{
					if(!murHaut)
						tourneHaut();
					else if(!murBas)
						tourneBas();
				}
			}
		}
	}

	else if(getStatut() == 3)
	{
		if(getPosY() <= 255 && !murBas)
			tourneBas();
		else if(getPosY() >= 255 && !murHaut)
			tourneHaut();
		else if(getPosX() <= 285 && !murDroite)
			tourneDroite();
		else if(getPosX() >= 285 && !murGauche)
			tourneGauche();
	}

	if(getStatut() == 2 && getPosX() == posX && getPosY() == posY)
		tourneBas();

	setPosition();
}