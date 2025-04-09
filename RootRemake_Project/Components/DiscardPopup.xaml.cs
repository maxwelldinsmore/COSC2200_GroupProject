using RootRemake_Project.ObjectClasses;
using System.Windows.Controls;
using System.Windows;

namespace RootRemake_Project.Components
{
    public partial class DiscardPopup : UserControl
    {
        public event Action<bool> DialogResult;
        private Card _currentCard;

        public DiscardPopup()
        {
            InitializeComponent();
        }

        // Mode 1: Initial warning (no card selection)
        public void ShowWarning(string message)
        {
            HeaderText.Text = message;
            CardDisplayBorder.Visibility = Visibility.Collapsed;
            ActionButton1.Visibility = Visibility.Collapsed;
            ActionButton2.Content = "OK";
            ActionButton2.Visibility = Visibility.Visible;
            Visibility = Visibility.Visible;
        }

        // Mode 2: Card discard confirmation
        public void ShowCardDiscard(Card card, string header)
        {
            _currentCard = card;
            HeaderText.Text = header;
            CardImage.Source = card.GetCardImage();
            CardInfoText.Text = $"{card.CardText}\nSuit: {GetSuitName(card.Suit)}";

            CardDisplayBorder.Visibility = Visibility.Visible;
            ActionButton1.Content = "Discard";
            ActionButton1.Visibility = Visibility.Visible;
            ActionButton2.Content = "Keep";
            ActionButton2.Visibility = Visibility.Visible;

            Visibility = Visibility.Visible;
        }

        private void ActionButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            bool result = button.Content.ToString() == "Discard" ||
                         button.Content.ToString() == "Confirm";

            DialogResult?.Invoke(result);
            Visibility = Visibility.Collapsed;
        }

        private string GetSuitName(int suit)
        {
            return suit switch
            {
                1 => "Bird (Wild)",
                2 => "Fox",
                3 => "Rabbit",
                4 => "Mouse",
                _ => "Unknown"
            };
        }
    }
}