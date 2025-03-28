using System.Windows.Media.Animation;

namespace RootRemake_Project.CharacterClasses
{

    public abstract class Character
    {
        public int TotalWarriors;
        public int TotalBuildings;
        private Random rand = new Random();
        /// <summary>
        /// The order in which the characters set up
        /// In game rules set from A to Z but in code
        /// 0 will be A, B would be 1 and so on
        /// </summary>
        public int CharacterSetupOrder;
        public int VictoryPoints = 0;

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
        public int[] Battle(Character attacker, Character defender)
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



}
