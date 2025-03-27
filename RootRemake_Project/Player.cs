using RootRemake_Project.CharacterClasses;
using RootRemake_Project.ObjectClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RootRemake_Project
{
    public class Player
    {
        public string UserName;
        public Character character { get; set; }
        public int VictoryPoints;
        public List<Card> hand { get; set; }

        public Player(string UserName)
        {
            hand = new List<Card>();
            VictoryPoints = 0;
            this.UserName = UserName;
        }

        public void DrawCard(List<Card> deck)
        {
            if (deck.Count > 0)
            {
                Card drawnCard = deck[0];
                hand.Add(drawnCard);
                deck.RemoveAt(0);
            }
        }

        public void DiscardCard(Card card, List<Card> discardPile)
        {
            if (hand.Contains(card))
            {
                hand.Remove(card);
                discardPile.Add(card);
            }
        }
    }
}