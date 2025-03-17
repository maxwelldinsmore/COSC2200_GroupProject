

namespace RootRemake_Project
{
    public class Card
    {
        public int Suit;
        public string CardText;
        public string CardArt;
        public int[] CraftCost;

        // Constructor
        public Card(int suit, string cardText, string cardArt, int[] craftCost)
        {
            Suit = suit;
            CardText = cardText;
            CardArt = cardArt;
            CraftCost = craftCost;
        }

        // Method to craft the card
        public void CraftCard()
        {
        }

    }
}
