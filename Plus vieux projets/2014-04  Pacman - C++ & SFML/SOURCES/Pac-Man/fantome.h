/************************************************************************************************************\
| Nom de la librairie: fantome.h																			 |
| Date de création: 29-04-2014																				 |
| Nom du créateur: Mike Girard																				 |
| Description: Objet fantome																				 |
\************************************************************************************************************/

// Directive au pré-processeur
#pragma once
#include <SFML/Graphics.hpp>
#include "bonhomme.h"

using namespace sf;

// Objet bonhomme
class fantome : public bonhomme
{
	private:
		Texture _charset[4][4];

		int _statut;

	public:
		fantome();								// Constructeur
		~fantome();								// Destructeur

		void init();							// Initialise le fantôme
		virtual void reinit() = 0;				// Réinitialise lors de la mort de pac-man

		void setStatut(int);					// Saisi le statut
		
		int getStatut();						// Renvoie le statut

		void verifiePosition();					// Vérifie si le fantôme est au centre ou non
	    void meurt();							// La fantome meurt
	    void lvlUP() const;						// Augmentation de la vitesse
	    void vulerable();						// Pac-Man mange une grosse pastille
	    void finVulnerable();					// Début de la fin de l'effet de pastille
		void ressucite();						// Le fantome renait
		
		//virtual void plusVulnerable();			// Fin de l'effet de pastille
		virtual void choixDirection(int, int, bool, bool, bool, bool) = 0;// Vérifie si le fantôme est au centre ou non
};