/************************************************************************************************************\
| Nom de la bibliotheque: comestible.h																	     |
| Date de cr�ation: 30-04-2014																				 |
| Nom du cr�ateur: Samuel Anctil																			 |
| Description: Objet comestible																				 |
\************************************************************************************************************/

// Directive au pr�-processeur
#pragma once
#include <SFML/Graphics.hpp>

using namespace sf;

// Objet comestible
class comestible
{
	private:
		int _posX,								// Position x
			_posY,								// Position y
			_largeur,							// Largeur
			_hauteur,							// Hauteur
			_dep;								// Num�ro de l'image

		bool _pasMange;							// Vrai si l'image est visible

		Texture _texture;						// Texture (charset)

		Sprite _image;							// Image

	public:
		comestible();							// Constructeur
		~comestible();							// Destructeur

		void setPosX(int);						// Modifie la position X
		void setPosY(int);						// Modifie la position Y
		void setPos(int, int);					// Modifie les positions X et Y
		void setLargeur(int);					// Modifie la largeur
		void setHauteur(int);					// Modifie la hauteur
		void setDep(int);						// Modifie le num�ro d'image
		void setPasMange(bool);					// Modifie l'�tat de visibilit� de l'objet comestible
		void setPosition();						// Fait la modification de la position de l'image
		void setCharset(Texture);				// Modifie le charset

		int getPosX() const;					// Renvoie la position X
		int getPosY() const;					// Renvoie la position Y
		int getLargeur() const;					// Renvoie la largeur
		int getHauteur() const;					// Renvoie la hauteur
		int getDep() const;						// Renvoie le num�ro d'image
		bool getPasMange() const;				// Renvois l'�tat de visibilit� de l'objet comestible

		Texture getCharset() const;				// Renvoie le charset
		Sprite getImage();						// Renvoie l'image
};