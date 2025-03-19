using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace RootRemake_Project.ObjectClasses
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

            //Suit 1 is Wild, Suit 2 is Fox, Suit 3 is Bunny, Suit 4 is Rat.

            //Row 1
            cardDeck.Add(new Card(1, "Saboteurs", 0, 0));
            cardDeck.Add(new Card(1, "Soup Kitchens", 1, 0));
            cardDeck.Add(new Card(1, "Boat Builders", 2, 0));
            cardDeck.Add(new Card(1, "Corvid Planners", 3, 0));
            cardDeck.Add(new Card(1, "Eyrie Émigré", 4, 0));
            cardDeck.Add(new Card(2, "Fox Partisans", 5, 0));
            cardDeck.Add(new Card(2, "Propaganda Bureau", 6, 0));
            cardDeck.Add(new Card(2, "False Orders", 7, 0));
            cardDeck.Add(new Card(2, "False Orders", 8, 0));
            cardDeck.Add(new Card(2, "Informants", 9, 0));

            //Row 2
            cardDeck.Add(new Card(2, "Informants", 0, 1));
            cardDeck.Add(new Card(3, "Rabbit Partisans", 1, 1));
            cardDeck.Add(new Card(3, "Tunnels", 2, 1));
            cardDeck.Add(new Card(3, "Tunnels", 3, 1));
            cardDeck.Add(new Card(3, "Charm Offensive", 4, 1));
            cardDeck.Add(new Card(3, "Coffin Makers", 5, 1));
            cardDeck.Add(new Card(3, "Swap Meet", 6, 1));
            cardDeck.Add(new Card(3, "Swap Meet", 7, 1));
            cardDeck.Add(new Card(4, "Mouse Partisans", 8, 1));
            cardDeck.Add(new Card(4, "League of Adventurous Mice", 9, 1));

            //Row 3
            cardDeck.Add(new Card(4, "League of Adventurous Mice", 0, 2));
            cardDeck.Add(new Card(4, "Murine Broker", 1, 2));
            cardDeck.Add(new Card(4, "Master Engravers", 2, 2));
            cardDeck.Add(new Card(1, "Saboteurs", 3, 2));
            cardDeck.Add(new Card(1, "Saboteurs", 4, 2));
            cardDeck.Add(new Card(1, "Woodland Runners", 5, 2));
            cardDeck.Add(new Card(1, "Arms Trader", 6, 2));
            cardDeck.Add(new Card(1, "Crossbow", 7, 2));
            cardDeck.Add(new Card(2, "Gently Used Knapsack", 8, 2));
            cardDeck.Add(new Card(2, "Root Tea", 9, 2));

            //Row 4
            cardDeck.Add(new Card(2, "Travel Gear", 0, 3));
            cardDeck.Add(new Card(2, "Protection Racket", 1, 3));
            cardDeck.Add(new Card(2, "Foxfolk Steel", 2, 3));
            cardDeck.Add(new Card(2, "Anvil", 3, 3));
            cardDeck.Add(new Card(1, "Birdy Bindle", 4, 3));
            cardDeck.Add(new Card(3, "Smuggler's Trail", 5, 3));
            cardDeck.Add(new Card(3, "Root Tea", 6, 3));
            cardDeck.Add(new Card(3, "A Visit to Friends", 7, 3));
            cardDeck.Add(new Card(3, "Bake Sale", 8, 3));
            cardDeck.Add(new Card(4, "Investments", 9, 3));

            //Row 5
            cardDeck.Add(new Card(4, "Travel Gear", 0, 4));
            cardDeck.Add(new Card(4, "Root Tea", 1, 4));
            cardDeck.Add(new Card(4, "Mouse-in-a-Sack", 2, 4));
            cardDeck.Add(new Card(4, "Sword", 3, 4));
            cardDeck.Add(new Card(4, "Crossbow", 4, 4));
            cardDeck.Add(new Card(2, "Ambush!", 5, 4));
            cardDeck.Add(new Card(3, "Ambush!", 6, 4));
            cardDeck.Add(new Card(4, "Ambush!", 7, 4));
            cardDeck.Add(new Card(1, "Ambush!", 8, 4));
            cardDeck.Add(new Card(1, "Ambush!", 9, 4));

            //Row 6
            cardDeck.Add(new Card(1, "Dominance", 0, 5));
            cardDeck.Add(new Card(2, "Dominance", 1, 5));
            cardDeck.Add(new Card(3, "Dominance", 2, 5));
            cardDeck.Add(new Card(4, "Dominance", 3, 5));



        }
    }
}
