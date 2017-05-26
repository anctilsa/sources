/************************************************************************************************************\
| Nom de la librairie: fantome3.cpp																			 |
| Date de création: 29-04-2014																				 |
| Nom du créateur: Mike Girard																				 |
| Description: Définition de l'objet fantome3																 |
\************************************************************************************************************/

// Directive au pré-processeur
#pragma once
#include "fantome3.h"
#include <assert.h>

// Constructeur
fantome3::fantome3()
{
	reinit();
}

// Destructeur
fantome3::~fantome3()
{
}


void fantome3::reinit()
{
	Texture texture;

	texture.loadFromFile("fantome3Charset.png");

	setDir(0);
	setPosX(285);
	setPosY(315);
	setCharset(texture);
	setPosition();
}

// Permet de suivre PacMan
void fantome3::choixDirection(int posX, int posY, bool murHaut, bool murDroite, bool murBas, bool murGauche)
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