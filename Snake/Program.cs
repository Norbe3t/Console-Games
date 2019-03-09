using System;
using System.Linq;
using System.Collections.Generic;

namespace Snake
{
    class Program
    {
        static void Main()
        {
            SnakeGame game = new SnakeGame();
            //game.Menu();
            game.Start();
        }
    }

    class SnakeGame
    {
        public void Menu(){}
        public void Start()
        {
            var _gameField = new GameField(100, 20);
            var _food = new Food(5, 5);
            var _snakeTail = new List<SnakeTail>();
            var _snake = new Snake(_gameField, _snakeTail, 10, 10);
            var _draw = new Draw(_gameField, _snake, _snakeTail, _food);

            _draw.DrawArea();
            _draw.DrawSnake();
            _draw.DrawFood();

            ConsoleKeyInfo keyinfo;
            do
            {
                keyinfo = Console.ReadKey();
                switch (keyinfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        _snake.action.MoveUp();
                        if(_snake.action.TryEat(_snake ,ref _snakeTail, ref _food))
                        {
                            _draw.DrawFood();
                        }
                        _draw.DrawSnake();
                        _draw.DrawSnakeTail();
                        break;

                    case ConsoleKey.DownArrow:
                        _snake.action.MoveDown();
                        if(_snake.action.TryEat(_snake ,ref _snakeTail, ref _food))
                        {
                            _draw.DrawFood();
                        }
                        _draw.DrawSnake();
                        _draw.DrawSnakeTail();
                        break;

                    case ConsoleKey.LeftArrow:
                        _snake.action.MoveLeft();
                        if(_snake.action.TryEat(_snake ,ref _snakeTail, ref _food))
                        {
                            _draw.DrawFood();
                        }
                        _draw.DrawSnake();
                        _draw.DrawSnakeTail();
                        break;

                    case ConsoleKey.RightArrow:
                        _snake.action.MoveRight();
                        if(_snake.action.TryEat(_snake ,ref _snakeTail, ref _food))
                        {
                            _draw.DrawFood();
                        }
                        _draw.DrawSnake();
                        _draw.DrawSnakeTail();
                        break;
                }
            }
            while (keyinfo.Key != ConsoleKey.X);
        }
        public void Stop(){}
        public void Restart(){}
        public void Pause(){}
    }

    class GameField
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public GameField(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }
    }

    class Draw
    {
        private GameField _gameField;
        private Snake _snake;
        private List<SnakeTail> _snakeTail;
        private Food _food;

        public int snakeX { get; set; }
        public int snakeY { get; set; }
        public int snakeTailX { get; set; }
        public int snakeTailY { get; set; }

        public Draw(GameField gameField, Snake snake, List<SnakeTail> snakeTail, Food food)
        {
            this._gameField = gameField;
            this._snake = snake;
            this._snakeTail = snakeTail;
            this._food = food;
        }

        private void SetCursor()
        {
            Console.SetCursorPosition(_gameField.Width, _gameField.Height);
        }

        private void SetCursor(int x, int y)
        {
            Console.SetCursorPosition(x, y);
        }

        public void DrawArea()
        {
            Console.Clear();
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);

            for (int y = 0; y < _gameField.Height; y++)
            {
                for (int x = 0; x < _gameField.Width; x++)
                {
                    if ((y == 0 || y == _gameField.Height - 1) && x < _gameField.Width - 1)
                    {
                        Console.Write("#");
                    }
                    else if (x == 0)
                    {
                        Console.Write("#");
                    }
                    else if (x == _gameField.Width - 1)
                    {
                        Console.WriteLine("#");
                    }
                    else
                    {
                        Console.Write(".");
                    }
                }
            }
        }

        public void DrawSnake()
        {
            if (snakeX != 0 || snakeY != 0)
            {
                SetCursor(snakeX, snakeY);
                Console.Write(".");
            }

            SetCursor(_snake.X, _snake.Y);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("O");
            Console.ForegroundColor = ConsoleColor.Gray;

            snakeX = _snake.X;
            snakeY = _snake.Y;

            SetCursor();
        }

        public void DrawSnakeTail()
        {
            if (snakeTailX != 0 || snakeTailY != 0)
            {
                SetCursor(snakeTailX, snakeTailY);
                Console.Write(".");
            }
            foreach (var item in _snakeTail)
            {
                SetCursor(item.X, item.Y);

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("o");
                Console.ForegroundColor = ConsoleColor.Gray;

                SetCursor();
            }
            if (_snakeTail.Count() != 0)
            {
                snakeTailX = _snakeTail.Last().X;
                snakeTailY = _snakeTail.Last().Y;
            }

            SetCursor();
        }

        public void DrawFood()
        {
            SetCursor(_food.X, _food.Y);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("*");
            Console.ForegroundColor = ConsoleColor.Gray;

            SetCursor();
        }
    }
    
    class Snake
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Action action;
        public Snake(GameField gameField, List<SnakeTail> snakeTail, int x, int y)
        {
            this.X = x;
            this.Y = y;
            action = new Action(this, snakeTail, gameField);
        }
    }

    class SnakeTail
    {
        public int X { get; set; }
        public int Y { get; set; }

        public SnakeTail(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }

    class Food
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Food(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }

    class Action
    {
        private Snake _snake;
        private List<SnakeTail> _snakeTail;
        private GameField _gameField;
        private Random random = new Random();

        int tempX = 0;
        int tempY = 0;

        int tempXX = 0;
        int tempYY = 0;

        public Action(Snake snake, List<SnakeTail> snakeTail, GameField gameField)
        {
            this._snake = snake;
            this._snakeTail = snakeTail;
            this._gameField = gameField;
        }

        public bool TryEat(Snake snake, ref List<SnakeTail> snakeTail, ref Food food)
        {
            if ((food.X == _snake.X) && (food.Y == _snake.Y))
            {
                if (snakeTail.Count != 0)
                {
                    var lastSnakeTail = snakeTail.Last();
                    snakeTail.Add(new SnakeTail(lastSnakeTail.X, lastSnakeTail.Y));
                }
                else
                {
                    snakeTail.Add(new SnakeTail(snake.X, snake.Y));
                }
                food.X = random.Next(1, _gameField.Width - 1);
                food.Y = random.Next(1, _gameField.Height - 1);
                return true;
            }
            return false;
        }
        public void MoveUp()
        {
            for (int i = 0; i < _snakeTail.Count; i++)
            {
                
                if(i == 0)
                {
                    tempX = _snakeTail[i].X;
                    tempY = _snakeTail[i].Y;
                    _snakeTail[i].X = _snake.X;
                    _snakeTail[i].Y = _snake.Y;
                }
                else
                {
                    tempXX = tempX;
                    tempYY = tempY;

                    tempX = _snakeTail[i].X;
                    tempY = _snakeTail[i].Y;
                    _snakeTail[i].X = tempXX;
                    _snakeTail[i].Y = tempYY; 
                }
            }
            if ((--_snake.Y) < 1)
            {
                _snake.Y = _gameField.Height - 2;
            }
        }
        public void MoveDown()
        {
            for (int i = 0; i < _snakeTail.Count; i++)
            {
                
                if(i == 0)
                {
                    tempX = _snakeTail[i].X;
                    tempY = _snakeTail[i].Y;
                    _snakeTail[i].X = _snake.X;
                    _snakeTail[i].Y = _snake.Y;
                }
                else
                {
                    tempXX = tempX;
                    tempYY = tempY;

                    tempX = _snakeTail[i].X;
                    tempY = _snakeTail[i].Y;
                    _snakeTail[i].X = tempXX;
                    _snakeTail[i].Y = tempYY; 
                }
            }
            if ((++_snake.Y) > _gameField.Height - 2)
            {
                _snake.Y = 1;
            }
        }
        public void MoveLeft()
        {
            for (int i = 0; i < _snakeTail.Count; i++)
            {
                
                if(i == 0)
                {
                    tempX = _snakeTail[i].X;
                    tempY = _snakeTail[i].Y;
                    _snakeTail[i].X = _snake.X;
                    _snakeTail[i].Y = _snake.Y;
                }
                else
                {
                    tempXX = tempX;
                    tempYY = tempY;

                    tempX = _snakeTail[i].X;
                    tempY = _snakeTail[i].Y;
                    _snakeTail[i].X = tempXX;
                    _snakeTail[i].Y = tempYY; 
                }
            }
            if ((--_snake.X) < 1)
            {
                _snake.X = _gameField.Width - 2;
            }
        }
        public void MoveRight()
        {
           for (int i = 0; i < _snakeTail.Count; i++)
            {
                
                if(i == 0)
                {
                    tempX = _snakeTail[i].X;
                    tempY = _snakeTail[i].Y;
                    _snakeTail[i].X = _snake.X;
                    _snakeTail[i].Y = _snake.Y;
                }
                else
                {
                    tempXX = tempX;
                    tempYY = tempY;

                    tempX = _snakeTail[i].X;
                    tempY = _snakeTail[i].Y;
                    _snakeTail[i].X = tempXX;
                    _snakeTail[i].Y = tempYY; 
                }
            }
            if ((++_snake.X) > _gameField.Width - 2)
            {
                _snake.X = 1;
            }
        }
    }
}