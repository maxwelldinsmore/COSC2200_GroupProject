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
        // TODO: Declare all information for all the cards.
        private static Bitmap spriteSheet;
        private static int spriteWidth = 5330; // 10 columns * 533
        private static int spriteHeight = 4470; // 6 rows * 745
        private static int quadWidth = 533;    // Card width
        private static int quadHeight = 745;   // Card height
        private static int columnsPerRow = 10; // Columns per row (except the last row)
        private static int lastRowColumns = 3; // Columns in the last row
        private static string imagePath = "RootRemake_Project/Assets/CardDeck.png";

     
        static Card[] cardDeck =
        {
            new Card(0,0),
            new Card(0,1),
        };
       
    }
}