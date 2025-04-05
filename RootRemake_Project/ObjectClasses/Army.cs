using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RootRemake_Project.ObjectClasses
{
    public class Army
    {
        public int PlayerID { get; set; }

        public int WarriorCount { get; set; }

        public Uri warriorImageArtName { get; set; }

        public string KeyString { get; set; } // The key string for the army

        

        public Army(int playerID, Uri imageArt, int warriorCount, string keyString) 
        {
            this.PlayerID = playerID;
            this.WarriorCount = warriorCount;
            this.warriorImageArtName = imageArt;
            this.KeyString = keyString;
        }
    }
}
