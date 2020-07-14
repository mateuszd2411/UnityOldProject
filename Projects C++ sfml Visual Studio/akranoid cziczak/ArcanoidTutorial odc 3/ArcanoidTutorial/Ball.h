#pragma once
#include <SFML\Graphics.hpp>
using namespace sf;

class Ball : public sf::Drawable
{
public:	 
	Ball(float t_X, float t_Y);  
	Ball() = delete;
	~Ball() = default;

	void update(); // aktualizacja kulki z klatki na klatke

	float left();					///krawedzie kulki
	float right();
	float top();
	float bottom();
private:
	CircleShape shape;
	const float ballRadious{ 10.0f };
	const float ballVelocity{ 4.0f };		///nadanie ruchu kulce
	Vector2f velocity{ ballVelocity,ballVelocity }; //nadanie predkosci kulce 2 wektory
	void draw(RenderTarget& target, RenderStates state) const override;  ///nadpisujemy
};

