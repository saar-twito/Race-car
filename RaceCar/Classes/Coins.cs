using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace RaceCar
{
    public class Coins : CommomCode
    {
        public Coins(Canvas canvas) : base(canvas)
        {
        }

        public override byte Speed { get; set; } = 5;

        public override void AddItemEveryTwoSec(Canvas canvas, int i)
        {
            if (LastSpawn.Elapsed.TotalSeconds >= 2)
            {
                UpdatePointsSubject(canvas, i);
                canvas.Children.Add(UIElements[i]);
                LastSpawn.Restart();
            }
        }

        public override void CreatSubject()
        {
            for (int i = 0; i < NumberOfObjects; i++)
            {
                Image coinImage = new Image
                {
                    Width = 20,
                    Height = 20,
                    Source = new BitmapImage(new Uri("Images/Coins/Coin.png", UriKind.Relative))
                };
                Xpoint = CommonMetheds.NewPoints();

                Point = new Point(Xpoint, 10);
                Canvas.SetLeft(coinImage, Point.X);
                Canvas.SetTop(coinImage, Point.Y);

                UIElements.Add(coinImage);
                PointsList.Add(Point);
            }
        }

        public override void RmoveItemAndPointsFromCanavas()
        {
            base.RmoveItemAndPointsFromCanavas();
        }

        public override void CheckBoundary(Canvas canvas, int i)
        {
            base.CheckBoundary(canvas, i);
        }

        public override void UpdatePointsSubject(Canvas canvas, int i)
        {
            base.UpdatePointsSubject(canvas, i);
        }
    }
}