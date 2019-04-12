using System;

namespace SnakeGame
{
    class GameArea
    {
        public byte Width { get; private set; }
        public byte Height { get; private set; }
        public char BorderSymbol { get; private set; }
        public char AreaSymbol { get; private set; }

        public GameArea(byte width, byte height, char borderSymbol = '#', char areaSymbol = ' ')
        {
            Width = width;
            Height = height;
            BorderSymbol = borderSymbol;
            AreaSymbol = areaSymbol;
        }
    }
}