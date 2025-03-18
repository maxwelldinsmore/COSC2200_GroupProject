

using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace RootRemake_Project
{
    public class Card
    {
        public int Suit;
        public string CardText;
        public BitmapSource CardArt { get; set; }
        public int[] CraftCost;
        public int CardX;
        public int CardY;
        private static int spriteWidth = 5330; // 10 columns * 533
        private static int spriteHeight = 4470; // 6 rows * 745
        private static int quadWidth = 533;    // Card width
        private static int quadHeight = 745;   // Card height
        private static int columnsPerRow = 10; // Columns per row (except the last row)
        private static int lastRowColumns = 3; // Columns in the last row
        private static string imagePath = Path.Combine("pack://application:,,,/Assets/CardDeck.png");
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
