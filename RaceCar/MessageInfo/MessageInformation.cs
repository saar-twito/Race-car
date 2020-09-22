using System.Windows;

namespace RaceCar.MessageInfo
{
    public static class MessageInformation
    {
        public static void GoodByMessage()
        {
            _ = MessageBox.Show("Oh well,it was great time with you 😁", "Race Car");
        }

        public static MessageBoxResult WhenGameEnds()
        {
            return MessageBox.Show(messageBoxText: "Would you like restart the game?", caption: "Race Car", button: MessageBoxButton.YesNo);
        }

        public static void CarSelectionMessage()
        {
            MessageBox.Show("Please pick a car first", "Pick a car", MessageBoxButton.OK);
        }
    }
}