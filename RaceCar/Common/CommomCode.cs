using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace RaceCar
{
    public abstract class CommomCode
    {
        public Canvas Canvas { get; private set; }
        public Point Point { get; set; }
        public Stopwatch LastSpawn { get; set; }
        public List<Point> PointsList { get; set; }
        public List<Image> UIElements { get; set; }
        public short Xpoint { get; set; }
        public abstract byte Speed { get; set; }
        public byte NumberOfObjects { get; } = 4;

        public const short BottomBorder = 420;

        public CommomCode(Canvas canvas)
        {
            Canvas = canvas;

            UIElements = new List<Image>();
            PointsList = new List<Point>();

            LastSpawn = Stopwatch.StartNew();
            CreatSubject();
        }

        public abstract void CreatSubject();

        public abstract void AddItemEveryTwoSec(Canvas canvas, int i);

        public virtual void RmoveItemAndPointsFromCanavas()
        {
            foreach (Image item in UIElements)
                Canvas.Children.Remove(item);

            for (int i = 0; i < PointsList.Count; i++)
                PointsList[i] = new Point(CommonMetheds.NewPoints(), 10);
        }

        public virtual void CheckBoundary(Canvas canvas, int i)
        {
            if (PointsList[i].Y > BottomBorder)
            {
                canvas.Children.Remove(UIElements[i]);
                PointsList[i] = new Point(CommonMetheds.NewPoints(), 10);
            }
            else
                UpdatePointsSubject(canvas, i);
        }

        public virtual void UpdatePointsSubject(Canvas canvas, int i)
        {
            PointsList[i] = new Point(PointsList[i].X, PointsList[i].Y + Speed);
            Canvas.SetTop(UIElements[i], PointsList[i].Y);
            Canvas.SetLeft(UIElements[i], PointsList[i].X);
        }
    }
}