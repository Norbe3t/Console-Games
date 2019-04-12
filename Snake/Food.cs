using System;

namespace SnakeGame
{
    class Food
    {
        private Random random;

        public byte X { get; private set; }
        public byte Y { get; private set; }
        public char FoodSymbol { get; private set; }
        public ConsoleColor FoodColor { get; private set; }


        public Food(byte x, byte y, char foodSymbol = '*', ConsoleColor foodColor = ConsoleColor.Green)
        {
            X = x;
            Y = y;

            FoodSymbol = foodSymbol;
            FoodColor = foodColor;

            random = new Random();
        }

        public void SetNewCoordinate(Snake snake, GameArea gameArea)
        {
            bool isSnakeCoordinate = true;
            int check;
            while (isSnakeCoordinate)
            {
                check = 0;

                X = (byte)random.Next(1, gameArea.Width - 1);
                Y = (byte)random.Next(1, gameArea.Height - 1);

                if(snake.X == X && snake.Y == Y)
                {
                    check++;
                }

                foreach (var tail in snake.Tail)
                {
                    if(tail.X == X && tail.Y == Y) 
                    {
                        check++;
                    }
                }
                if(check > 0 )
                {
                    isSnakeCoordinate = true;
                }
                else
                {
                    isSnakeCoordinate = false;
                }
            }
        }
    }
}