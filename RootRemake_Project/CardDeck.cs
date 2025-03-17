using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace RootRemake_Project
{
    internal static class CardDeck
    {
        private static Bitmap spriteSheet;
        private static int spriteWidth = 5330; // 10 columns * 533
        private static int spriteHeight = 4470; // 6 rows * 745
        private static int quadWidth = 533;    // Card width
        private static int quadHeight = 745;   // Card height
        private static int columnsPerRow = 10; // Columns per row (except the last row)
        private static int lastRowColumns = 3; // Columns in the last row

        static CardDeck()
        {
            // Load the sprite sheet from the specified path
            string imagePath = "RootRemake_Project/Assets/CardDeck.png";
            try
            {
                spriteSheet = new Bitmap(imagePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading sprite sheet: {ex.Message}");
                throw;
            }
        }

        public static Card? GetCard(int index) // Mark return type as nullable
        {
            // Calculate the row and column of the card
            int row = index / columnsPerRow;
            int col = index % columnsPerRow;

            // Check if the index is out of range (sixth row has only 3 columns)
            if (row == 5 && col >= lastRowColumns)
            {
                return null; // Return null for invalid indices
            }

            // Calculate the position of the card in the sprite sheet
            int x = col * quadWidth;
            int y = row * quadHeight;

            // Extract the card image
            Rectangle cropArea = new Rectangle(x, y, quadWidth, quadHeight);
            Bitmap cardImage = spriteSheet.Clone(cropArea, spriteSheet.PixelFormat);

            // Save the card image to a file or use it as needed
            string cardArtPath = $"card_{index}.png";
            cardImage.Save(cardArtPath);

            // Create a new Card object
            int suit = index % 4; // Example suit calculation
            string cardText = $"Card {index}";
            int[] craftCost = new int[] { 1, 2, 3 }; // Example crafting cost

            return new Card(suit, cardText, cardArtPath, craftCost);
        }
    }
}