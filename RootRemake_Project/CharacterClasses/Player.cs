using RootRemake_Project.ObjectClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RootRemake_Project.CharacterClasses
{
    public abstract class Player
    {

        public string UserName { get; set; }
        public int TotalWarriors;
        public int TotalBuildings;
        public Uri warriorArt;
        private Random rand = new Random();
        /// <summary>
        /// The order in which the characters set up
        /// In game rules set from A to Z but in code
        /// 0 will be A, B would be 1 and so on
        /// </summary>
        public int CharacterSetupOrder;
        public int VictoryPoints { get; set; }

        public List<Card> Hand { get; set; }

        public void DrawCard(List<Card> deck)
        {
            if (deck.Count > 0)
            {
                Card drawnCard = deck[0];
                Hand.Add(drawnCard);
                deck.RemoveAt(0);
            }
        }

        public void DiscardCard(Card card, List<Card> discardPile)
        {
            if (Hand.Contains(card))
            {
                Hand.Remove(card);
                discardPile.Add(card);
            }
        }

        abstract public void CharacterSetup();

        abstract public void BirdSong();

        abstract public void Daylight();

        abstract public void Evening();

        abstract public void Move();

        /// <summary>
        /// The battle function takes two Players in a location and has them fight
        /// rolls are 0-3 equal odds, and two dice are rolled, attacking player gets 
        /// advantage (gets higher number roll) in expection to woodland alliance where they
        /// get advantage if they are the defender
        /// 
        /// </summary>
        /// <returns>int[] {damage attacker takes, damage defender takes}</returns>
        /// //TODO: add condition for woodland alliance
        public int[] Battle(Player attacker, Player defender)
        {
            int roll1 = rand.Next(0, 4);
            int roll2 = rand.Next(0, 4);
            if (roll1 > roll2)
            {
                return new int[] { roll1, roll2 };
            }
            else
            {
                return new int[] { roll2, roll1 };
            }
        }

        /// <summary>
        /// Gets Name of the Character
        /// </summary>
        abstract public string CharacterName();


    }

    public class TurnManager
    {
        private List<Player> players;
        private int currentPlayerIndex = 0;
        public TurnManager(List<Player> players)
        {
            this.players = players;
        }

        /// <summary>
        /// Changes the player turn to the next in the list
        /// </summary>
        public void NextTurn()
        {
            currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
        }

        /// <summary>
        /// Changes to Morning to Daylight to Evening 
        /// </summary>
        public void StartDayCycle()
        {
            foreach (var player in players)
            {
                player.BirdSong();
            }

            foreach (var player in players)
            {
                player.Daylight();
            }

            foreach (var player in players)
            {
                player.Evening();
            }

        }

    }
}
