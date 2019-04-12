using System;

namespace SnakeGame 
{
    class Tail
    {
        public byte X { get; set; }
        public byte Y { get; set; }

        public Tail(byte x, byte y)
        {
            X = x;
            Y = y;    
        }
    }
}