/************************************************************************************************************\
| Nom de la bibliotheque: pillule.h																		     |
| Date de cr�ation: 18-05-2014																				 |
| Nom du cr�ateur: Samuel Anctil																			 |
| Description: Objet pillule																				 |
\************************************************************************************************************/

// Directive au pr�-processeur
#pragma once
#include "comestible.h"

using namespace sf;

// Objet fruit
class pilule : public comestible
{
	private:
		int _nbPoint;							// Nombre de point

	public:
		pilule();								// Constructeur
		~pilule();								// Destructeur

		void setPoint(int);						// Modifie le nombre de point

		int getPoint() const;					// Renvoie le nombre de point
};