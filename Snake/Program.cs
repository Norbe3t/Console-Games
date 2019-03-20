using System;
using System.Linq;
using System.Threading;
using System.Collections.Generic;

namespace SnakeGame
{
    class Program
    {
        static void Main()
        {
            GameEngine game = new GameEngine();
            game.Start();
            Console.ReadKey();
        }
    }
}