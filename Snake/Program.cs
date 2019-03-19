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
            Snake snake = new Snake();
            Console.WriteLine(snake);
            Console.ReadKey();
        }
    }
}