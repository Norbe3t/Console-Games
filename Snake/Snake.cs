using System;
using System.Linq;
using System.Threading;
using System.Collections.Generic;

namespace SnakeGame
{
    class Snake
    {
        public int X { get; set; }
        public int Y { get; set; }

        public char HeadSymbol { get; private set; }
        public char TailSymbol { get; private set; }

        public ConsoleColor HeadColor { get; private set; }
        public ConsoleColor TailColor { get; private set; }

        public List<Tail> Tail { get; private set; }

        public Snake(int x, int y, char headSymbol = 'O', char tailSymbol = 'o', ConsoleColor headColor = ConsoleColor.Red, ConsoleColor tailColor = ConsoleColor.Yellow)
        {
            X = x;
            Y = y;

            HeadSymbol = headSymbol;
            TailSymbol = tailSymbol;

            HeadColor = headColor;
            TailColor = tailColor;
        }

        public void Eat() { }

        public void MoveUp() { }
        public void MoveDown() { }
        public void MoveLeft() { }
        public void MoveRight() { }


    }
}