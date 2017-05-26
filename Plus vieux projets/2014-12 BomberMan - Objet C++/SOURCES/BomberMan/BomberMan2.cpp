/***********************************************************************************************************\
| Program name	: BomberMan																					|
| Creator name	: Mike Girard																				|
| Creation date	: 27-11-2014																				|
| Description	:
\***********************************************************************************************************/

// Preprocessor directives
#include <SFML\Graphics.hpp>
#include <string>
#include <fstream>
#include <list>
#include "menu.h"
#include "map.h"
#include "block.h"

using namespace sf;

// Main program
int main()
{
	bool started = false;

	float tempRafraichissement = 1 / (float)60;	// Frame limit

	Image image;

	image.loadFromFile("textures.bmp");
	image.createMaskFromColor(Color(50, 100, 150), 0);

	RenderWindow window;

	Time timer;

	Clock chrono;

	Texture texture;
	texture.loadFromImage(image);

	Event _event;

	string mapName;

	ifstream mapStream;

	menu mainMenu(texture);

	Map bomberMap(texture);

	list<block>	bomberBlock;
	list<block>::iterator itBlock = bomberBlock.begin();

	window.create(sf::VideoMode(600, 600), "BomberMan", Style::Close);

	started = mainMenu.show(window, !started);
	//started = true;

	if (started)
	{
		mapName = "map.txt";
		
		mapStream.open(mapName);

		bomberMap.load(mapStream);

		block blockTemp(texture, 40, 40);

		for (int i = 0; i < 10; i++)
		{
			bomberBlock.insert(itBlock, blockTemp);
		}

		while (started)
		{
			timer = chrono.getElapsedTime();

			while (window.pollEvent(_event))
			{
				switch (_event.type)
				{
				case Event::Closed:
					started = false;
					break;

				case Event::KeyPressed:
					switch (_event.key.code)
					{
					case Keyboard::Q:
						itBlock = bomberBlock.begin();
						itBlock->blow();
						break;
					}
					break;
				}
			}

			if (!bomberBlock.empty())
			{
				itBlock = bomberBlock.begin();

				while (itBlock != bomberBlock.end())
				{
					if (itBlock->isBlowed())
						itBlock = bomberBlock.erase(itBlock);
					else
						itBlock++;
				}
			}

			if (timer.asSeconds() >= tempRafraichissement)// Limit frame rate
			{
				window.clear(Color(255, 255, 255));

				bomberMap.draw(window, 0, 80);

				itBlock = bomberBlock.begin();

				while (itBlock != bomberBlock.end())
				{
					itBlock->draw(window);
					itBlock++;
				}

				window.display();

				timer = chrono.restart();
			}
		}
	}

	return 0;
}