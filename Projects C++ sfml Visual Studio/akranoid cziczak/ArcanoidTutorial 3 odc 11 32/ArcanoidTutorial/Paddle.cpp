#include "stdafx.h"
#include "Paddle.h"


Paddle::Paddle(float t_X, float t_Y)
{
	shape.setPosition(t_X, t_Y);
	shape.setSize({ this->paddleWidth, this->paddleHeight });
	shape.setFillColor(Color::Green);
	shape.setOrigin(paddleWidth / 2.f, paddleHeight / 2.f);
}

void Paddle::draw(RenderTarget& target, RenderStates state) const
{
	target.draw(this->shape, state);
}

void Paddle::update()
{
	this->shape.move(this->velocity);
}


