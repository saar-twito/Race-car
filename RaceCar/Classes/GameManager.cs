using RaceCar.MessageInfo;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace RaceCar
{
    public class GameManager
    {
        public MainWindow MainWindow { get; private set; }
        public Canvas Canvas { get; private set; }
        public BitmapImage BitmapImage { get; private set; }
        public Car Car { get; private set; }
        public LineSegment LineSegment { get; private set; }
        public Coins Coins { get; private set; }
        public Collision ColiitionCoin { get; private set; }
        public Police Police { get; private set; }
        public MovmentManager MovmentManager { get; private set; }
        public bool IsUserSelected { get; private set; }
        public int Counter { get; private set; } = 0;

        public GameManager(MainWindow mainWindow, Canvas canvas)
        {
            MainWindow = mainWindow;
            Canvas = canvas;
        }

        public void StartGame()
        {
            InitializeClasses();
            LineSegment.DrawSubject(Canvas);
            MovmentManager.DrawCoins(Canvas);
            MovmentManager.DrawPolice(Canvas);
            Car.CarManager(BitmapImage);
            MovmentManager.StartTimers();
        }

        public void RestartGame()
        {
            Police.RmoveItemAndPointsFromCanavas();
            Coins.RmoveItemAndPointsFromCanavas();
            LineSegment.RmoveTheCoinsFromCanavas();
            Car.CarImage.Source = null;

            MessageBox.Show("Great! Lets start", "Race Car");
            MainWindow.GameRules.Visibility = Visibility.Visible;
            MainWindow.StartGame.Visibility = Visibility.Visible;
            MainWindow.CarOption.Visibility = Visibility.Visible;
            MainWindow.ImagesOfCars.Visibility = Visibility.Visible;
        }

        public void EndGame()
        {
            MovmentManager.StopTimers();

            switch (MessageInformation.WhenGameEnds())
            {
                case MessageBoxResult.Yes:
                    RestartGame();
                    break;

                case MessageBoxResult.No:
                    MessageInformation.GoodByMessage();
                    Application.Current.Shutdown();
                    break;
            }
        }

        private void InitializeClasses()
        {
            if (Counter == 0)
            {
                Car = new Car(Canvas);
                LineSegment = new LineSegment(Canvas);
                Coins = new Coins(Canvas);
                Police = new Police(Canvas);
                ColiitionCoin = new Collision(Car, Coins, MainWindow, Canvas, Police);
                MovmentManager = new MovmentManager(Car, LineSegment, Coins, Canvas, ColiitionCoin, Police, this);
                Counter++;
            }
        }

        public void PickACar(object sender)
        {
            if (sender != null)
            {
                switch (((RadioButton)sender).Content.ToString())
                {
                    case "G Car":
                        BitmapImage = new BitmapImage(new Uri("Images/Car/Green.png", UriKind.Relative));
                        break;

                    case "B Car":
                        BitmapImage = new BitmapImage(new Uri("Images/Car/Blue.png", UriKind.Relative));
                        break;

                    case "R Car":
                        BitmapImage = new BitmapImage(new Uri("Images/Car/RedCar.png", UriKind.Relative));
                        break;
                }
                IsUserSelected = true;
            }
        }
    }
}