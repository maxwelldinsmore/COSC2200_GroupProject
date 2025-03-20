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
        public int UserName;
        private Character character { get; set; }
        public int VictoryPoints;
        public Card[] hand { get; set; }

        public Player()
        {


        }


        /// <summary>
        /// TODO: Ask Kyle about this
        /// Property for the character. 
        /// </summary>

    }
}
