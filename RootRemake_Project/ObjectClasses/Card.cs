using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace RootRemake_Project.ObjectClasses
{
    public class Card
    {
        /// <summary>
        /// Suit 1 is Wild, Suit 2 is Fox, Suit 3 is Bunny, Suit 4 is Rat.
        /// </summary>
        public int Suit;
        public string CardText;
        public BitmapSource CardArt { get; set; }
        public int[] CraftCost;
        public int CardX;
        public int CardY;
        // 10 columns * 533 6 rows * 745

        // TODO: Pixel Count slightly off so will show part of a card and another card
        private readonly static int quadWidth = 537;    // Card width
        private readonly static int quadHeight = 749;   // Card height
        private static BitmapImage CardSheet;

        static Card()
        {
            try
            {
                Uri imageUri = new Uri("pack://application:,,,/Assets/CardDeck.png", UriKind.RelativeOrAbsolute);
                CardSheet = new BitmapImage(imageUri);
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log the error)
                Console.WriteLine($"Error loading card sheet: {ex.Message}");
            }
        }


        // Constructor
        public Card(int suit, string cardText, int cardX, int cardY)
        {
            Suit = suit;
            CardX = cardX;
            CardY = cardY;
            CardText = cardText;
            CraftCost = new int[] { 0, 0, 0 };

            int x = CardX * quadWidth;
            int y = CardY * quadHeight;

            // Extract the card image
            CardArt = new CroppedBitmap(CardSheet, new Int32Rect(x, y, quadWidth, quadHeight));
        }

        public BitmapSource GetCardImage()
        {
            if (CardSheet == null)
            {
                throw new InvalidOperationException("Card sheet image not loaded.");
            }

            Int32Rect rect = new Int32Rect(CardX * quadWidth, CardY * quadHeight, quadWidth, quadHeight);
            return new CroppedBitmap(CardSheet, rect);
        }
    }

    
}
