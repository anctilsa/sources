/************************************************************************************************************\
| Nom de la librairie: fantome1.h																			 |
| Date de cr�ation: 30-04-2014																				 |
| Nom du cr�ateur: Mike Girard																				 |
| Description: Objet fantome1																				 |
\************************************************************************************************************/

// Directive au pr�-processeur
#pragma once
#include <SFML/Graphics.hpp>
#include "fantome.h"
#include <assert.h>

// Objet fantome1
class fantome1 : public fantome
{
	public:
		fantome1();
		~fantome1();

		void reinit();

		void choixDirection(int, int, bool, bool, bool, bool);
};