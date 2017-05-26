/************************************************************************************************************\
| Nom de la librairie: fantome.h																			 |
| Date de cr�ation: 29-04-2014																				 |
| Nom du cr�ateur: Mike Girard																				 |
| Description: Objet fantome																				 |
\************************************************************************************************************/

// Directive au pr�-processeur
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

		void init();							// Initialise le fant�me
		virtual void reinit() = 0;				// R�initialise lors de la mort de pac-man

		void setStatut(int);					// Saisi le statut
		
		int getStatut();						// Renvoie le statut

		void verifiePosition();					// V�rifie si le fant�me est au centre ou non
	    void meurt();							// La fantome meurt
	    void lvlUP() const;						// Augmentation de la vitesse
	    void vulerable();						// Pac-Man mange une grosse pastille
	    void finVulnerable();					// D�but de la fin de l'effet de pastille
		void ressucite();						// Le fantome renait
		
		//virtual void plusVulnerable();			// Fin de l'effet de pastille
		virtual void choixDirection(int, int, bool, bool, bool, bool) = 0;// V�rifie si le fant�me est au centre ou non
};