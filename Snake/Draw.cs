using System;

namespace SnakeGame
{
    class Draw
    {
        public void DrawGameArea(GameArea gameArea)
        {
            Console.Clear();
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);

            for (int i = 0; i < gameArea.Height; i++)
            {
                if (i == 0 || i == gameArea.Height - 1)
                {
                    Console.WriteLine(new string(gameArea.Border, gameArea.Width));
                }
                else
                {
                    Console.WriteLine(gameArea.Border + new string(gameArea.Area, gameArea.Width - 2) + gameArea.Border);
                }
            }
        }

        public void DrawSnake(Snake snake)
        {
            Console.SetCursorPosition(snake.X, snake.Y);
            Console.ForegroundColor = snake.HeadColor;

            Console.Write(snake.HeadSymbol);

            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void DrawFood(Food food)
        {
            Console.SetCursorPosition(food.X, food.Y);
            Console.ForegroundColor = food.FoodColor;

            Console.Write(food.FoodSymbol);

            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}