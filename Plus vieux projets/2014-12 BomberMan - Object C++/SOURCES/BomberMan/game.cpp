/***********************************************************************************************************\
|File	: game.cpp																							|
|Autor	: Mike Girard & Samuel Anctil																		|
|Date	: 2014 - 11 - 27																					|
|Goal	: Ressource for the class game.																		|
\***********************************************************************************************************/

// Preprocessor directives
#include <cstdlib>
#include <time.h>
#include "game.h"

// Builder
game::game()
{
	_continue = _openMenu = false;

	_fps = 1 / (float)60;

	_image.loadFromFile("textures.bmp");
	_image.createMaskFromColor(Color(50, 100, 150), 0);
}

// Init the game
void game::init()
{
	_texture.loadFromImage(_image);

	_mainMenu.loadTexture("menuBackground.png", _texture);

	initMap();
	initBlock();
	initManAndBomb();
	initMonster();
}

// Init the map
void game::initMap()
{
	ifstream mapStream;
	string mapName = "map.txt";

	mapStream.open(mapName);

	_bomberMap.init(_texture, 0, 0, 40, 40);
	_bomberMap.load(mapStream);
}

// Init the blocks
void game::initBlock()
{
	_itBlock = _bomberBlock.begin();

	srand(time(nullptr));

	for (int i = 0; i < _bomberMap.getNbCol(); i++) // Set blocks in the columns
	{
		for (int j = 0; j < _bomberMap.getNbLn(); j++) // Set blocks in the lines
		{
			if ('0' == _bomberMap.getElement(i, j) &&
				(i != 1 || j != 1) &&
				(i != 1 || j != 2) &&
				(i != 2 || j != 1) &&
				rand() % 2 == 0)				// 50% chances of putting a blocks, except for pos
			{									// 1,1 - 1,2 - 2,1
				_itBlock = _bomberBlock.insert(_itBlock, block(_texture, (i * 40), (j * 40)));
				_bomberMap.setElement('2', i, j);
			}
		}
	}
}

// Init the man
void game::initManAndBomb()
{
	_man.init(_texture, IntRect(0, 160, 120, 160), 3, 4); // Bomber man
	_man.setLimit(IntRect(40, 120, 480, 400));
	_man.setPosition(40, 120);

	_man.getRect();

	_itBomb = _bomb.begin();
}

// Init the monsters
void game::initMonster()
{
	_itMonster = _monster.begin();

	for (int i = 0; i < _bomberMap.getNbCol(); i++) // Set monsters in the columns
	{
		for (int j = 0; j < _bomberMap.getNbLn(); j++) // Set monsters in the lines
		{
			if ('0' == _bomberMap.getElement(i, j) &&
				(i != 1 || j != 1) &&
				(i != 1 || j != 2) &&
				(i != 2 || j != 1) &&
				rand() % 10 == 0)				// 10% chances of putting a monster, except for pos
			{									// 1,1 - 1,2 - 2,1
				_itMonster = _monster.insert(_itMonster, monster(_texture, IntRect(0, 160, 120, 160), 100));
				_itMonster->setPosition(40 + (40 * (i - 1)), 120 + (40 * (j - 1)));
				_itMonster->setLimit(IntRect(40, 120, 480, 400));
			}
		}
	}
}

void game::play(RenderWindow &window)
{
	init();

	_continue = true;

	while (_continue)							// While continue is true
	{
		while (window.pollEvent(_event))		// While there is an event and continue is true
			catchEvent(_event);
		
		if (_openMenu)							// The menu is opened
		{
			_continue = _mainMenu.show(window);
			_openMenu = false;
		}

		_timer = _chrono.getElapsedTime();

		_itBlock = _bomberBlock.begin();

		while (_itBlock != _bomberBlock.end())	// Check all the blocks
		{
			if (_itBlock->isBlowed())			// If the block is blowed, erase it
			{
				_bomberMap.setElement('0', (_itBlock->getPosX() / _man.width()),(_itBlock->getPosY() / _man.height()) - 2);
				_itBlock = _bomberBlock.erase(_itBlock);
			}
			else
				_itBlock++;
		}

		_itBomb = _bomb.begin();

		while (_itBomb != _bomb.end())			// Check all the blocks
		{
			if (_itBomb->explode())
				bombExplose(*_itBomb);

			if (_itBomb->isBlowed())			// If the block is blowed, erase it
			{
				_itBomb = _bomb.erase(_itBomb);
				_man.bombExploded();
			}
			else
				_itBomb++;
		}

		_itMonster = _monster.begin();

		while (_itMonster != _monster.end())
		{
			if (_itMonster->dead())
				_itMonster = _monster.erase(_itMonster);
			else
				_itMonster++;
		}

		update(window);
	}
}

// Catch an event
void game::catchEvent(const Event &event)		// Catch an event
{
	switch (event.type)							// Depending of the event
	{
		case Event::Closed:						// Clic X of the window
			_continue = false;
			break;

		case Event::KeyReleased:				// Key released
			switch (event.key.code)				// Depending of the key
			{
			case Keyboard::W:					// W
				_man.stopMove();
				break;

			case Keyboard::D:					// D
				_man.stopMove();
				break;

			case Keyboard::S:					// S
				_man.stopMove();
				break;

			case Keyboard::A:					// A
				_man.stopMove();
				break;
			}
			break;

		case Event::KeyPressed:					// Key pressed
			switch (event.key.code)				// Depending of the key
			{
			case Keyboard::Escape:
				_openMenu = true;
				break;

				case Keyboard::W:				// W
					_man.enableMove();
					_man.setDirection(0);
					break;

				case Keyboard::D:				// D
					_man.enableMove();
					_man.setDirection(1);
					break;

				case Keyboard::S:				// S
					_man.enableMove();
					_man.setDirection(2);
					break;

				case Keyboard::A:				// A
					_man.enableMove();
					_man.setDirection(3);
					break;

				case Keyboard::Q:				// Q
					placeBomb();
					break;
			}
			break;
	}
}

// Update the game state
void game::update(RenderWindow &window)
{
	if (_timer.asSeconds() >= _fps)				// Limit the number of frame per seconds
	{
		window.clear(Color(255, 255, 255));

		if (_man.getNbLife() > 0)				// If bomber man still have at least a life		
		{
			window.clear(Color(255, 255, 255));

			_bomberMap.draw(window, 0, 80);

			updateBlock(window);
			updateManAndBomb(window);
			updateMonster(window);

			window.display();
		}

		_timer = _chrono.restart();
	}
}

// Update the blocks
void game::updateBlock(RenderWindow &window)
{
	_itBlock = _bomberBlock.begin();

	while (_itBlock != _bomberBlock.end())		// Draw the blocks
	{
		_itBlock->draw(window);
		_itBlock++;
	}
}

// Update the man
void game::updateManAndBomb(RenderWindow &window)
{
	_itBomb = _bomb.begin();

	while (_itBomb != _bomb.end())				// To draw each bomb
	{
		_itBomb->draw(window);
		_itBomb++;
	}

	if (_man.alive())							// The bomber man is alive
	{
		if (!_bomberMap.collision(_man.getRect(), _man.dir(), 0, 80)) // There is no collision
			_man.update();
		else
			_man.slide(canSlide(_man));
	}
	else
		_man.update();

	_man.draw(window);
}

// Update the monsters
void game::updateMonster(RenderWindow &window)
{
	//_itMonster = _monster.begin();

	//while (_itMonster != _monster.end())		// Update the monsters if there is at least one
	//{
	//	changeDirection();

	//	if(!collision(*_itMonster, 0, 80))
	//		_itMonster->move();

	//	_itMonster++;
	//}

	//_itMonster = _monster.begin();

	//while (_itMonster != _monster.end())		// To draw each monster
	//{
	//	_itMonster->draw(window);
	//	_itMonster++;
	//}
}

// Change the direction of a monster
void game::changeDirection(int nbTry)
{
	if (_itMonster->posX() % 40 == 0 && _itMonster->posY() % 40 == 0) // The monsterm is at an intersection
	{
		if (_bomberMap.collision(_itMonster->getRect(), _itMonster->dir(), 0, 80)) // There is a collision so the dir change
			_itMonster->changeDirection(); 
	}

	if (_bomberMap.collision(_itMonster->getRect(), _itMonster->dir(), 0, 80)) // If there is a colision and that there is still hope!
		changeDirection(nbTry + 1);
}

//// Check if there is a collision in front of the character
//bool game::collision(const character &charact) const
//{
//	//Vector2i size = _bomberMap.getCaseSize();	// Size of a case
//
//	//int col = (charact.posX() - posX) / size.x,	// Col
//	//	row = (charact.posY() - posY) / size.y;	// Row
//
//	//switch (charact.dir())						// Check the collisions following the direction
//	//{
//	//case 1:										// Up
//	//	if (_bomberMap.getElement(col, row - 1) == '0')
//	//		return false;
//	//	break;
//
//	//case 2:										// Right
//	//	if (_bomberMap.getElement(col + 1, row) == '0')
//	//		return false;
//	//	break;
//
//	//case 3:										// Down
//	//	if (_bomberMap.getElement(col, row + 1) == '0')
//	//		return false;
//	//	break;
//
//	//case 4:										// Left
//	//	if (_bomberMap.getElement(col - 1, row) == '0')
//	//		return false;
//	//	break;
//	//}
//
//	//return true;
//
//	int posX = charact.posX(),
//		posY = charact.posY(),
//		width = charact.width(),
//		height = charact.height();
//
//	switch (charact.dir())						// Check the collisions following the direction
//	{
//	case 0:										// Up
//		if (_bomberMap.getElement(posX / width, (((posY - 1) - (height * 2)) / height)) != '0' ||
//			_bomberMap.getElement((posX / width) + 1, (((posY - 1) - (height * 2)) / height)) != '0' &&
//			posX % width != 0)
//			return true;
//		break;
//
//	case 1:										// Left
//		if (_bomberMap.getElement((posX / width) + 1, (posY - (height * 2)) / height) != '0' ||
//			_bomberMap.getElement((posX / width) + 1, ((posY - (height * 2)) / height) + 1) != '0' &&
//			posY % height != 0)
//			return true;
//		break;
//
//	case 2:										// Down
//		if (_bomberMap.getElement(posX / width, ((posY - (height * 2)) / height) + 1) != '0' ||
//			_bomberMap.getElement((posX / width) - 1, ((posY - (height * 2)) / height) + 1) != '0'&&
//			posX % width != 0)
//			return true;
//		break;
//
//	case 3:										// Right
//		if (_bomberMap.getElement(((posX - 1) / width), (posY - (height * 2)) / height) != '0' ||
//			_bomberMap.getElement(((posX - 1) / width), ((posY - (height * 2)) / height) - 1) != '0'&&
//			posY % height != 0)
//			return true;
//		break;
//	}
//	
//	return false;
//}

// Check if the character can slide
int game::canSlide(const character &charact) const
{
	if (_bomberMap.collision(charact.getRect(), charact.dir(), 0, 80)) // If there is a collision
		return 5;

	switch (charact.dir())					// Following the direction
	{
	case 0:									// Up
		if (charact.posX() % charact.width() != 0) // No need to slide
		{
			if (charact.posX() % charact.width() <= 15) //Slide left
				return 1;
			else if (charact.posX() % charact.width() >= charact.width() - 15) //Slide right
				return 3;
		}
		break;
			
	case 1:									// Left
		if (charact.posY() % charact.height() != 0) // No need to slide
		{
			if (charact.posY() % charact.height() <= 15) //Slide up
				return 2;
			else if (charact.posY() % charact.height() >= charact.height() - 15) //Slide down
				return 0;
		}
		break;

	case 2:									// Down
		if (charact.posX() % charact.width() != 0) // No need to slide
		{
			if (charact.posX() % charact.width() <= 15) //Slide left
				return 1;
			else if (charact.posX() % charact.width() >= charact.width() - 15) //Slide right
				return 3;
		}
		break;

	case 3:									// Right
		if (charact.posY() % charact.height() != 0) // No need to slide
		{
			if (charact.posY() % charact.height() <= 15) //Slide up
				return 2;
			else if (charact.posY() % charact.height() >= charact.height() - 15) //Slide down
				return 0;
		}
		break;
	}
}

// The bomber man place a bomb
void game::placeBomb()
{
	Vector2i pos;								// For the adjusted pos
	
	if (_man.canPlaceBomb())					// Check if a bomb can be placed
	{
		if (!_bomberMap.collision(_man.getRect(), _man.dir(), 0, 80)) // Check if there is no collision
		{
			_itBomb = _bomb.insert(_itBomb, bomb(_texture, IntRect(0, 480, 120, 40), 3, 1));
			setBombRange(*_itBomb);
			_itBomb->setExplosion(_texture, IntRect(0, 360, 120, 120), 3, 3);

			switch (_man.dir())					// Place a bomb depending of the direction of the man
			{
			case 0:								// Up
				_itBomb->setPosition(_man.posX(), _man.posY() - 40);
				break;

			case 1:								// Right
				_itBomb->setPosition(_man.posX() + 40, _man.posY());
				break;

			case 2:								// Down
				_itBomb->setPosition(_man.posX(), _man.posY() + 40);
				break;

			case 3:								// Left
				_itBomb->setPosition(_man.posX() - 40, _man.posY());
				break;
			}

			pos = _bomberMap.adjustPosition(IntRect(_itBomb->getPosX(), _itBomb->getPosY(), _itBomb->getWidth(), _itBomb->getHeight()), 40, 40);
			_itBomb->setPosition(pos.x, pos.y);

			_man.placeBomb();
		}
	}
}

// Set the range of the explosion
void game::setBombRange(bomb &bomb)
{
	bomb.setRange(2);
	bomb.setRange(findBombRange(bomb, 0), findBombRange(bomb, 1),
				  findBombRange(bomb, 2), findBombRange(bomb, 3));
}

// Find the range of each side for the explosion
int game::findBombRange(bomb &bomb, int dir, int newRange)
{
	switch (dir)								// Set the change depending on the direction
	{
	case 0:										// Up
		if (bomb.getRange() > newRange)			// Don't make the range higher than the base range
		{
			if (_bomberMap.getElement(bomb.getPosX() / 40, ((bomb.getPosY() / 40) - 1 - newRange) - 2) == '0')
				newRange = findBombRange(bomb, 0, newRange + 1);	// No collision
			else
				return newRange;
		}
		else
			return newRange;
		break;

	case 1:										// Right
		if (bomb.getRange() > newRange)			// Don't make the range higher than the base range
		{
			if (_bomberMap.getElement((bomb.getPosX() / 40) + 1 + newRange, (bomb.getPosY() / 40) - 2) == '0')
				newRange = findBombRange(bomb, 1, newRange + 1);	// No collision
			else
				return newRange;
		}
		else
			return newRange;
		break;

	case 2:										// Down
		if (bomb.getRange() > newRange)			// Don't make the range higher than the base range
		{
			if (_bomberMap.getElement(bomb.getPosX() / 40, ((bomb.getPosY() / 40) + 1 + newRange) - 2) == '0')
				newRange = findBombRange(bomb, 2, newRange + 1);	// No collision
			else
				return newRange;
		}
		else
			return newRange;
		break;

	case 3:										// Left
		if (bomb.getRange() > newRange)			// Don't make the range higher than the base range
		{
			if (_bomberMap.getElement((bomb.getPosX() / 40) - 1 - newRange, (bomb.getPosY() / 40) - 2) == '0')
				newRange = findBombRange(bomb, 3, newRange + 1);	// No collision
			else
				return newRange;
		}
		else
			return newRange;
		break;
	}
}

// Make a block explose
void game::bombExplose(bomb &bomb)
{
	killCharacter(bomb, _man);

	_itMonster = _monster.begin();

	while (_itMonster != _monster.end())
	{
		killCharacter(bomb, *_itMonster);
		_itMonster++;
	}

	if (bomb.getRange(0) != bomb.getRange() ||
		bomb.getRange(1) != bomb.getRange() ||
		bomb.getRange(2) != bomb.getRange() ||
		bomb.getRange(3) != bomb.getRange())	// Check if the bomb have touch something
	{											
		if (_bomberMap.getElement((bomb.getPosX() / 40), 
								  (bomb.getPosY() / 40) - 2 - bomb.getRange(0) - 1) == '2' &&
								   bomb.getRange(0) != bomb.getRange()) // Blow up
		{
			_itBlock = _bomberBlock.begin();

			while (_itBlock != _bomberBlock.end()) // Check all the blocks
			{
				if (_itBlock->getPosX() == bomb.getPosX() &&
					_itBlock->getPosY() == bomb.getPosY() - ((bomb.getRange(0) + 1) * 40))
					_itBlock->blow();			// The block is blowed
				_itBlock++;
			};
		}

		if (_bomberMap.getElement((bomb.getPosX() / 40) + bomb.getRange(1) + 1,
								  (bomb.getPosY() / 40) - 2) == '2' &&
								   bomb.getRange(1) != bomb.getRange())	// Blow Right
		{
			_itBlock = _bomberBlock.begin();

			while (_itBlock != _bomberBlock.end()) // Check all the blocks
			{
				if (_itBlock->getPosX() == bomb.getPosX() + ((bomb.getRange(1) + 1) * 40) &&
					_itBlock->getPosY() == bomb.getPosY()) // The block is blowed
					_itBlock->blow();
				_itBlock++;
			};
		}

		if (_bomberMap.getElement(bomb.getPosX() / 40,
								 (bomb.getPosY() / 40) - 2 + bomb.getRange(2) + 1) == '2' &&
								  bomb.getRange(2) != bomb.getRange()) // Blow down
		{
			_itBlock = _bomberBlock.begin();

			while (_itBlock != _bomberBlock.end()) // Check all the blocks
			{
				if (_itBlock->getPosX() == bomb.getPosX() &&	
					_itBlock->getPosY() == bomb.getPosY() + ((bomb.getRange(2) + 1) * 40))
					_itBlock->blow();			// The block is blowed
				_itBlock++;
			};
		}

		int meow = (bomb.getPosX() / 40) - bomb.getRange(3) - 1;

		if (_bomberMap.getElement((bomb.getPosX() / 40) - bomb.getRange(3) - 1,
								  (bomb.getPosY() / 40) - 2) == '2' &&
								   bomb.getRange(3) != bomb.getRange()) // Blow left
		{
			_itBlock = _bomberBlock.begin();

			while (_itBlock != _bomberBlock.end()) // Check all the blocks
			{
				if (_itBlock->getPosX() == bomb.getPosX() - ((bomb.getRange(3) + 1) * 40) &&
					_itBlock->getPosY() == bomb.getPosY()) // The block is blowed
					_itBlock->blow();

				_itBlock++;
			}
		}
	}
}

// Kill or not a character
void game::killCharacter(bomb &bomb, character &charac)
{
	if ((((charac.posX() >= bomb.getPosX() - (bomb.getRange(3) * 40) - 39 &&
		charac.posX() <= bomb.getPosX()) ||
		(charac.posX() >= bomb.getPosX() + 40 &&
		charac.posX() <= bomb.getPosX() + (bomb.getRange(2) * 40) + 39)) &&
		(charac.posY() >= bomb.getPosY() - 39 &&
		charac.posY() <= bomb.getPosY() + 39)) ||
		(((charac.posY() >= bomb.getPosY() - (bomb.getRange(0) * 40) - 39 &&
		charac.posY() <= bomb.getPosY()) ||
		(charac.posY() >= bomb.getPosY() + 40 &&
		charac.posY() <= bomb.getPosY() + (bomb.getRange(2) * 40) + 39)) &&
		(charac.posX() >= bomb.getPosX() - 39 &&
		charac.posX() <= bomb.getPosX() + 39)))	// Check if the chracter is touched by the explosion
		if (charac.alive())
			charac.hit();
}

// Destroyer
game::~game()
{
	_continue = false;
}