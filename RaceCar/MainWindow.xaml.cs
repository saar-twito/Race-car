using RaceCar.MessageInfo;
using System.Windows;

namespace RaceCar
{
    public partial class MainWindow : Window
    {
        public GameManager GameManager { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            GameManager = new GameManager(this, MyCanvas);
        }

        private void WhichCar_Click(object sender, RoutedEventArgs e)
        {
            GameManager.PickACar(sender);
        }

        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            switch (GameManager.IsUserSelected)
            {
                case true:
                    CollapseIrelevantUI();
                    GameManager.StartGame();
                    break;

                default:
                    MessageInformation.CarSelectionMessage();
                    break;
            }
        }

        private void CollapseIrelevantUI()
        {
            GameRules.Visibility = Visibility.Collapsed;
            StartGame.Visibility = Visibility.Collapsed;
            CarOption.Visibility = Visibility.Collapsed;
            ImagesOfCars.Visibility = Visibility.Collapsed;
            LittelRectangles.Visibility = Visibility.Collapsed;
        }

        private void GameRules_Click(object sender, RoutedEventArgs e)
        {
            new RulsWindow().Show();
        }
    }
}