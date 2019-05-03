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
                    Console.WriteLine(new string(gameArea.BorderSymbol, gameArea.Width));
                }
                else
                {
                    Console.WriteLine(gameArea.BorderSymbol + new string(gameArea.AreaSymbol, gameArea.Width - 2) + gameArea.BorderSymbol);
                }
            }
        }

        public void DrawBonusArea(GameArea gameArea)
        {
            Console.SetCursorPosition(0, gameArea.Height);
            for(int i = 0; i < 5; i++)
            {
                if(i != 4)
                {
                    Console.WriteLine(gameArea.BorderSymbol + new string(gameArea.AreaSymbol, gameArea.Width - 2) + gameArea.BorderSymbol);
                }
                else 
                {
                    Console.WriteLine(new string(gameArea.BorderSymbol, gameArea.Width));
                }
            }
        }

        public void DrawSnake(Snake snake, GameArea gameArea)
        {
            ClearSnakeTrack(snake, gameArea);

            Console.SetCursorPosition(snake.X, snake.Y);
            Console.ForegroundColor = snake.HeadColor;

            Console.Write(snake.HeadSymbol);

            Console.ForegroundColor = ConsoleColor.Gray;
            DrawSnakeTail(snake);
        }

        public void DrawSnakeTail(Snake snake)
        {
            foreach (var tail in snake.Tail)
            {
                Console.SetCursorPosition(tail.X, tail.Y);
                Console.ForegroundColor = snake.TailColor;

                Console.Write(snake.TailSymbol);

                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }

        public void ClearSnakeTrack(Snake snake, GameArea gameArea)
        {
            if (snake.Tail.Count != 0)
            {
                int lastX = snake.Tail[snake.Tail.Count - 1].X;
                int lastY = snake.Tail[snake.Tail.Count - 1].Y;
                Console.SetCursorPosition(lastX, lastY);
                Console.Write(gameArea.AreaSymbol);
            }
            else
            {
                Console.SetCursorPosition(snake.X, snake.Y);
                Console.Write(gameArea.AreaSymbol);
            }
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