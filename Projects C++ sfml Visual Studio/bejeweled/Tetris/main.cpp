#include <SFML\Graphics.hpp>
#include <time.h>
using namespace sf;

int ts = 54;   ////tile size
Vector2i offset(48, 24);

struct piece
{
	int x, y, col, row, kind, match;
	piece() { match = 0; }

}	grid[10][10];

void swap(piece p1, piece p2)
{
	std::swap(p1.col, p2.col);
	std::swap(p1.row, p2.row);

	grid[p1.row][p1.col] = p1;
	grid[p2.row][p2.col] = p2;
}

int main()
{
	srand(time(0));

	RenderWindow app(VideoMode(740, 480), "Match-3 Game!");
	app.setFramerateLimit(60);

	Texture t1, t2;
	t1.loadFromFile("images/background.png");
	t2.loadFromFile("images/gems.png");

	Sprite background(t1), gems(t2);

	for (int i=1;i<=8;i++)
	for (int j = 1; j <= 8; j++)
	{
		grid[i][j].kind = rand() % 7;
		grid[i][j].col = j;
		grid[i][j].row = i;
		grid[i][j].x = j*ts;
		grid[i][j].y = i*ts;
	}

	int x0, y0, x, y; int click = 0; Vector2i pos;

	while (app.isOpen())
	{
		Event e;
		while (app.pollEvent(e))
		{
			if (e.type == Event::Closed)
				app.close();

			if (e.type == Event::Closed)
				app.close();

			if(e.type == Event::MouseButtonPressed)
				if (e.key.code == Mouse::Left)
				{
					click++;
					pos = Mouse::getPosition(app) - offset;
				}
		}

		////draw////				 
		app.draw(background);

		for(int i = 1; i <= 8;i++)
		for (int j = 1; j <= 8; j++)
		{
			piece p = grid[i][j];
			gems.setTextureRect(IntRect(p.kind * 49, 0, 49, 49));
			gems.setPosition(p.x, p.y);
			gems.move(offset.x - ts, offset.y - ts);
			app.draw(gems);

		}

		app.display();

    }



	return 0;
}