/************************************************************************************************************\
| Nom de la librairie: fantome2.h																			 |
| Date de cr�ation: 30-04-2014																				 |
| Nom du cr�ateur: Mike Girard																				 |
| Description: Objet fantome2																				 |
\************************************************************************************************************/

// Directive au pr�-processeur
#pragma once
#include <SFML/Graphics.hpp>
#include "fantome.h"
#include <assert.h>

// Objet fantome2
class fantome2 : public fantome
{
	public:
		fantome2();
		~fantome2();

		void reinit();

		void choixDirection(int, int, bool, bool, bool, bool);
};