// ArcanoidTutorial.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <SFML\Graphics.hpp>
#include <SFML\Window.hpp>
#include "Ball.h"
using namespace sf;
using namespace std;


int main()
{
	Ball ball(400, 300);		///wspolrzedne ekranu
	RenderWindow window{ VideoMode{800,600},"Arcanoid Tutorial" };
	window.setFramerateLimit(60);
	Event event;
	while (true)
	{
		window.clear(Color::Black);
		window.pollEvent(event);

		if (event.type == Event::Closed)
		{
			window.close();
			break;
		}
		ball.update();	   //wywolanie metody

		window.draw(ball);	
		window.display();

	}
    return 0;
}

