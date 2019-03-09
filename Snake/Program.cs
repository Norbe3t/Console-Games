using System;
using System.Linq;
using System.Threading;
using System.Collections.Generic;

namespace Snake
{
    class Program
    {
        static void Main()
        {
            SnakeGame game = new SnakeGame();
            game.Start();
        }
    }

    class SnakeGame
    {
        public void Menu() { }
        public void Start()
        {
            var _gameField = new GameField(30, 20);
            var _draw = new Draw(_gameField);

            _draw.DrawArea();
            _draw.DrawSnake();
            _draw.DrawFood();

            var _snakeControl = new SnakeControl(_draw);

            while (true)
            {
                if(_snakeControl.StartMove())
                    break;
                Thread.Sleep(200);
            }
            GameOver();
        }
        public void GameOver() 
        { 
            Console.Clear();
            Console.WriteLine("Game Over !!!");
            Console.ReadKey();
        }
        public void Restart() { }
        public void Pause() { }
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
        public Snake _snake { get; private set; }
        private List<SnakeTail> _snakeTail;
        private Food _food;

        public int snakeX { get; set; }
        public int snakeY { get; set; }
        public int snakeTailX { get; set; }
        public int snakeTailY { get; set; }

        public Draw(GameField gameField)
        {
            this._gameField = gameField;
            this._snakeTail = new List<SnakeTail>();
            this._food = new Food(_gameField);
            this._snake = new Snake(this, _gameField, _snakeTail, _food);
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
                        Console.Write(" ");
                    }
                }
            }
        }

        public void DrawSnake(bool isSnakeTail = false)
        {
            if (!isSnakeTail && (snakeX != 0 || snakeY != 0))
            {
                SetCursor(snakeX, snakeY);
                Console.Write(" ");
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
                Console.Write(" ");
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
        private Random random;

        public int X { get; set; }
        public int Y { get; set; }
        public List<SnakeTail> SnakeTail { get; private set; }
        public Action action;

        public Snake(Draw draw, GameField gameField, List<SnakeTail> snakeTail, Food food)
        {
            random = new Random();
            this.X = random.Next(1, gameField.Width / 2);
            this.Y = random.Next(1, gameField.Height / 2);
            this.SnakeTail = snakeTail;
            action = new Action(draw, this, food, gameField);
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
        private Random random;
        public int X { get; set; }
        public int Y { get; set; }

        public Food(GameField gameField)
        {
            random = new Random();
            this.X = random.Next(gameField.Width / 2, gameField.Width - 2);
            this.Y = random.Next(1, gameField.Height - 2);
        }
    }

    class Action
    {
        private GameField _gameField;
        private Draw _draw;
        private Snake _snake;
        private List<SnakeTail> _snakeTail;
        private Food _food;
        private Random random = new Random();

        private bool _isSnakeEatFood = false;

        int tempX = 0;
        int tempY = 0;

        int tempXX = 0;
        int tempYY = 0;

        public Action(Draw draw, Snake snake, Food food, GameField gameField)
        {
            this._draw = draw;
            this._snake = snake;
            this._snakeTail = _snake.SnakeTail;
            this._food = food;
            this._gameField = gameField;
        }

        public bool TryEat()
        {
            if ((_food.X == _snake.X) && (_food.Y == _snake.Y))
            {
                if (_snakeTail.Count != 0)
                {
                    var lastSnakeTail = _snakeTail.Last();
                    _snakeTail.Add(new SnakeTail(lastSnakeTail.X, lastSnakeTail.Y));
                }
                else
                {
                    _snakeTail.Add(new SnakeTail(_snake.X, _snake.Y));
                }
                _food.X = random.Next(1, _gameField.Width - 1);
                _food.Y = random.Next(1, _gameField.Height - 1);
                return true;
            }
            return false;
        }
        public bool MoveUp()
        {
            SnakeTail();
            if ((--_snake.Y) < 1)
            {
                _snake.Y = _gameField.Height - 2;
            }
            Draw(TryEat());
            return CheckTail();
        }
        public bool MoveDown()
        {
            SnakeTail();
            if ((++_snake.Y) > _gameField.Height - 2)
            {
                _snake.Y = 1;
            }
            Draw(TryEat());
            return CheckTail();
        }
        public bool MoveLeft()
        {
            SnakeTail();
            if ((--_snake.X) < 1)
            {
                _snake.X = _gameField.Width - 2;
            }
            Draw(TryEat());
            return CheckTail();
        }
        public bool MoveRight()
        {
            SnakeTail();
            if ((++_snake.X) > _gameField.Width - 2)
            {
                _snake.X = 1;
            }
            Draw(TryEat());
            return CheckTail();
        }

        public void Draw(bool drawFood)
        {
            _draw.DrawSnakeTail();
            _draw.DrawSnake(_isSnakeEatFood);
            if (drawFood)
            {
                _draw.DrawFood();
                _isSnakeEatFood = true;
            }
        }

        public bool CheckTail()
        {
            if(_snakeTail.Count > 3)
            {
                foreach(var item in _snakeTail)
                {
                    if (_snake.X == item.X && _snake.Y == item.Y)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void SnakeTail()
        {
            for (int i = 0; i < _snakeTail.Count; i++)
            {

                if (i == 0)
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
        }
    }

    class SnakeControl
    {
        private Draw _draw;
        private int _motion = 0;

        public SnakeControl(Draw draw)
        {
            this._draw = draw;
        }

        public bool StartMove()
        {
            ConsoleKeyInfo keyinfo;
            while (Console.KeyAvailable)
            {
                keyinfo = Console.ReadKey();
                switch (keyinfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (_motion != 1)
                            _motion = 3;
                        break;

                    case ConsoleKey.DownArrow:
                        if (_motion != 3)
                            _motion = 1;
                        break;

                    case ConsoleKey.LeftArrow:
                        if (_motion != 0)
                            _motion = 2;
                        break;

                    case ConsoleKey.RightArrow:
                        if (_motion != 2)
                            _motion = 0;
                        break;
                }
            }
            return Move();
        }

        private bool Move()
        {
            switch (_motion)
            {
                case 0:
                    return _draw._snake.action.MoveRight();

                case 1:
                    return _draw._snake.action.MoveDown();

                case 2:
                    return _draw._snake.action.MoveLeft();

                case 3:
                    return _draw._snake.action.MoveUp();
            }
            return false;
        }
    }
}