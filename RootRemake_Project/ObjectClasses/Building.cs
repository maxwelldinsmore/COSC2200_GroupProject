using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RootRemake_Project.ObjectClasses
{
    public class Building
    {
        public int BuildingID;
        /// <summary>
        /// Id Of 
        /// </summary>
        public int playerID;
        /// <summary>
        /// 0 = Base Type, 1 = Sawmill, 2 = Recruiter, 3 = Workshop If built by Marquis de Cat
        /// </summary>
        public int BuildingType = 0;
        public int CharacterID;
        public string BuildingArt = "";
        //TODO: Implement Building Art and decide on how to display it
        public Building(int playerID, int CharacterID, int BuildingType = 0) 
        { 
            this.playerID = playerID;
            this.CharacterID = CharacterID;
        }

    }
}
