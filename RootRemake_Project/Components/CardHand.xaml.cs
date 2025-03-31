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
        public CardHand()
        {
            InitializeComponent();
        }

        //TODO: handle more than 5 cards
        public void DisplayHand(List<Card> hand)
        {
            CardsPanel.Children.Clear();

            foreach (var card in hand)
            {
                var cardControl = new Image()
                {
                    Source = card.GetCardImage(),
                    Height = 200,
                    Margin = new Thickness(5),
                    Tag = card // Store card reference for click handling
                };

                // Optional hover effect
                cardControl.MouseEnter += (s, e) => cardControl.Height = 220;
                cardControl.MouseLeave += (s, e) => cardControl.Height = 200;

                CardsPanel.Children.Add(cardControl);
            }
        }
    }
}
    

