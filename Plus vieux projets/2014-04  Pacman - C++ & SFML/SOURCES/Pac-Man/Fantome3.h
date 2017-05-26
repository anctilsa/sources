/************************************************************************************************************\
| Nom de la librairie: fantome3.h																			 |
| Date de création: 30-04-2014																				 |
| Nom du créateur: Mike Girard																				 |
| Description: Objet fantome3																				 |
\************************************************************************************************************/

// Directive au pré-processeur
#pragma once
#include <SFML/Graphics.hpp>
#include "fantome.h"
#include <assert.h>

// Objet fantome3
class fantome3 : public fantome
{
	public:
		fantome3();
		~fantome3();

		void reinit();

		void choixDirection(int, int, bool, bool, bool, bool);
};