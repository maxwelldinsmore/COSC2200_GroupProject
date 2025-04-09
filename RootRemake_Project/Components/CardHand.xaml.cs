using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RootRemake_Project.ObjectClasses;

namespace RootRemake_Project.Components
{
    /// <summary>
    /// Interaction logic for CardHand.xaml
    /// </summary>
    public partial class CardHand : UserControl
    {
        public event Action<string>? CardClicked; // Declare the event as nullable

        public CardHand()
        {
            InitializeComponent();
        }

        public void DisplayHand(List<Card> hand)
        {
            CardsPanel.Children.Clear();

            const int cardsPerRow = 5;
            int numRows = (int)Math.Ceiling((double)hand.Count / cardsPerRow);

            for (int row = 0; row < numRows; row++)
            {
                var rowPanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    Margin = new Thickness(0, 5, 0, 5)
                };

                // Insert new rows at the top so they appear above the previous ones
                CardsPanel.Children.Insert(0, rowPanel);

                for (int col = 0; col < cardsPerRow; col++)
                {
                    int cardIndex = row * cardsPerRow + col;
                    if (cardIndex >= hand.Count) break;

                    var card = hand[cardIndex];

                    var cardControl = new Image()
                    {
                        Source = card.GetCardImage(),
                        Height = 200,
                        Margin = new Thickness(5),
                        Opacity = 1,
                        Tag = card.CardKey
                    };

                    cardControl.MouseEnter += (s, e) => cardControl.Height = 250;
                    cardControl.MouseLeave += (s, e) => cardControl.Height = 220;
                    cardControl.MouseDown += (s, e) =>
                    {
                        CardClicked?.Invoke(card.CardKey);
                    };

                    rowPanel.Children.Add(cardControl);
                }
            }
        }
    }
}
    

