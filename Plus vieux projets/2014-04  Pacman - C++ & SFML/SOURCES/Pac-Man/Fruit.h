/************************************************************************************************************\
| Nom de la bibliotheque: fruit.h																		     |
| Date de cr�ation: 30-04-2014																				 |
| Nom du cr�ateur: Samuel Anctil																			 |
| Description: Objet fruit																					 |
\************************************************************************************************************/

// Directive au pr�-processeur
#pragma once
#include "comestible.h"

using namespace sf;

// Objet fruit
class fruit : public comestible
{
	private:
		int _point[4];							// Points selon le fruit

	public:
		fruit();								// Constructeur
		~fruit();								// Destructeur

		void setPoint(int []);					// Modifie les point selon le fruit

		int getPoint();							// Renvoie le nombre de point selon le fruit
};