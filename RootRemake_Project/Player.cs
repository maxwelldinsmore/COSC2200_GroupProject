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
        private Character character { get; set; }
        public int VictoryPoints;
        public List<Card> hand { get; set; }

        public Player(string UserName)
        {
            hand = new List<Card>();
            VictoryPoints = 0;
            this.UserName = UserName;
        }



    }
}
