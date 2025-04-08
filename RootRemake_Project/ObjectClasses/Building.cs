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
        public Uri BuildingArt { get; set; }
        //TODO: Implement Building Art and decide on how to display it
        public Building(int playerID, string BuildingType) 
        { 
            this.playerID = playerID;
            this.BuildingType = BuildingType;
            BuildingKey = "Building_" + helperFunctions.GetRandomString();

            switch (BuildingType)
            {
                case "Roost":
                    BuildingArt = new Uri("pack://application:,,,/Assets/Eyrie/Roost.png", UriKind.RelativeOrAbsolute);
                    break;
                case "Sawmill":
                    BuildingArt = new Uri("pack://application:,,,/Assets/Marquis/Sawmill.png", UriKind.RelativeOrAbsolute);
                    break;
                case "Recruiter":
                    BuildingArt = new Uri("pack://application:,,,/Assets/Marquis/Recruiter.png", UriKind.RelativeOrAbsolute);
                    break;
                case "Workshop":
                    BuildingArt = new Uri("pack://application:,,,/Assets/Marquis/Workshop.png", UriKind.RelativeOrAbsolute);
                    break;
                default:
                    throw new ArgumentException("Invalid building type");
            }

        }



    }
}
