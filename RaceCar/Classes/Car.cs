using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace RaceCar
{
    public class Car
    {
        public Canvas Canvas { get; private set; }
        public Image CarImage { get; set; }
        public Point CarPoints { get; set; }

        public byte LeftBorder { get; private set; } = 10;
        public int RightBorder { get; private set; } = 360;
        public byte TopBorder { get; private set; } = 70;
        public int BottomBorder { get; private set; } = 400;

        public Car(Canvas canvas)
        {
            Canvas = canvas;
        }

        public void CarManager(BitmapImage bitmapImage)
        {
            CarImage = new Image
            {
                Source = bitmapImage,
                Width = 70,
                Height = 70
            };

            CarPoints = new Point(195, 385);
            Canvas.SetLeft(CarImage, CarPoints.X);
            Canvas.SetTop(CarImage, CarPoints.Y);

            Canvas.Children.Add(CarImage);
        }
    }
}