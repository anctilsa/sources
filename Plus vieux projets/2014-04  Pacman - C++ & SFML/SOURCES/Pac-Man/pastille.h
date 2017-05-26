/************************************************************************************************************\
| Nom de la bibliotheque: fruit.h																		     |
| Date de cr�ation: 30-04-2014																				 |
| Nom du cr�ateur: Samuel Anctil																			 |
| Description: Objet pastille																				 |
\************************************************************************************************************/

// Directive au pr�-processeur
#pragma once
#include "comestible.h"

using namespace sf;

// Objet fruit
class pastille : public comestible
{
	private:
		int _nbPoint;							// Nombre de point

	public:
		pastille();								// Constructeur
		~pastille();							// Destructeur

		void setPoint(int);						// Modifie le nombre de point

		int getPoint() const;					// Renvoie le nombre de point
};