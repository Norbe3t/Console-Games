using System;

namespace SnakeGame
{
    class GameArea
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public char Border { get; private set; }
        public char Area { get; private set; }

        public GameArea(int width, int height, char border, char area)
        {
            Width = width;
            Height = height;
            Border = border;
            Area = area;
        }
    }
}