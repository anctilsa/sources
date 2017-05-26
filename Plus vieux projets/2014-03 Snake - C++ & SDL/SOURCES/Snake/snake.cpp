/* En-tęte du programme
   ====================
Programme:		snake.cpp
Auteur:			Samuel Anctil et Matthieu Bourgeois
Date:			2014-03-31
Description:	Voici l’indémodable jeu Snake. Le but est de manger le plus de fruits possibles sans 
				que le serpent se touche lui-męme ou qu’il touche un mur. Le serpent grandi quand il 
				mange un fruit et sa vitesse augmente ou diminue quand il touche un carrée bleu ou rouge.
				
				PAS TERMINÉ OU PAS FAIT: Pouvoir utiliser la fonction nouvellePartie(),
										 Création des fruits d'autres couleurs
										 Le menu 
										 Gestion des collisions; pas tous pris en compte
										 Affichage des statistiques */

// directives au pré-processeur
#include <SDL\SDL.h>
#include <iostream>
#include <windows.h> 

using namespace std;

// prototypes de fonction
void demarrageSDL();
void creationImageFond(SDL_Surface *&img, SDL_Rect &pos);
void creationCorps(SDL_Surface *img[], SDL_Rect pos[], const int W, const int H, int &lgr);
void creationFruit(SDL_Surface *&imgFruit, SDL_Rect &posFruit, int W, int H);
void creationFruitBleu(SDL_Surface *&imgFruit, SDL_Rect &posFruit, int W, int H);
void creationFruitRouge(SDL_Surface *&imgFruit, SDL_Rect &posFruit, int W, int H);
void initCharset(SDL_Rect pos[], int nbImage, int x, int y, int w, int h);
int direction(SDL_Event event, int &dirBas, int &dirDroit);
bool collisionMur(SDL_Rect pos[], int W, int H);
bool collisionSerpent(SDL_Rect pos[], int lgr);
bool collisionFruit(SDL_Rect pos[], SDL_Rect posFruit);
//void nouvellePartie(int &lgr, SDL_Surface *img[], SDL_Rect pos[], int const W, int const H, SDL_Rect &posFruit, int &dirDroit, int &dirBas, int &vitesse, int &nbFruit, int &nbPoint);

// programme principale
int main(int argc, char *argv[])
{
	// déclaration des constantes
	const int W = 32, // largeur d'une partie du serpent
			  H = 32, // hauteur d'une partie du serpent
	   		  WFENETRE = 640, // largeur de la fenètre
		      HFENETRE = 640; // hauteur de la fenètre

	// déclaration des variables
	SDL_Event event;								// événement capté par SDL_WaitEvent
	SDL_Surface *ecran = NULL; 						// pointeur écran
	SDL_Surface *imgCorps[1000] = {NULL};			// pointeur image corps
	SDL_Surface *imgFruit = NULL;					// pointeur image fruit
	//SDL_Surface *imgFruitBleu = NULL;				// pointeur image fruit bleu
	//SDL_Surface *imgFruitRouge = NULL;				// pointeur image fruit rouge
	SDL_Surface *imgFond = NULL;					// pointeur image de fond
	SDL_Rect posFond;								// position image de fond
	SDL_Rect posCorps[1000] = {0};					// position du corps du serpent à l'écran
    SDL_Rect posRecImg[4];							// position du rectangle dans l'image des tętes
	SDL_Rect posFruit;								// position du fruit a l'ecran
	SDL_Rect posFruitBleu;							// position du fruit bleu
	SDL_Rect posFruitRouge;							// position du fruit rouge

	bool colMur,									// vrai si la tête du serpent touche un mur
		 colSerpent,								// vrai si la tête du serpent entre en collision avec son corps
		 colFruit,									// vrai si la tête du serpent touche un fruit 
		 colFruitBleu,								// vrai si la tête du serpent touche un fruit bleu
	  	 colFruitRouge;								// vrai si la tête du serpent touche un fruit rouge

	/*char nomFruit[3][15] = {"fruit.bmp", 
							"fruitBleu.bmp", 
						    "fruitRouge.bmp"};		// pour les images de fruits*/
	
	int vitesse = 150,								// pour controler la vitesse du serpent (+ le nombre est haut, - il va vite)
		longueurSerpent = 0,						// longueur du serpent
		recommencer = 1,							// flag pour la fin du programme
		iContinuer = 1,								// flag pour la fin de la boucle
		dirTete = 0,								// indique l'image de la tête que défini la direction du serpent
		dirDroit = 0,								// pour la direction sur l'axe des x
		dirBas = 0,									// pour la direction sur l'axe des y
		tempsActuel = 0,							// temps actuel
		tempsPrecedent = 0,							// temps précédent
		nbFruit = 0,								// nombre de fruit récoltés
		nbPoint = 0;								// nombre de points gagnés

	demarrageSDL();

	/* créer la fenètre */
	ecran = SDL_SetVideoMode(WFENETRE, HFENETRE, 32, SDL_HWSURFACE | SDL_DOUBLEBUF);

	/* met un titre à la fenètre */
	SDL_WM_SetCaption("Snake", NULL);

	/* met une icone à la fenètre */
    SDL_WM_SetIcon(SDL_LoadBMP("sdl_icone.bmp"), NULL);
		
	creationImageFond(imgFond, posFond);
	creationCorps(imgCorps, posCorps, WFENETRE, HFENETRE, longueurSerpent);
	creationFruit(imgFruit, posFruit, WFENETRE, HFENETRE);
	initCharset(posRecImg, 4, 0, 32, 32, 32);

	/* active la répétition des touches */
	SDL_EnableKeyRepeat(10, 10);

	while(iContinuer)								// tant que iContinuer ne change pas
	{
		SDL_PollEvent(&event);

		switch(event.type)							// selon le type d’événement
		{
			case SDL_QUIT:							// X de la fenčtre
				iContinuer = 0;
				break;

			case SDL_KEYDOWN:						// tous les touches
				dirTete = direction(event, dirBas, dirDroit);
				
				switch(event.type)					// selon le type d’événement
				{
					case SDLK_ESCAPE:				// touche échape
					iContinuer = 0;
					break;
				}
		}

		tempsActuel = SDL_GetTicks();

		if(tempsActuel - tempsPrecedent > vitesse) // 100 ms	
		{
			/* gestion des collisions */
			colMur = collisionMur(posCorps, WFENETRE, HFENETRE);
			colSerpent = collisionSerpent(posCorps, longueurSerpent);
			colFruit = collisionFruit(posCorps, posFruit);

			if (colMur == true || colSerpent == true)
			{
				//nouvellePartie(longueurSerpent, imgCorps, posCorps, WFENETRE, HFENETRE, posFruit, dirDroit, dirBas, vitesse, nbFruit, nbPoint);
				break;
			}

			if (colFruit == true)
			{
				nbFruit ++;
				nbPoint += 10;
				creationCorps(imgCorps, posCorps, WFENETRE, HFENETRE, longueurSerpent);
				creationFruit(imgFruit, posFruit, WFENETRE, HFENETRE);
			}
	
			/* gestion du mouvement du serpent */
			for (int i = longueurSerpent; i > 0; i --)
			{
				posCorps[i].x = posCorps[i - 1].x;
				posCorps[i].y = posCorps[i - 1].y;
			}
				posCorps[0].x += dirDroit * W;
				posCorps[0].y += dirBas * W;

			tempsPrecedent = tempsActuel;
		}
		else 
			SDL_Delay(vitesse - (tempsActuel - tempsPrecedent));	

		/* efface l'écran */
		SDL_FillRect(ecran, NULL, SDL_MapRGB(ecran->format, 17, 206, 112));
	
		/* change l'emplacement de l'image */
		for (int i = 0; i < longueurSerpent; i ++)
			if (i == 0)
				SDL_BlitSurface(imgCorps[0], &posRecImg[dirTete], ecran, &posCorps[0]);
			else
				SDL_BlitSurface(imgCorps[i], NULL, ecran, &posCorps[i]);
		SDL_BlitSurface(imgFruit, NULL, ecran, &posFruit);

		/* met à jour l'affichage */
		SDL_Flip(ecran);
	}
		

	/* libère la mémoire */
	for (int i = 0; i < longueurSerpent; i ++)
		SDL_FreeSurface(imgCorps[i]);
	SDL_FreeSurface(imgFruit);
	//SDL_FreeSurface(imgFruitBleu);
	//SDL_FreeSurface(imgFruitRouge);

	/* ferme la SDL */
	SDL_Quit();

 	return EXIT_SUCCESS;							// valeur retourné en cas de réussite
}

// démarre la SDL
void demarrageSDL()
{
	if (SDL_Init(SDL_INIT_VIDEO) == -1)				// si la SDL ne démarre pas correctement
	{
		cout << "Erreur lors du démarrage de la SDL", SDL_GetError();
		exit(EXIT_FAILURE);							// valeur retourné en cas d'erreur
	}
}

// l'image de fond
void creationImageFond(SDL_Surface *&img, SDL_Rect &pos)
{
	img = SDL_LoadBMP("fondMenu.bmp");

	pos.x = 0;
	pos.y = 0;
}

// créer le corps du serpent
void creationCorps(SDL_Surface *img[], SDL_Rect pos[], const int W, const int H, int &lgr)
{
	if (lgr == 0)									// si la longueur du serpent est égale à 1 (seulement la tête)
	{
		img[0] = SDL_LoadBMP("tete-serpent.bmp");
		pos[0].x = W / 2;
		pos[0].y = H / 2;
	}
	else
	{
		img[lgr] = SDL_LoadBMP("corps-serpent.bmp");
		pos[lgr].x = pos[lgr - 1].x - 32;
		pos[lgr].y = pos[lgr - 1].y;
	}

	lgr ++;
}

// créer le fruit que le serpent pourra manger
void creationFruit(SDL_Surface *&imgFruit, SDL_Rect &posFruit, int W, int H)
{
	imgFruit = SDL_LoadBMP("pomme.bmp");

	posFruit.x = rand() % (W / 32) * 32 - 32;
	posFruit.y = rand() % (W / 32) * 32 - 32;
}


// initialise un charset
void initCharset(SDL_Rect posRecImg[], int nbImage, int x, int y, int w, int h)
{
	for (int i = 0; i < nbImage; i ++)				// pour les lignes
	{
		posRecImg[i].x = x;
		posRecImg[i].y = i * y;
		posRecImg[i].w = w;
		posRecImg[i].h = h;
	}
}

// renvoit la direction du serpent
int direction(SDL_Event event, int &dirBas, int &dirDroit)
{
	int static dir = 0;
		
	switch (event.key.keysym.sym)
	{	
		case SDLK_UP:								// flèche haut
			if (dirBas != 1)						// si le serpent ne va pas vers le bas
			{
				dir = 3;
				dirBas = -1;
				dirDroit= 0;
			}

			break;
							
		case SDLK_DOWN:								// flèche bas
			if (dirBas != -1)						// si le serpent ne va pas vers le haut
			{
				dir = 0;
				dirBas = 1;
				dirDroit = 0;
			}

			break;

		case SDLK_RIGHT:							// flèche droite
			if (dirDroit != -1)						// si le serpent ne va pas vers la gauche
			{
				dir = 2;
				dirBas = 0;
				dirDroit = 1;
			}

			break;

		case SDLK_LEFT:								// flèche gauche
			if (dirDroit != 1)						// si le serpent ne va pas vers la droite
			{
				dir = 1;
				dirBas = 0;
				dirDroit = -1;
			}

			break;
	}

	return dir;
}

// vérifie si la tête du serpent touche au mur
bool collisionMur(SDL_Rect pos[], int W, int H)
{
	if(pos[0].x > W - 32)							// si la tête du serpent touche au mur de droite							
		return true;
	else if(pos[0].x < 0)							// si la tête du serpent touche au mur de gauche
		return true;
	else if(pos[0].y > H - 32)						// si la tête du serpent touche au mur du bas
		return true;
	else if(pos[0].y < 0)							// si la tête du serpent touche au mur du haut	
		return true;

	return false;
}

// vérifie si la tête du serpent entre en collision avec son corps
bool collisionSerpent(SDL_Rect pos[], int lgr)
{
	for (int i = 1; i < lgr; i ++)
	{
		if (pos[0].x == pos[i].x && 
			pos[0].y == pos[i].y)					// si la tête du serpent touche le corps du serpent
			return true;
	}

	return false;
}

// vérifie si la tête du serpent touche à un fruit
bool collisionFruit(SDL_Rect pos[], SDL_Rect posFruit)
{
	if (pos[0].x == posFruit.x &&
		pos[0].y == posFruit.y)						// si la tête du serpent touche le fruit
		return true;	

	return false;
}

//// permet de créer une nouvelle partie
//void nouvellePartie(int &lgr, SDL_Surface *img[], SDL_Rect pos[], int const W, int const H, SDL_Rect &posFruit, 
//					int &dirDroit, int &dirBas, int &vitesse, int &nbFruit, int &nbPoint)
//{
//	/* efface le serpent sauf la tête */
//	for (int i = lgr; i > 0; i --)
//		img[lgr] = NULL;
//
//
//	/* change l'emplacement de la tête et du fruit */
//	pos[0].x = W / 2;
//	pos[0].y = H / 2;
//	posFruit.x = rand() % (W / 32) * 32 - 32;
//	posFruit.y = rand() % (W / 32) * 32 - 32;
//
//	/* initialisation de la direction et de la vitesse */
//	dirBas = 0;
//	dirDroit = 0;
//	vitesse = 150;
//
//	/* initialisation des statistiques */
//	nbFruit = 0;
//	nbPoint = 0;
//}