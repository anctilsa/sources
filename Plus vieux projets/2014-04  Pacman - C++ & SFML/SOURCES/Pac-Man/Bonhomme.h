/************************************************************************************************************\
| Nom de la librairie: bonhomme.h																			 |
| Date de création: 29-04-2014																				 |
| Nom du créateur: Mike Girard																				 |
| Description: Objet bonhomme																				 |
\************************************************************************************************************/

// Directive au pré-processeur
#pragma once
#include <SFML/Graphics.hpp>

using namespace sf;

// Objet bonhomme
class bonhomme
{
	private:
		int _posX,
			_posY,
			_largeur,
			_hauteur,
			_dep,
			_interDep,
			_dir,
			_nbDep;

		
		float _vitesse;

		bool _peutBouger;

		Texture _texture;

		Sprite _image;

	public:
		bonhomme();								// Constructeur
		~bonhomme();							// Destructeur

		void setPosX(int);						// Saisi la position X
		void setPosY(int);						// Saisi la position Y
		void setLargeur(int);					// Saisi la largeur
		void setHauteur(int);					// Saisi la hauteur
		void setVitesse(int);					// Saisi la vitesse
		void setDep(int);						// Saisi la position du déplacement
		void setInterDep(int);
		void setDir(int);						// Saisi la direction
		void setNbDep(int);						// Saisi le nombre de "déplacement"
		void setCharset(Texture);				// Saisi le charset
		void setPosition();						// Fait la saisi de la position de l'image
		void setPeutBouger(bool);

		int getPosX() const;					// Renvoie la position X
		int getPosY() const;					// Renvoie la position Y
		int getLargeur() const;					// Renvoie la largeur
		int getHauteur() const;					// Renvoie la hauteur
		int getVitesse() const;					// Renvoie la vitesse
		int getDep() const;						// Renvoie la position du déplacement
		int getInterDep() const;
		int getDir() const;						// Renvoie la direction
		int getNbDep() const;					// Renvoie le nombre de "déplacement"
		bool getPeutBouger() const;
		Texture getCharset() const;				// Renvoie le charset
		Sprite getImage();						// Renvoie l'image

		void tourneHaut();						// Permet de tourner vers le haut
		void tourneDroite();					// Permet de tourner vers la droite
		void tourneBas();						// Permet de tourner vers le bas
		void tourneGauche();					// Permet de tourner vers la gauche
		void avance();							// Permet d'avancer
		void teleport();						// Permet de la téléporter
};