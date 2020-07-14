// ArcanoidTutorial.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <SFML\Graphics.hpp>
#include <SFML\Window.hpp>
#include "Ball.h"
#include "Paddle.h"
#include "Block.h" 
using namespace sf;
using namespace std;


template <class T1, class T2> bool IsIntersecting(T1& A, T2& B)		///klasa szablonowa
{
	return A.right() >= B.left() && A.left() <= B.right()
		&& A.bottom() >= B.top() && A.top() <= B.bottom();			///sprawdzanie czy dany ksztalt przecian sie z drugim
}

bool colisionTest(Paddle& paddle, Ball& ball)
{
	if (!IsIntersecting(paddle, ball)) return false;
	
	ball.moveUp();

	if (ball.getPosition().x < paddle.getPosition().x)
	{
		ball.moveLeft();
	}
	else if (ball.getPosition().x > paddle.getPosition().x)
	{
		ball.moveDown();
	}
}

bool colisionTest(Block& block, Ball& ball)
{
	if (!IsIntersecting(block, ball)) return false;

	block.destroy();

	float overlapLeft{ ball.right() - block.left() };
	float overlapRight{ block.right() - ball.left() };

	float overlapTop{ ball.bottom() - block.top() };
	float overlapBottom{ block.bottom() - ball.top() };

	bool ballFromLeft(abs(overlapLeft) < abs(overlapRight));
	bool ballFromTop(abs(overlapTop) < abs(overlapBottom));

	float minOverlapX{ ballFromLeft ? overlapLeft : overlapRight };
	float minOverlapY{ ballFromTop ? overlapTop : overlapBottom };

	if (abs(minOverlapX) < abs(minOverlapY))
	{
		ballFromLeft ? ball.moveLeft() : ball.moveRight();
	}
	else
	{
		ballFromTop ? ball.moveUp() : ball.moveDown();
	}


}

int main()
{
	Ball ball(400, 300);		///wspolrzedne ekranu
	Paddle paddle(400, 550);
	RenderWindow window{ VideoMode{800,600},"Arcanoid Tutorial" };
	window.setFramerateLimit(60);
	Event event;
	unsigned blocksX{ 10 }, blocksY{ 4 }, blockWidth{ 60 }, blockHeight{ 20 };

	vector<Block> blocks;

	for (int i = 0; i < blocksY; i++)
	{
		for (int j = 0; j < blocksX; j++)
		{
			blocks.emplace_back((j + 1)*(blockWidth + 10), (i + 2)*(blockHeight + 5), blockWidth, blockHeight);
		}
	}
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
		paddle.update();
		colisionTest(paddle, ball);

		for (auto& block : blocks) if (colisionTest(block, ball)) break; 

		auto iterator = remove_if(begin(blocks), end(blocks), [](Block& block) {return block.isDestroyed(); });
		blocks.erase(iterator, end(blocks));

		window.draw(ball);
		window.draw(paddle);

		for (auto& block : blocks)
			window.draw(block);
		window.display();

	}
    return 0;
}

