/************************************************************************************************************\
| Nom de la librairie: pacman.h																				 |
| Date de création: 29-04-2014																				 |
| Nom du créateur: Mike Girard																				 |
| Description: Objet pacman																					 |
\************************************************************************************************************/

// Directive au pré-processeur
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
		void setKeyMemory(int);					// Met la dernière direction choisie en mémoire

		bool getVivant();						// Renvoie si le pacman est vivant ou non
		int getNbVie() const;					// Renvoie le nombre de vie
		int getPointage();						// Renvoie le nombre de point
		int getKetMemory();						// Garde la dernière direction choisie en mémoire

		void mange(int);						// Mange quelque chose
		void meurt();							// Se fait manger par un fantôme
		void changeDir(bool, bool, bool, bool);	// Change de direction lorsque possible
};