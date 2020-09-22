using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace RaceCar
{
    public class Police : CommomCode
    {
        private const float Time = 1.2f;

        public override byte Speed { get; set; } = 7;

        public Police(Canvas canvas) : base(canvas)
        {
        }

        public override void CreatSubject()
        {
            for (int i = 0; i < NumberOfObjects; i++)
            {
                Point = new Point(CommonMetheds.NewPoints(), 10);

                Image policeImage = new Image
                {
                    Width = 70,
                    Height = 70,
                    Stretch = Stretch.Fill,
                    Source = new BitmapImage(new Uri("Images/Obstacle/PoliceCar.png", UriKind.Relative))
                };

                Canvas.SetLeft(policeImage, Point.X);
                Canvas.SetTop(policeImage, Point.Y);

                UIElements.Add(policeImage);
                PointsList.Add(Point);
            }
        }

        public override void RmoveItemAndPointsFromCanavas() => base.RmoveItemAndPointsFromCanavas();

        public override void CheckBoundary(Canvas canvas, int i) => base.CheckBoundary(canvas, i);

        public override void UpdatePointsSubject(Canvas canvas, int i) => base.UpdatePointsSubject(canvas, i);

        public override void AddItemEveryTwoSec(Canvas canvas, int i)
        {
            if (LastSpawn.Elapsed.TotalSeconds >= Time)
            {
                UpdatePointsSubject(canvas, i);
                canvas.Children.Add(UIElements[i]);
                LastSpawn.Restart();
            }
        }
    }
}