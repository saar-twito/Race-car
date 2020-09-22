using System;
using System.Windows;
using System.Windows.Controls;

namespace RaceCar
{
    public class Collision
    {
        public Car Car { get; private set; }
        public Coins Coins { get; private set; }
        public MainWindow MainWindow { get; private set; }
        public Canvas Canvas { get; private set; }
        public Police Police { get; private set; }
        public int CoinsCounter { get; private set; } = 0;
        public bool IsCollisionCoins { get; private set; }

        public Collision(Car car, Coins coins, MainWindow mainWindow, Canvas canvas, Police police)
        {
            Car = car;
            Coins = coins;
            MainWindow = mainWindow;
            Canvas = canvas;
            Police = police;
        }

        public void CollisionCoinsWithCar(int i)
        {
            Rect car = new Rect
            {
                Width = Car.CarImage.Width,
                Height = Car.CarImage.Height,
                X = Car.CarPoints.X,
                Y = Car.CarPoints.Y
            };
            Rect coins = new Rect
            {
                Width = Coins.UIElements[i].Width,
                Height = Coins.UIElements[i].Height,
                X = Coins.PointsList[i].X,
                Y = Coins.PointsList[i].Y
            };
            IsCollisionCoins = car.IntersectsWith(coins);

            IncreaseGoldCounter(i);
        }

        public bool CollisionPoliceWithCar(int i)
        {
            Rect car = new Rect
            {
                Width = Car.CarImage.Width,
                Height = Car.CarImage.Height,
                X = Car.CarPoints.X,
                Y = Car.CarPoints.Y
            };
            Rect police = new Rect
            {
                Width = Police.UIElements[i].Width,
                Height = Police.UIElements[i].Height,
                X = Police.PointsList[i].X,
                Y = Police.PointsList[i].Y
            };
            return car.IntersectsWith(police);
        }

        private void IncreaseGoldCounter(int i)
        {
            if (IsCollisionCoins == true)
            {
                Canvas.Children.Remove(Coins.UIElements[i]);
                Coins.PointsList[i] = new Point(CommonMetheds.NewPoints(), 10);

                MainWindow.GoldCounter.Text = Convert.ToString(++CoinsCounter);
                IsCollisionCoins = false;
            }
        }
    }
}