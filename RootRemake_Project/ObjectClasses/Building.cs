using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RootRemake_Project.ObjectClasses
{
    public class Building
    {
        /// <summary>
        /// Id Of player
        /// </summary>
        public int playerID { get; set; }
        /// <summary>
        /// 0 = Base Type, 1 = Sawmill, 2 = Recruiter, 3 = Workshop If built by Marquis de Cat
        /// </summary>
        public string BuildingType { get; set; }
        public string BuildingKey { get; set; }
        public Uri BuildingArt;
        //TODO: Implement Building Art and decide on how to display it
        public Building(int playerID, string BuildingType) 
        { 
            this.playerID = playerID;
            this.BuildingType = BuildingType;
            this.BuildingKey = helperFunctions.GetRandomString();
        }



    }
}
