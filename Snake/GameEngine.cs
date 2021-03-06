using System;
using System.Threading.Tasks;
using System.Threading;

namespace SnakeGame
{
    class GameEngine
    {
        Random random;
        private int direction = 0;
        private bool isGameRunning = false;

        Draw draw;
        GameArea gameArea;
        Snake snake;
        Food food;

        public GameEngine(){
            random = new Random();

            draw = new Draw();
            gameArea = new GameArea(40, 15);
            snake = new Snake((byte)random.Next(1, gameArea.Width - 1), (byte)random.Next(1, gameArea.Height - 1));
            food = new Food((byte)random.Next(1, gameArea.Width - 1), (byte)random.Next(1, gameArea.Height - 1));
        }

        public void Start()
        {
            isGameRunning = true;
            draw.DrawGameArea(gameArea);
            draw.DrawBonusArea(gameArea);
            draw.DrawSnake(snake, gameArea);
            draw.DrawFood(food);

            RenderAsync(60);
            SetDirectionAsync();
        }

        private void Move()
        {
            switch(direction)
            {
                case 0:
                    snake.MoveUp(gameArea.Height);
                    break;
                case 1:
                    snake.MoveDown(gameArea.Height);
                    break;
                case 2:
                    snake.MoveLeft(gameArea.Width);
                    break;
                case 3:
                    snake.MoveRight(gameArea.Width);
                    break;
            }
        }

        private async void SetDirectionAsync()
        {
            await Task.Run(() => SetDirection());
        }

        private void SetDirection()
        {
            while(isGameRunning)
            {
                if(Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    switch(key.Key)
                    {
                        case ConsoleKey.UpArrow:
                            if(direction != 1)
                                direction = 0;
                            break;
                        case ConsoleKey.DownArrow:
                            if(direction != 0)
                                direction = 1;
                            break;
                        case ConsoleKey.LeftArrow:
                            if(direction != 3)
                                direction = 2;
                            break;
                        case ConsoleKey.RightArrow:
                            if(direction != 2)
                                direction = 3;
                            break;
                    }
                }
            }
        }

        private async void RenderAsync(int fps)
        {
            await Task.Run(() => Render(fps));
        }

        private void Render(int fps)
        {
            while(isGameRunning)
            {
                draw.ClearSnakeTrack(snake, gameArea);
                Move();
                draw.DrawSnake(snake, gameArea);
                TryEat();
                if (isGameRunning)
                {    
                    draw.DrawFood(food);
                    Thread.Sleep(fps);   
                }
            }
        }

        private void TryEat()
        {
            if(!snake.IsFoodIsTail())
            {
                if(snake.Eat(food))
                {
                    food.SetNewCoordinate(snake, gameArea);   
                }
            }
            else
            {
                isGameRunning = false;
                Console.Clear();
                Console.WriteLine("Game over !");
            }
        }
    }
}