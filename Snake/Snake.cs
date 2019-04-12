using System;
using System.Linq;
using System.Threading;
using System.Collections.Generic;

namespace SnakeGame
{
    class Snake
    {
        public byte X { get; set; }
        public byte Y { get; set; }

        public char HeadSymbol { get; private set; }
        public char TailSymbol { get; private set; }

        public ConsoleColor HeadColor { get; private set; }
        public ConsoleColor TailColor { get; private set; }

        public List<Tail> Tail { get; private set; }

        public Snake(byte x, byte y, char headSymbol = 'O', char tailSymbol = 'o', ConsoleColor headColor = ConsoleColor.Red, ConsoleColor tailColor = ConsoleColor.Yellow)
        {
            X = x;
            Y = y;

            Tail = new List<Tail>();

            HeadSymbol = headSymbol;
            TailSymbol = tailSymbol;

            HeadColor = headColor;
            TailColor = tailColor;
        }

        public bool Eat(Food food)
        {
            if (food.X == X && food.Y == Y)
            {
                Tail.Add(new Tail(X, Y));
                return true;
            }
            return false;
        }

        public void MoveUp(byte maxY)
        {

            MoveTail();
            if (Y == 1)
                Y = (byte)(maxY - 2);
            else
                Y--;
        }
        public void MoveDown(byte maxY)
        {
            MoveTail();
            if (Y == maxY - 2)
                Y = 1;
            else
                Y++;
        }
        public void MoveLeft(byte maxX)
        {
            MoveTail();
            if (X == 1)
                X = (byte)(maxX - 2);
            else
                X--;
        }
        public void MoveRight(byte maxX)
        {
            MoveTail();
            if (X == maxX - 2)
                X = 1;
            else
                X++;
        }

        private void MoveTail()
        {
            byte[] firstXYTemp = new byte[2];
            byte[] secondXYTemp = new byte[2];

            for (int i = 0; i < Tail.Count; i++)
            {
                if (i == 0)
                {
                    firstXYTemp[0] = Tail[i].X;
                    firstXYTemp[1] = Tail[i].Y;

                    Tail[i].X = X;
                    Tail[i].Y = Y;
                }
                else
                {
                    secondXYTemp[0] = Tail[i].X;
                    secondXYTemp[1] = Tail[i].Y;

                    Tail[i].X = firstXYTemp[0];
                    Tail[i].Y = firstXYTemp[1];

                    firstXYTemp[0] = secondXYTemp[0];
                    firstXYTemp[1] = secondXYTemp[1];
                }
            }
        }
    }
}