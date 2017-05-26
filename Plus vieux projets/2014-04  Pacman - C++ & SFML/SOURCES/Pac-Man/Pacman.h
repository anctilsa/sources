/************************************************************************************************************\
| Nom de la librairie: pacman.h																				 |
| Date de cr�ation: 29-04-2014																				 |
| Nom du cr�ateur: Mike Girard																				 |
| Description: Objet pacman																					 |
\************************************************************************************************************/

// Directive au pr�-processeur
#pragma once
#include <SFML/Graphics.hpp>
#include "bonhomme.h"
#include <string>

using namespace sf;
using namespace std;

// Objet bonhomme
class pacman : public bonhomme
{
	private:
	    Texture _textureMort;

		Sprite _pacmanMort;

		bool _vivant;

		int _nbVie,
			_pointage,
			_keyMemory;

		string _nom;

	public:
		pacman();								// Constructeur
		~pacman();								// Destructeur

		void init();							// Initialise de le pac-man
		void reinit();							// Le ressucite

		void setVivant(bool);					// Saisi si le pacman est vivant ou non
		void setNbVie(int);						// Saisi le nombre de vie
		void setPointage(int);					// Saisi le nombre de point
		void setKeyMemory(int);					// Met la derni�re direction choisie en m�moire

		bool getVivant();						// Renvoie si le pacman est vivant ou non
		int getNbVie() const;					// Renvoie le nombre de vie
		int getPointage();						// Renvoie le nombre de point
		int getKetMemory();						// Garde la derni�re direction choisie en m�moire

		void mange(int);						// Mange quelque chose
		void meurt();							// Se fait manger par un fant�me
		void changeDir(bool, bool, bool, bool);	// Change de direction lorsque possible
};