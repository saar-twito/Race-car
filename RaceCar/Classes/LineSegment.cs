using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace RaceCar
{
    public class LineSegment
    {
        private const int NumberOfLine = 3;
        private const int ButtomBorder = 440;

        public Canvas Canvas { get; private set; }
        public Point Point { get; private set; }
        public DateTime LastSpawn { get; private set; }
        public List<Point> PointsList { get; private set; }
        public List<Rectangle> UIElements { get; private set; }

        public int Speed { get; private set; } = 7;

        public LineSegment(Canvas canvas)
        {
            Canvas = canvas;
            UIElements = new List<Rectangle>();
            PointsList = new List<Point>();

            LastSpawn = DateTime.Now;
            CreatSubject();
        }

        public void CreatSubject()
        {
            for (int i = 0; i < NumberOfLine; i++)
            {
                var rectangle = new Rectangle
                {
                    Width = 10,
                    Height = 110,
                    Fill = new SolidColorBrush(Colors.White)
                };

                Point = new Point(225, -120);
                Canvas.SetLeft(rectangle, Point.X);
                Canvas.SetTop(rectangle, Point.Y);

                UIElements.Add(rectangle);
                PointsList.Add(Point);
            }
        }

        public void RmoveTheCoinsFromCanavas()
        {
            foreach (var item in UIElements)
                Canvas.Children.Remove(item);
        }

        public void DrawSubject(Canvas canvas)
        {
            for (int i = 0; i < UIElements.Count; i++)
            {
                if (canvas.Children.Contains(UIElements[i]))
                    CheckBoundary(canvas, i);
                else
                    AddItemEveryTwoSec(canvas, i);
            }
        }

        public void AddItemEveryTwoSec(Canvas canvas, int i)
        {
            if (CheckIf2SecPassed())
            {
                UpdatePointsSubject(canvas, i);
                canvas.Children.Add(UIElements[i]);
                LastSpawn = DateTime.Now;
            }
        }

        private bool CheckIf2SecPassed() =>
            Convert.ToByte((DateTime.Now - LastSpawn).TotalSeconds) >= 2;

        public void CheckBoundary(Canvas canvas, int i)
        {
            if (PointsList[i].Y > ButtomBorder)
            {
                canvas.Children.Remove(UIElements[i]);
                PointsList[i] = new Point(Point.X, Point.Y + Speed);
            }
            else
                UpdatePointsSubject(canvas, i);
        }

        public void UpdatePointsSubject(Canvas canvas, int i)
        {
            PointsList[i] = new Point(PointsList[i].X, PointsList[i].Y + Speed);
            Canvas.SetTop(UIElements[i], PointsList[i].Y);
        }
    }
}