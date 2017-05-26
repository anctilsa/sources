/************************************************************************************************************\
| Nom du programme : Pac-Man.cpp																			 |
| Date de création : 29-04-2014																				 |
| Nom des créateurs: Samuel Anctil et Mike Girard															 |
| Description du programme: Jeu de pacman : Petit bonhomme jaune qui se promène en mangeant des pilules et	 |
|							qui se fait poursuivre par des fantômes qui veulent le tuer. Il peut aussi manger|
|							des pilules plus grosses qui lui permettent de manger les fantômes. Le but du jeu|
|							est de manger toutes les pilules de chaque niveau malgré l’augmentation de la	 |
|							vitesse des fantômes.															 |
|																											 |
|Statut du programme: Affichage des statistiques incomplet.													 |
|					  Vitesse des fantômes vulnérable à revoir.												 |
|					  Changement des fantômes lorsque pacman mange une pastille.							 |
\************************************************************************************************************/

// Directives pré-processeur
#include <SFML/Graphics.hpp>
#include <SFML/Audio.hpp>
#include <fstream>
#include <string>
#include <sstream>
#include "Pacman.h"
#include "Fantome1.h"
#include "Fantome2.h"
#include "Fantome3.h"
#include "Fantome4.h"
#include "Fruit.h"
#include "pastille.h"
#include "pilule.h"

using namespace std;
using namespace sf;

// Variable globale
int	LARGEURFENETRE = 600,
	HAUTEURFENETRE = 690;

// Déclaration des structures
#pragma region structures

struct stats
{
	int meilleurPointage,						// Meilleure pointage depuis la première partie
		nbPartie,								// Nombre de parties cumulées
		level,									// Niveau de difficulté des fantômes
		nbPastilles,							// Nombre de pastilles sur la carte
		nbFantomesMangees;						// Nombre de fantômes mangées
};

struct textesStats
{
	Text txtTitreStats,							// Titre du menu des statistiques
		 numero[10],							// Nombre identifiant le classement d'un pointage
		 nbPoint[10],							// Nombre de points total d'une ancienne partie
		 nbPartie[2];							// Nombre de partie jouées
};

struct textesJeu
{
	Text meilleurPointage[2],					// Meilleur pointage
		 pointage[2];							// Pointage actuel
};

struct texturesJeu
{
	Texture carte,								// Carte du jeu
			vie[3],								// Vie
			fruits[4];							// Fruits
};

struct imagesJeu
{
	Sprite carte,								// Image de la carte du jeu
		   vie[3],								// Images des vies
		   fruits[4];							// Images des fruits
};

#pragma endregion

// Déclaration des fonctions
bool afficherMenu(RenderWindow &, textesStats);
void initImagesmenu(Sprite [], Texture []);
void initImageTexte(ifstream &, texturesJeu &, imagesJeu &, textesJeu &, Font &);
void rafraichirMenu(RenderWindow &, Sprite [], int);
void afficherStats(RenderWindow &, textesStats);
void initTexteStats(ifstream &, textesStats &, Font &);
void rafraichirStats(RenderWindow &, textesStats &, Font &);
void initRectangle(RectangleShape []);
void initPastillesPilules(pastille [], pilule []);
void initLignePastilles(pastille [], int &, int, int, int, int);
void rafraichirCarteJeu(RenderWindow &, pacman &, fantome *[], fruit &, pastille [], pilule [], imagesJeu &, textesJeu &, Font &, stats &);
void debut();
string convertirInt(int);
void collision(pacman &, fantome *[], fruit &, pastille [], pilule [], stats &, RectangleShape []);
bool collisionMurHaut(bonhomme &, RectangleShape []);
bool collisionMurDroite(bonhomme &, RectangleShape []);
bool collisionMurBas(bonhomme &, RectangleShape []);
bool collisionMurGauche(bonhomme &, RectangleShape []);
void reinit(pacman &, fantome *[]);
void reinitPartie(pacman &, fantome *[], fruit &, pastille [], pilule [], stats &, RectangleShape []);

// Programme principal
int main()
{
	// Déclaration des variables
	RenderWindow ecran(VideoMode(0, 0), "");	// L'écran principal
	ecran.create(VideoMode(LARGEURFENETRE, HAUTEURFENETRE), "", Style::Titlebar);
	ecran.setMouseCursorVisible(false);

	pacman Pacman;								// Instanciation de pacman
	fantome *Fantome[4];						// Instanciation des fantomes
	Fantome[0] = new fantome1;					// Instanciation du fantome 1
	Fantome[1] = new fantome2;					// Instanciation du fantome 2
	Fantome[2] = new fantome3;					// Instanciation du fantome 3
	Fantome[3] = new fantome4;					// Instanciation du fantome 4
	fruit Fruit;								// Instanciation du fruit
	pastille Pastilles[324];					// Instanciation des pastilles
	pilule Pilules[4];							// Instanciation des Pilules
	
	RectangleShape mur[47];						// Intanciation des murs

	texturesJeu textures;						// Textures pour l'affichage dans le jeu
	imagesJeu images;							// Images pour l'affichage dans le jeu
	textesJeu textes;							// Textes pour l'affichage dans le jeu
	textesStats txtStats;						// Textes pour l'affichage des statistiques
	stats statistique = {0, 0, 1, 324, 0};		// Les statistiques
	Font font;									// Police du texte

	ifstream entree("stats.txt");				// Contient les statistiques

	Clock clock;								// Chronomètre

	Time rafraichit;							// Temps pour rafrachissement
	
	bool continuer;								// Définie si le programme s'arrête

	Event event;								// Événement
		 
	font.loadFromFile("namco__.ttf");
		 
	continuer = afficherMenu(ecran, txtStats);

	if (continuer)								// Si continuer vaut vrai
	{
		initImageTexte(entree, textures, images, textes, font);
		initRectangle(mur);
		initPastillesPilules(Pastilles, Pilules);
		rafraichirCarteJeu(ecran, Pacman, Fantome, Fruit, Pastilles, Pilules, images, textes, font, statistique);
		debut();
	}

	// Boucle principale
    while (continuer)							// Tant que la personne veut continuer
    {
		rafraichirCarteJeu(ecran, Pacman, Fantome, Fruit, Pastilles, Pilules, images, textes, font, statistique);

		clock.restart();

		while(rafraichit.asMilliseconds() <= 25)	// Attend 25 millisecondes
			rafraichit = clock.getElapsedTime();

		rafraichit = clock.restart();

		if(Pacman.getVivant())						// Si pacman est vivant
		{
			while(ecran.pollEvent(event))			// Le jeu tourne jusqu'à la mort de Pac-Man ou une victoire
			{
				switch(event.type)					// Fait la sélection du type d'événement
				{
					case Event::KeyPressed:			// Appuie sur une touche
						switch(event.key.code)		// Fait la sélection du code de touche
						{
							case Keyboard::Escape:	// Appuie sur la touche "échap"
								continuer = afficherMenu(ecran, txtStats);
							break;

							case Keyboard::Up:		// Appuie sur la touche directionnelle haut
								Pacman.setKeyMemory(0);
							break;

							case Keyboard::Right:	// Appuie sur la touche directionnelle droite
								Pacman.setKeyMemory(1);
							break;

							case Keyboard::Down:	// Appuie sur la touche directionnelle bas
								Pacman.setKeyMemory(2);
							break;

							case Keyboard::Left:	// Appuie sur la touche directionnelle gauche
								Pacman.setKeyMemory(3);
							break;

							case Keyboard::F5:
								reinitPartie(Pacman, Fantome, Fruit, Pastilles, Pilules, statistique, mur);
							break;
						}

					break;
				}
			}
			
			for(int i = 0; i < 4; i++)                  // Pour tous les fantômes
			{
				
				Fantome[i]->verifiePosition();

				if(Fantome[i]->getStatut() != 2)        // Si le statut du fantôme est 2
					Fantome[i]->choixDirection(Pacman.getPosX(), Pacman.getPosY(),
										       collisionMurHaut(*Fantome[i], mur),
											   collisionMurDroite(*Fantome[i], mur),
											   collisionMurBas(*Fantome[i], mur),
											   collisionMurGauche(*Fantome[i], mur));
				else
					Fantome[i]->choixDirection(285, 255,
											   collisionMurHaut(*Fantome[i], mur),
											   collisionMurDroite(*Fantome[i], mur),
											   collisionMurBas(*Fantome[i], mur),
											   collisionMurGauche(*Fantome[i], mur));

				Fantome[i]->avance();
			}

			collision(Pacman, Fantome, Fruit, Pastilles, Pilules, statistique, mur);

			if(Pacman.getPointage() == 750)             // Si le pointage est 750
				Fruit.setPasMange(true);
		}

		else
			if(Pacman.getDep() == Pacman.getNbDep())	// Si la position du sprite est égale
			{
				reinit(Pacman, Fantome);

				clock.restart();
				
				while(rafraichit.asMilliseconds() <= 1000)	// Attend 1000 millisecondes
					rafraichit = clock.getElapsedTime();
				
				if(Pacman.getNbVie() < 1)		// Si le nombre de vie est plus grand que 1
				{
					reinitPartie(Pacman, Fantome, Fruit, Pastilles, Pilules, statistique, mur);

					continuer = 0;

					continuer = afficherMenu(ecran, txtStats);

					if(continuer == true)		// Si continuer vaut vrai
					{
						rafraichirCarteJeu(ecran, Pacman, Fantome, Fruit, Pastilles, Pilules, images,
											textes, font, statistique);
						debut();
					}
				}
			}


		Pacman.changeDir(collisionMurHaut(Pacman, mur), 
						 collisionMurDroite(Pacman, mur),
						 collisionMurBas(Pacman, mur), 
						 collisionMurGauche(Pacman, mur));

		Pacman.avance();
    }

    return 0;
}

// Afichage du menu
bool afficherMenu(RenderWindow &ecran, textesStats txtStats)
{
	bool poursuivre = true,						// Définie si on sort d'une boucle
		 choix;
		 
	Texture textureBouton[5];					// Texture des boutons

	Sprite bouton[5];							// Les boutons du menu
	
	int posBouton = 1;

	Event event;								// Événement

	initImagesmenu(bouton, textureBouton);
	rafraichirMenu(ecran, bouton, posBouton);

	do
	{
		if(ecran.waitEvent(event))				// Attend qu'un "événement" se produise
		{
			switch(event.type)					// Fait la sélection du type d'événement
			{
				case Event::KeyPressed:			// Appuie sur une touche
					switch(event.key.code)
					{
						case Keyboard::Down:	// Appuie sur la flèche directionnelle bas
							if(posBouton < 3)	// Si 3
								posBouton ++;
							else
								posBouton = 1;

							rafraichirMenu(ecran, bouton, posBouton);
						break;

						case Keyboard::Up:		// Appuie sur la flèche directionnelle haut
							if(posBouton > 1)	// Si 1
								posBouton --;
							else
								posBouton = 3;

							rafraichirMenu(ecran, bouton, posBouton);
						break;

						case Keyboard::Return:	// Appui sur la touche "entrée"
							if(bouton[4].getPosition()==bouton[1].getPosition())// Vérifie si le bouton
							{									// de sélection est vis-à-vis le premier
								poursuivre = false;
								choix = true;
							}

							else if(bouton[4].getPosition()==bouton[2].getPosition())// Vérifie si le bouton
							{									// de sélection est vis-à-vis le deuxième
								afficherStats(ecran, txtStats);	
								rafraichirMenu(ecran, bouton, posBouton);
							}

							else if(bouton[4].getPosition()==bouton[3].getPosition())// Vérifie si le bouton
								choix = poursuivre = false;		// de sélection est vis-à-vis le troisième
						break;
					}

				break;
			}
		}

	} while (poursuivre);						// Tourne tant qu'un choix n'est pas fait pour quitter le menu

	return choix;
}

// Initialise les images du menu
void initImagesmenu(Sprite button[5], Texture texture[5])
{
	int largeurButton = 500,					// Largeur des boutons
		hauteurButton = 120;					// Hauteur des boutons

	for (int i = 0; i < 5; i++)					// Charge les 5 images
	{
		switch(i)								// Selon le numéro d'image
		{
			case 0:
				texture[i].loadFromFile("logo.png");
			break;

			case 1:
				texture[i].loadFromFile("bouton1.png");
			break;

			case 2:
				texture[i].loadFromFile("bouton2.png");
			break;

			case 3:
				texture[i].loadFromFile("bouton3.png");
			break;

			case 4:
				texture[i].loadFromFile("choix.png");
				button[4].setPosition(button[1].getPosition());
			break;
		}

		button[i].setTexture(texture[i]);

		if(i > 0 && i < 4)						// Si le numéro d'image est plus grand que 0 ou plus petit que 4
			button[i].setPosition((LARGEURFENETRE / 2)-(largeurButton / 2),
								 ((i+1) * hauteurButton) + ((i+1) * 10));
	}
}

// Initialise les images et les textes affichés dans le jeu
void initImageTexte(ifstream &entree, texturesJeu &textures, imagesJeu &images, textesJeu &textes, Font &font)
{
	// Initialisation des images
	// Carte
	textures.carte.loadFromFile("carte.bmp");
	images.carte.setPosition(0, 0);
	images.carte.setTexture(textures.carte);

	// Vies
	for (int i = 0; i < 3; i ++)				// Pour tous les vies
	{
		textures.vie[i].loadFromFile("vie.png");
		images.vie[i].setPosition(30 + (i * 30), 662);
		images.vie[i].setTexture(textures.vie[i]);
	}

	// Fruits
	textures.fruits[0].loadFromFile("cerise.png");
	textures.fruits[1].loadFromFile("fraise.png");
	textures.fruits[2].loadFromFile("orange.png");
	textures.fruits[3].loadFromFile("pomme.png");

	for (int i = 0; i < 4; i ++)				// Pour tous les fruits
	{
		images.fruits[i].setPosition(540 - ((i) * 30), 662);
		images.fruits[i].setTexture(textures.fruits[i]);
	}

	// Initialisation des textes
	// Texte "record"
	textes.meilleurPointage[0].setString("record");
	textes.meilleurPointage[0].setPosition(45, 5);
	textes.meilleurPointage[0].setColor(Color::White);
	textes.meilleurPointage[0].setCharacterSize(15);
	textes.meilleurPointage[0].setFont(font);

	// Meilleur pointage
	string meilleurPointage;					// Meilleur pointage
	entree >> meilleurPointage;

	textes.meilleurPointage[1].setString(meilleurPointage);
	textes.meilleurPointage[1].setPosition(185, 10);
	textes.meilleurPointage[1].setColor(Color::White);
	textes.meilleurPointage[1].setCharacterSize(10);
	textes.meilleurPointage[1].setFont(font);

	// Texte "score"
	textes.pointage[0].setString("score");
	textes.pointage[0].setPosition(315, 5);
	textes.pointage[0].setColor(Color::White);
	textes.pointage[0].setCharacterSize(15);
	textes.pointage[0].setFont(font);

	// Pointage actuel
	textes.pointage[1].setString("0");
	textes.pointage[1].setPosition(435, 10);
	textes.pointage[1].setColor(Color::White);
	textes.pointage[1].setCharacterSize(10);
	textes.pointage[1].setFont(font);
}

// Fait le rafraichissement du menu
void rafraichirMenu(RenderWindow &ecran, Sprite piton[5], int place)
{
	ecran.clear();

	piton[4].setPosition(piton[place].getPosition());

	for (int i = 0; i < 5; i++)					// Pour tous les boutons
		ecran.draw(piton[i]);

	ecran.display();
}

// Affichage des statistiques
void afficherStats(RenderWindow &ecran, textesStats txtStats)
{
	ifstream entree("stats.txt");
	
	bool poursuivre = true;						// Définie si on sort d'une boucle

	int pos = 0;								// Pour la position du texte					

	Font font;									// Police du texte

	font.loadFromFile("namco__.ttf");

	initTexteStats(entree, txtStats, font);
	
	Event event;
	
	do
	{
		rafraichirStats(ecran, txtStats, font);

		if(ecran.waitEvent(event))				// Attend qu'un "événement" se produise
		{
			switch(event.type)					// Fait la sélection du type d'événement
			{
				case Event::KeyPressed:			// Appuie sur une touche
					switch(event.key.code)
					{
						case Keyboard::Return:	// Appui sur la touche "entrée"
							poursuivre = false;
						break;
					}

				break;
			}
		}

	} while (poursuivre);						// Tourne tant qu'un choix n'est pas fait pour quitter le menu
}

// Initialise le texte des statistiques
void initTexteStats(ifstream &entree, textesStats &txtStats, Font &font)
{
	string donnees;

	txtStats.txtTitreStats.setString("meilleures scores");
	txtStats.txtTitreStats.setPosition(30, 30);
	txtStats.txtTitreStats.setColor(Color(230, 220, 22));
	txtStats.txtTitreStats.setCharacterSize(26);
	txtStats.txtTitreStats.setFont(font);

	for (int i = 0; i < 10; i ++)				// Pour les classements et pointages
	{
		for (int j = 0; j < 2; j ++)			// Pour les classements et pointages
		{
			if (j == 0)							// Si j vaut 0
			{
				txtStats.numero[i].setString(convertirInt(i + 1));
				txtStats.numero[i].setPosition(30, 60 + (50 * (i + 1)));
				txtStats.numero[i].setColor(Color::White);
				txtStats.numero[i].setCharacterSize(20);
				txtStats.numero[i].setFont(font);
			}

			if (j == 1)							// Si j vaut 1
			{
				entree >> donnees;

				txtStats.nbPoint[i].setString(donnees);
				txtStats.nbPoint[i].setPosition(300, 60 + (50 * (i + 1)));
				txtStats.nbPoint[i].setColor(Color::White);
				txtStats.nbPoint[i].setCharacterSize(20);
				txtStats.nbPoint[i].setFont(font);
			}
		}
	}

	txtStats.nbPartie[0].setString("nb parties");
	txtStats.nbPartie[0].setPosition(30, 620);
	txtStats.nbPartie[0].setColor(Color(230, 220, 22));
	txtStats.nbPartie[0].setCharacterSize(20);
	txtStats.nbPartie[0].setFont(font);

	entree >> donnees;

	txtStats.nbPartie[1].setString(donnees);
	txtStats.nbPartie[1].setPosition(300, 620);
	txtStats.nbPartie[1].setColor(Color::White);
	txtStats.nbPartie[1].setCharacterSize(20);
	txtStats.nbPartie[1].setFont(font);
}

// Fait le rafraichissement de l'écran des stats
void rafraichirStats(RenderWindow &ecran, textesStats &txtStats, Font &font)
{
	ecran.clear();

	ecran.draw(txtStats.txtTitreStats);

	for (int i = 0; i < 10; i ++)				// Pour les classements et pointages
	{
		ecran.draw(txtStats.numero[i]);
		ecran.draw(txtStats.nbPoint[i]);
	}

	ecran.draw(txtStats.nbPartie[0]);
	ecran.draw(txtStats.nbPartie[1]);

	ecran.display();
}

// Initialise les "murs" qui bloquent les "bonhommes", ceux qui téléporte et celui qui bloque le centre à pacman
void initRectangle(RectangleShape mur[])
{
	// Mur
	mur[0].setPosition(30, 30);
	mur[0].setScale(540, 15);

	mur[1].setPosition(285, 30);
	mur[1].setScale(30, 105);

	mur[2].setPosition(555, 30);
	mur[2].setScale(15, 210);
	
	mur[3].setPosition(30, 30);
	mur[3].setScale(15, 210);
	
	mur[4].setPosition(75, 75);
	mur[4].setScale(60, 60);

	mur[5].setPosition(165, 75);
	mur[5].setScale(90, 60);

	mur[6].setPosition(345, 75);
	mur[6].setScale(90, 60);

	mur[7].setPosition(465, 75);
	mur[7].setScale(60, 60);

	mur[8].setPosition(75, 165);
	mur[8].setScale(60, 30);

	mur[9].setPosition(165, 165);
	mur[9].setScale(30, 150);

	mur[10].setPosition(225, 165);
	mur[10].setScale(150, 30);

	mur[11].setPosition(285, 165);
	mur[11].setScale(30, 90);

	mur[12].setPosition(405, 165);
	mur[12].setScale(30, 150);

	mur[13].setPosition(465, 165);
	mur[13].setScale(60, 30);

	mur[14].setPosition(0, 225);
	mur[14].setScale(135, 90);

	mur[15].setPosition(165, 225);
	mur[15].setScale(90, 30);

	mur[16].setPosition(345, 225);
	mur[16].setScale(90, 30);

	mur[17].setPosition(465, 225);
	mur[17].setScale(105, 90);

	mur[18].setPosition(225, 285);
	mur[18].setScale(60, 30);

	mur[19].setPosition(225, 285);
	mur[19].setScale(30, 90);

	mur[20].setPosition(315, 285);
	mur[20].setScale(60, 30);

	mur[21].setPosition(345, 285);
	mur[21].setScale(30, 90);

	mur[22].setPosition(0, 345);
	mur[22].setScale(135, 90);

	mur[23].setPosition(165, 345);
	mur[23].setScale(30, 90);

	mur[24].setPosition(225, 345);
	mur[24].setScale(150, 30);

	mur[25].setPosition(405, 345);
	mur[25].setScale(30, 90);

	mur[26].setPosition(465, 345);
	mur[26].setScale(105, 90);

	mur[27].setPosition(225, 405);
	mur[27].setScale(150, 30);

	mur[28].setPosition(285, 405);
	mur[28].setScale(30, 90);

	mur[29].setPosition(30, 420);
	mur[29].setScale(15, 240);

	mur[30].setPosition(555, 420);
	mur[30].setScale(15, 240);

	mur[31].setPosition(75, 465);
	mur[31].setScale(60, 30);

	mur[32].setPosition(105, 465);
	mur[32].setScale(30, 90);

	mur[33].setPosition(165, 465);
	mur[33].setScale(90, 30);

	mur[34].setPosition(345, 465);
	mur[34].setScale(90, 30);

	mur[35].setPosition(465, 465);
	mur[35].setScale(60, 30);

	mur[36].setPosition(465, 465);
	mur[36].setScale(30, 90);

	mur[37].setPosition(30, 525);
	mur[37].setScale(45, 30);

	mur[38].setPosition(165, 525);
	mur[38].setScale(30, 90);

	mur[39].setPosition(225, 525);
	mur[39].setScale(150, 30);

	mur[40].setPosition(285, 525);
	mur[40].setScale(30, 90);

	mur[41].setPosition(405, 525);
	mur[41].setScale(30, 90);

	mur[42].setPosition(525, 525);
	mur[42].setScale(45, 30);

	mur[43].setPosition(75, 585);
	mur[43].setScale(180, 30);

	mur[44].setPosition(345, 585);
	mur[44].setScale(180, 30);

	mur[45].setPosition(30, 645);
	mur[45].setScale(540, 15);

	// Mur central
	mur[46].setPosition(225, 285);
	mur[46].setScale(150, 90);
}

// Initialise les pastilles
void initPastillesPilules(pastille Pastilles[], pilule Pilules[])
{
	int numeroPastille = 0;

	initLignePastilles(Pastilles, numeroPastille, 45, 45, 15, 1);
	initLignePastilles(Pastilles, numeroPastille, 315, 45, 15, 1);

	initLignePastilles(Pastilles, numeroPastille, 45, 60, 1, 2);
	initLignePastilles(Pastilles, numeroPastille, 45, 105, 1, 2);
	initLignePastilles(Pastilles, numeroPastille, 135, 60, 1, 5);
	initLignePastilles(Pastilles, numeroPastille, 255, 60, 1, 5);
	initLignePastilles(Pastilles, numeroPastille, 315, 60, 1, 5);
	initLignePastilles(Pastilles, numeroPastille, 435, 60, 1, 5);
	initLignePastilles(Pastilles, numeroPastille, 525, 60, 1, 2);
	initLignePastilles(Pastilles, numeroPastille, 525, 105, 1, 2);

	initLignePastilles(Pastilles, numeroPastille, 45, 135, 33, 1);

	initLignePastilles(Pastilles, numeroPastille, 45, 150, 1, 3);
	initLignePastilles(Pastilles, numeroPastille, 135, 150, 1, 3);
	initLignePastilles(Pastilles, numeroPastille, 195, 150, 1, 3);
	initLignePastilles(Pastilles, numeroPastille, 375, 150, 1, 3);
	initLignePastilles(Pastilles, numeroPastille, 435, 150, 1, 3);
	initLignePastilles(Pastilles, numeroPastille, 525, 150, 1, 3);

	initLignePastilles(Pastilles, numeroPastille, 45, 195, 7, 1);
	initLignePastilles(Pastilles, numeroPastille, 195, 195, 5, 1);
	initLignePastilles(Pastilles, numeroPastille, 315, 195, 5, 1);
	initLignePastilles(Pastilles, numeroPastille, 435, 195, 7, 1);

	initLignePastilles(Pastilles, numeroPastille, 135, 210, 1, 15);
	initLignePastilles(Pastilles, numeroPastille, 435, 210, 1, 15);

	initLignePastilles(Pastilles, numeroPastille, 45, 435, 15, 1);
	initLignePastilles(Pastilles, numeroPastille, 315, 435, 15, 1);

	initLignePastilles(Pastilles, numeroPastille, 45, 450, 1, 3);
	initLignePastilles(Pastilles, numeroPastille, 135, 450, 1, 3);
	initLignePastilles(Pastilles, numeroPastille, 255, 450, 1, 3);
	initLignePastilles(Pastilles, numeroPastille, 315, 450, 1, 3);
	initLignePastilles(Pastilles, numeroPastille, 435, 450, 1, 3);
	initLignePastilles(Pastilles, numeroPastille, 525, 450, 1, 3);

	initLignePastilles(Pastilles, numeroPastille, 60, 495, 2, 1);
	initLignePastilles(Pastilles, numeroPastille, 135, 495, 21, 1);
	initLignePastilles(Pastilles, numeroPastille, 495, 495, 2, 1);

	initLignePastilles(Pastilles, numeroPastille, 75, 510, 1, 3);
	initLignePastilles(Pastilles, numeroPastille, 135, 510, 1, 3);
	initLignePastilles(Pastilles, numeroPastille, 195, 510, 1, 3);
	initLignePastilles(Pastilles, numeroPastille, 375, 510, 1, 3);
	initLignePastilles(Pastilles, numeroPastille, 435, 510, 1, 3);
	initLignePastilles(Pastilles, numeroPastille, 495, 510, 1, 3);

	initLignePastilles(Pastilles, numeroPastille, 45, 555, 7, 1);
	initLignePastilles(Pastilles, numeroPastille, 195, 555, 5, 1);
	initLignePastilles(Pastilles, numeroPastille, 315, 555, 5, 1);
	initLignePastilles(Pastilles, numeroPastille, 435, 555, 7, 1);

	initLignePastilles(Pastilles, numeroPastille, 45, 570, 1, 3);
	initLignePastilles(Pastilles, numeroPastille, 255, 570, 1, 3);
	initLignePastilles(Pastilles, numeroPastille, 315, 570, 1, 3);
	initLignePastilles(Pastilles, numeroPastille, 525, 570, 1, 3);

	initLignePastilles(Pastilles, numeroPastille, 45, 615, 33, 1);

	Pilules[0].setPos(45, 90);
	Pilules[1].setPos(525, 90);
	Pilules[2].setPos(45, 495);
	Pilules[3].setPos(525, 495);
}

// Initialise une ligne verticale ou horizontale de plusieurs pastilles
void initLignePastilles(pastille Pastilles[], int &numeroPastille, int posX, int posY, int largeurLigne, int hauteurLigne)
{
	for(int y = 0; y < hauteurLigne; y++)			// Pour la largeur d'une ligne horizontale
		for(int x = 0; x < largeurLigne; x++)		// Pour la hauteur d'une ligne verticale
		{
			Pastilles[numeroPastille].setPos(posX + (x * (Pastilles[numeroPastille].getLargeur() / 2)), posY + (y * (Pastilles[numeroPastille].getHauteur() / 2)));

			numeroPastille++;
		}
}

// Fait le rafraichissement de la table de jeu
void rafraichirCarteJeu(RenderWindow &ecran, pacman &Pacman, fantome *Fantome[4], fruit &Fruit,
						pastille Pastilles[], pilule Pilules[], imagesJeu &images, textesJeu &textes, Font &font, stats &statistique)
{
	ecran.clear();

	// Affichage des images
	ecran.draw(images.carte);

	if(Fruit.getPasMange())						// Si le fruit n'est pas visible
		ecran.draw(Fruit.getImage());

	for(int i = 0; i < 324; i++)				// Pour tous les pastilles
		if(Pastilles[i].getPasMange())			// Si la pastilles n'est pas visible
		{
			ecran.draw(Pastilles[i].getImage());
			statistique.nbPastilles--;
		}

	for(int i = 0; i < 4; i++)					// Pour tous les pilules
		if(Pilules[i].getPasMange())			// Si la pilule n'est pas mangé
			ecran.draw(Pilules[i].getImage());

	for(int i = 0; i < 4; i++)					// Pour tous les fantômes
		if(Fantome[i]->getStatut() != 0 && Pacman.getVivant())		// Si le statut du fantôme n'est pas 0 et que pacman est vivant
			ecran.draw(Fantome[i]->getImage());

	if(Pacman.getVivant() || Pacman.getDep() != Pacman.getNbDep())	// Si pacman est vivant et que la position du sprite 
		ecran.draw(Pacman.getImage());

	for(int i = 0; i < 4; i++)										// Pour tous les fantômes
		if(Fantome[i]->getStatut() == 0 && Pacman.getVivant())		// Si les statut du fantôme est 0 et que pacman est vivant
			ecran.draw(Fantome[i]->getImage());

	// Mise à jour de l'affichage des statistiques
	for(int i = 0; i < Pacman.getNbVie(); i++)	// Pour afficher les vies
		ecran.draw(images.vie[i]);

	if(statistique.nbPastilles == 0)		// Si le nombre de pastilles est 0
		statistique.level++;

	if(statistique.level < 4)				// Si le level est moins grand que 5
		for(int i = 0; i < statistique.level; i++)	// Pour afficher les fruits qui peuvent apparaitrent
			ecran.draw(images.fruits[i]);
	else
		for(int i = 0; i < 4; i++)			// Pour afficher les fruits qui peuvent apparaitrent (tous les fruits)
			ecran.draw(images.fruits[i]);

	// Affichage des textes
	ecran.draw(textes.meilleurPointage[0]);
	ecran.draw(textes.meilleurPointage[1]);
	ecran.draw(textes.pointage[0]);

	textes.pointage[1].setString(convertirInt(Pacman.getPointage()));	

	ecran.draw(textes.pointage[1]);

	ecran.display();
}

// Début pour la musique
void debut()
{
	Music musique;
	Clock temps;
	Time timer;

	musique.openFromFile("pacman_beginning.wav");
	musique.play();

	temps.restart();

	while(timer.asMilliseconds() <= 4500)	// Attend 4 secondes
		timer = temps.getElapsedTime();
}

// Converti un integer en string et renvoie le string
string convertirInt(int nombre)
{
	string resultat;
	ostringstream convert;

	convert << nombre;
	resultat = convert.str();

	return resultat;
}

// Vérifie s'il y a des collisions et avec quoi
void collision(pacman &Pacman, fantome *Fantome[4], fruit &Fruit, pastille Pastilles[], pilule Pilules[], stats &statistique,
				RectangleShape mur[])
{
	if(Pacman.getVivant())					// Si pacman n'est pas en vie
	{
		for(int i = 0; i < 4; i++)					// Pour vérifier tous les fantômes
		{
			if(Pacman.getPosX() + (Pacman.getLargeur() / 2) >= Fantome[i]->getPosX() + (Fantome[i]->getLargeur() / 2) &&
			   Pacman.getPosX() + (Pacman.getLargeur() / 2) <= Fantome[i]->getPosX() + (Fantome[i]->getLargeur() / 2) &&
			   Pacman.getPosY() + (Pacman.getHauteur() / 2) >= Fantome[i]->getPosY() + (Fantome[i]->getHauteur() / 2) &&
			   Pacman.getPosY() + (Pacman.getHauteur() / 2) <= Fantome[i]->getPosY() + (Fantome[i]->getHauteur() / 2))	// Si pacman touche le milieu d'un fantôme
				switch(Fantome[i]->getStatut())		// Selon le statut du fantôme
				{
					case 0:							// Si le fantôme est agressif
						Pacman.meurt();
						//musique.openFromFile("pacman_death.wav");
					break;

					case 1:							// Si le fantôme est peureux
						statistique.nbFantomesMangees ++;
						Pacman.mange(200 * (statistique.nbFantomesMangees));
						Fantome[i]->meurt();
						Fantome[i]->setStatut(2);	// Fantôme mort
						//musique.openFromFile("pacman_eatghost.wav");
					break;
				}
		}

		for(int i = 0; i < 324; i++)
			if(Pacman.getPosX() + (Pacman.getLargeur() / 2) >= Pastilles[i].getPosX() + (Pastilles[i].getLargeur() / 2) &&
			   Pacman.getPosX() + (Pacman.getLargeur() / 2) <= Pastilles[i].getPosX() + (Pastilles[i].getLargeur() / 2) &&
			   Pacman.getPosY() + (Pacman.getHauteur() / 2) >= Pastilles[i].getPosY() + (Pastilles[i].getHauteur() / 2) &&
			   Pacman.getPosY() + (Pacman.getHauteur() / 2) <= Pastilles[i].getPosY() + (Pastilles[i].getHauteur() / 2))		// Si pacman touche le milieu d'une pastille
			{
				if(Pastilles[i].getPasMange() == true)	// Si la pastille est visible
				{
					Pacman.mange(Pastilles[i].getPoint());
					Pastilles[i].setPasMange(false);
					//musique.openFromFile("pacman_chomp.wav");
				}
			}

		for(int i = 0; i < 4; i++)
			if(Pacman.getPosX() + (Pacman.getLargeur() / 2) >= Pilules[i].getPosX() + (Pilules[i].getLargeur() / 2) &&
			   Pacman.getPosX() + (Pacman.getLargeur() / 2) <= Pilules[i].getPosX() + (Pilules[i].getLargeur() / 2) &&
			   Pacman.getPosY() + (Pacman.getHauteur() / 2) >= Pilules[i].getPosY() + (Pilules[i].getHauteur() / 2) &&
			   Pacman.getPosY() + (Pacman.getHauteur() / 2) <= Pilules[i].getPosY() + (Pilules[i].getHauteur() / 2))		// Si pacman touche le milieu d'une pillule
			{
				if(Pilules[i].getPasMange() == true)	// Si la pillule est visible
				{
					Pacman.mange(Pilules[i].getPoint());
					Pilules[i].setPasMange(false);
					//musique.openFromFile("pacman_chomp.wav");

					for(int i = 0; i < 4; i++)			// Pour tous les fantômes
					{
						Fantome[i]->setStatut(1);
						Fantome[i]->vulerable();
					}
				}
			}

		if(Pacman.getPosX() + (Pacman.getLargeur() / 2) >= Fruit.getPosX() + (Fruit.getLargeur() / 2) &&
		   Pacman.getPosX() + (Pacman.getLargeur() / 2) <= Fruit.getPosX() + (Fruit.getLargeur() / 2) &&
		   Pacman.getPosY() + (Pacman.getHauteur() / 2) >= Fruit.getPosY() + (Fruit.getHauteur() / 2) &&
		   Pacman.getPosY() + (Pacman.getHauteur() / 2) <= Fruit.getPosY() + (Fruit.getHauteur() / 2))
		{
			Pacman.mange(Fruit.getPoint());
			Fruit.setPasMange(false);
			//musique.openFromFile("pacman_eatfruit.wav");
		}

		switch(Pacman.getDir())						// Selon la direction de pacman
		{
			case 0:									// Haut
				Pacman.setPeutBouger(!collisionMurHaut(Pacman, mur));
			break;

			case 1:									// Droite
				Pacman.setPeutBouger(!collisionMurDroite(Pacman, mur));
			break;

			case 2:									// Bas
				Pacman.setPeutBouger(!collisionMurBas(Pacman, mur));
			break;

			case 3:									// Gauche
				Pacman.setPeutBouger(!collisionMurGauche(Pacman, mur));
			break;
		}
	}
}

// Vérifie s'il y a une collision avec un mur en haut
bool collisionMurHaut(bonhomme &Bonhomme, RectangleShape mur[])
{
	for(int i = 0; i < 47; i++)
		if(Bonhomme.getPosY() == (mur[i].getPosition().y + mur[i].getScale().y) &&
		   Bonhomme.getPosX() + Bonhomme.getLargeur() - 1 >= mur[i].getPosition().x &&
		   Bonhomme.getPosX() <= mur[i].getPosition().x + mur[i].getScale().x - 1)
		return true;

	return false;
}

// Vérifie s'il y a une collision avec un mur à droite
bool collisionMurDroite(bonhomme &Bonhomme, RectangleShape mur[])
{
	for(int i = 0; i < 47; i++)
		if(mur[i].getPosition().x == (Bonhomme.getPosX() + Bonhomme.getLargeur()) &&
		   Bonhomme.getPosY() + Bonhomme.getHauteur() - 1 >= mur[i].getPosition().y &&
		   Bonhomme.getPosY() <= mur[i].getPosition().y + mur[i].getScale().y - 1)
			return true;

	return false;
}

// Vérifie s'il y a une collision avec un mur en bas
bool collisionMurBas(bonhomme &Bonhomme, RectangleShape mur[])
{
	for(int i = 0; i < 47; i++)
		if(mur[i].getPosition().y == (Bonhomme.getPosY() + Bonhomme.getHauteur()) &&
		   Bonhomme.getPosX() + Bonhomme.getLargeur() - 1 >= mur[i].getPosition().x &&
		   Bonhomme.getPosX() <= mur[i].getPosition().x + mur[i].getScale().x - 1)
			return true;

	return false;
}

// Vérifie s'il y a une collision avec un mur à gauche
bool collisionMurGauche(bonhomme &Bonhomme, RectangleShape mur[])
{
	for(int i = 0; i < 47; i++)
		if(Bonhomme.getPosX() == (mur[i].getPosition().x + mur[i].getScale().x)&&
		   Bonhomme.getPosY() + Bonhomme.getHauteur() - 1 >= mur[i].getPosition().y &&
		   Bonhomme.getPosY() <= mur[i].getPosition().y + mur[i].getScale().y - 1)
			return true;

	return false;
}

// Réinitialise pacman
void reinit(pacman &Pacman, fantome *Fantome[])
{
	Pacman.reinit();

	for(int i = 0; i < 4; i++)					// Pour tous les fantômes
		Fantome[i]->reinit();
}

// Réinitialise la partie
void reinitPartie(pacman &Pacman, fantome *Fantome[4], fruit &Fruit, pastille Pastilles[], pilule Pilules[], stats &statistiques, RectangleShape mur[])
{
	Pacman.init();
	
	for(int i = 0; i < 4; i++)					// Pour tous les fantômes
	{
		Fantome[i]->init();
		Fantome[i]->reinit();
	}

	Fruit.setPasMange(false);

	initPastillesPilules(Pastilles, Pilules);

	statistiques.nbPastilles = 0;
}