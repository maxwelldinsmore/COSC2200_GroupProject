using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace RootRemake_Project
{
    public static class CardDeck
    {
        public static List<Card> cardDeck;

        static CardDeck()
        {
            InitializeCardDeck();
        }

        private static void InitializeCardDeck()
        {
            cardDeck = new List<Card>();

            // Example initialization of cards
            cardDeck.Add(new Card(1, "Example Card 1", 0, 0));
            cardDeck.Add(new Card(2, "Example Card 2", 1, 0));
            // Add more cards as needed
        }
    }
}
