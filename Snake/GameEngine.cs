using System;

namespace SnakeGame
{
    class GameEngine
    {
        Random random;

        Draw draw;
        GameArea gameArea;
        Snake snake;
        Food food;

        public GameEngine(){
            random = new Random();

            draw = new Draw();
            gameArea = new GameArea(40, 15, '#', ' ');
            snake = new Snake(random.Next(1, gameArea.Width - 1), random.Next(1, gameArea.Height - 1));
            food = new Food(random.Next(1, gameArea.Width - 1), random.Next(1, gameArea.Height - 1));
        }

        public void Start()
        {
            draw.DrawGameArea(gameArea);
            draw.DrawSnake(snake);
            draw.DrawFood(food);
        }
    }
}