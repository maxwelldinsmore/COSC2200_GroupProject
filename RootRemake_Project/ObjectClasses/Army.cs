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
        /// Id of the player's character for doing character specific testing
        /// </summary>
        public int characterID { get; set; }
        public string warriorCount { get; set; }

        public string warriorArt { get; set; }

        public Army() { }
    }
}
