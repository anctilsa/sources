/************************************************************************************************************\
| Nom de la librairie: fantome2.cpp																			 |
| Date de cr�ation: 29-04-2014																				 |
| Nom du cr�ateur: Mike Girard																				 |
| Description: D�finition de l'objet fantome2																 |
\************************************************************************************************************/

// Directive au pr�-processeur
#pragma once
#include "fantome2.h"
#include <assert.h>

// Constructeur
fantome2::fantome2()
{
	reinit();
}

// Destructeur
fantome2::~fantome2()
{
}


void fantome2::reinit()
{
	Texture texture;

	texture.loadFromFile("fantome2Charset.png");

	setDir(1);
	setPosX(255);
	setPosY(315);
	setCharset(texture);
	setPosition();
}

// Permet de suivre PacMan
void fantome2::choixDirection(int posX, int posY, bool murHaut, bool murDroite, bool murBas, bool murGauche)
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