/************************************************************************************************************\
| Nom de la librairie: fantome4.h																			 |
| Date de création: 30-04-2014																				 |
| Nom du créateur: Mike Girard																				 |
| Description: Objet fantome4																				 |
\************************************************************************************************************/

// Directive au pré-processeur
#pragma once
#include <SFML/Graphics.hpp>
#include "fantome.h"
#include <assert.h>

// Objet fantome4
class fantome4 : public fantome
{
	public:
		fantome4();
		~fantome4();

		void reinit();

		void choixDirection(int, int, bool, bool, bool, bool);
};