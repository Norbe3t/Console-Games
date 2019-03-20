using System;

namespace SnakeGame
{
    class Food
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public char FoodSymbol { get; private set; }
        public ConsoleColor FoodColor { get; private set; }

        public Food(int x, int y, char foodSymbol = '*', ConsoleColor foodColor = ConsoleColor.Green)
        {
            X = x;
            Y = y;
            FoodSymbol = foodSymbol;
            FoodColor = foodColor;
        }
    }
}