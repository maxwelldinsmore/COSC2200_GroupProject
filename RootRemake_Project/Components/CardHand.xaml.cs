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
        public event Action<int> CardClicked; // Event for card clicks
        
        public CardHand()
        {
            InitializeComponent();
        }

        public void DisplayHand(List<Card> hand)
        {
            CardsPanel.Children.Clear();

            for (int i = 0; i < hand.Count; i++)
            {
                int cardIndex = i; // Capture current index
                var card = hand[i];

                var cardControl = new Image()
                {
                    Source = card.GetCardImage(),
                    Height = 200,
                    Margin = new Thickness(5),
                    Tag = cardIndex // Store index instead of card object
                };

                // Keep your hover effects
                cardControl.MouseEnter += (s, e) => cardControl.Height = 220;
                cardControl.MouseLeave += (s, e) => cardControl.Height = 200;

                // Add click handler
                cardControl.MouseDown += (s, e) => 
                {
                    CardClicked?.Invoke(cardIndex);
                };

                CardsPanel.Children.Add(cardControl);
            }
        }
    }
}
    

