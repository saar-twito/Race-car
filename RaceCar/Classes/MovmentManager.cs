using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace RaceCar
{
    public class MovmentManager
    {
        public byte Speed { get; set; } = 3;

        private DispatcherTimer carTimer;
        private DispatcherTimer lineTimer;
        private DispatcherTimer coinTimer;
        private DispatcherTimer policeTimer;

        public Canvas Canvas { get; private set; }
        public GameManager GameManager { get; private set; }
        public Car Car { get; private set; }
        public LineSegment LineSegment { get; private set; }
        public Coins Coins { get; private set; }
        public Collision ColiitionCoin { get; private set; }
        public Police Police { get; private set; }
        public bool IsGameOver { get; set; }

        public MovmentManager(Car car, LineSegment lineSegment, Coins coins, Canvas canvas, Collision coliitionCoin, Police police, GameManager gameManager)
        {
            Car = car;
            LineSegment = lineSegment;
            Coins = coins;
            Canvas = canvas;
            ColiitionCoin = coliitionCoin;
            Police = police;
            GameManager = gameManager;

            PreperTimers();
        }

        private void PreperTimers()
        {
            carTimer = new DispatcherTimer();
            carTimer.Interval = TimeSpan.FromMilliseconds(10);
            carTimer.Tick += CarTimer_Tick;

            lineTimer = new DispatcherTimer();
            lineTimer.Interval = TimeSpan.FromMilliseconds(50);
            lineTimer.Tick += Line_Tick;

            coinTimer = new DispatcherTimer();
            coinTimer.Interval = TimeSpan.FromMilliseconds(50);
            coinTimer.Tick += Coin_Tick;

            policeTimer = new DispatcherTimer();
            policeTimer.Interval = TimeSpan.FromMilliseconds(50);
            policeTimer.Tick += PoliceTimer_Tick;
        }

        private void PoliceTimer_Tick(object sender, EventArgs e) => MovePolice(Canvas); // Move police

        private void Coin_Tick(object sender, EventArgs e) => MoveCoin(Canvas); // Move coins

        private void Line_Tick(object sender, EventArgs e) => MoveLine(); // Move line

        private void CarTimer_Tick(object sender, EventArgs e) => MoveCar(); // Move car

        private void MoveLine() => LineSegment.DrawSubject(Canvas); // Draw line

        private void MoveCoin(Canvas canvas) => DrawCoins(canvas); // Draw coins

        private void MovePolice(Canvas canvas) => DrawPolice(canvas); // Draw police

        private void MoveCar()
        {
            if (Keyboard.IsKeyDown(Key.Left))
            {
                if (Car.CarPoints.X > Car.LeftBorder)
                    Car.CarPoints = new Point(Car.CarPoints.X - Speed, Car.CarPoints.Y);
            }
            else if (Keyboard.IsKeyDown(Key.Right))
            {
                if (Car.CarPoints.X < Car.RightBorder)
                    Car.CarPoints = new Point(Car.CarPoints.X + Speed, Car.CarPoints.Y);
            }
            else if (Keyboard.IsKeyDown(Key.Up))
            {
                if (Car.CarPoints.Y > Car.TopBorder)
                    Car.CarPoints = new Point(Car.CarPoints.X, Car.CarPoints.Y - Speed);
            }
            else if (Keyboard.IsKeyDown(Key.Down))
            {
                if (Car.CarPoints.Y < Car.BottomBorder)
                    Car.CarPoints = new Point(Car.CarPoints.X, Car.CarPoints.Y + Speed);
            }

            Canvas.SetLeft(Car.CarImage, Car.CarPoints.X);
            Canvas.SetTop(Car.CarImage, Car.CarPoints.Y);
        }

        public void DrawCoins(Canvas canvas)
        {
            if (IsGameOver == false)
            {
                for (int i = 0; i < Coins.UIElements.Count; i++)
                {
                    if (IsCanvasContainsThatCoin(canvas, i))
                    {
                        ColiitionCoin.CollisionCoinsWithCar(i);
                        Coins.CheckBoundary(canvas, i);
                    }
                    else
                        Coins.AddItemEveryTwoSec(canvas, i);
                }
            }
        }

        private bool IsCanvasContainsThatCoin(Canvas canvas, int i)
        {
            return canvas.Children.Contains(Coins.UIElements[i]);
        }

        public void DrawPolice(Canvas canvas)
        {
            for (int i = 0; i < Police.UIElements.Count; i++)
            {
                if (IsGameOver == false)
                {
                    if (IsCanvasContainsThatPolice(canvas, i))
                    {
                        IsGameOver = ColiitionCoin.CollisionPoliceWithCar(i);
                        if (IsGameOver == true)
                        {
                            GameManager.EndGame();
                            break;
                        }

                        Police.CheckBoundary(canvas, i);
                    }
                    else Police.AddItemEveryTwoSec(canvas, i);
                }
            }
        }

        private bool IsCanvasContainsThatPolice(Canvas canvas, int i)
        {
            return canvas.Children.Contains(Police.UIElements[i]);
        }

        public void StopTimers()
        {
            if (IsGameOver == true)
            {
                carTimer.Stop();
                policeTimer.Stop();
                coinTimer.Stop();
                lineTimer.Stop();
            }
        }

        public void StartTimers()
        {
            IsGameOver = false;
            carTimer.Start();
            policeTimer.Start();
            coinTimer.Start();
            lineTimer.Start();
        }
    }
}