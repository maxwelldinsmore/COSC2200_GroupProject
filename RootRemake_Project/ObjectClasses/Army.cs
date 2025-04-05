using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RootRemake_Project.ObjectClasses
{
    public class Army
    {
        public string playerID { get; set; }

        /// <summary>
        /// Id of the player's Character for doing Character specific testing
        /// </summary>
        public int characterID { get; set; }
        public string WarriorCount { get; set; }

        public string warriorImageArtName { get; set; }

        public string WarriorArt { get; set; }

        public Army() { }
    }
}
