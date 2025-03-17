

using System.Drawing;

namespace RootRemake_Project
{
    public class Card
    {
        /// <summary>
        /// Suit of card 0 to 4
        /// Wildcard 0, Bunny 1, Fox 2, Mouse 3
        /// </summary>
        public int Suit;
        public string CardText;
        public static Bitmap CardArt;
        public int[] CraftCost;
        public int CardX;
        public int CardY;
        private static int spriteWidth = 5330; // 10 columns * 533
        private static int spriteHeight = 4470; // 6 rows * 745
        private static int quadWidth = 533;    // Card width
        private static int quadHeight = 745;   // Card height
        private static int columnsPerRow = 10; // Columns per row (except the last row)
        private static int lastRowColumns = 3; // Columns in the last row
        private static string imagePath = "RootRemake_Project/Assets/CardDeck.png";
        private Bitmap CardSheet = new Bitmap(imagePath);

        // Constructor
        public Card(int suit, string cardText, int cardX, int cardY)
        {
            Suit = suit;
            CardX = cardX;
            CardY = cardY;
            CardText = cardText;
            CraftCost = [0,0,0];


            int x = CardX * quadWidth;
            int y = CardY * quadHeight;

            // Extract the card image
            Rectangle cropArea = new Rectangle(x, y, quadWidth, quadHeight);
            Bitmap CardArt = CardSheet.Clone(cropArea, CardSheet.PixelFormat);
        }

        // Method to craft the card
        public void CraftCard()
        {
        }

    }
}
