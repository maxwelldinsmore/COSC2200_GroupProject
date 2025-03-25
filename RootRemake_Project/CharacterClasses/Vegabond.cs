using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RootRemake_Project.CharacterClasses
{
    public class Vegabond
    {

        public string Name { get; set; }
        public int RuinItems { get; set; }
        public Dictionary<string, int> FactionRank { get; set; }
        public List<string> Quests { get; set; }

        public Vegabond(string name)
        {
            Name = name;
            RuinItems = 0;
            FactionRank = new Dictionary<string, int>();
            Quests = new List<string>();

        }

        public void ExploreRuin()
        {
            RuinItems++;

        }

        public void ChangeReputation(string faction, int amount)
        {
            if (!FactionRank.ContainsKey(faction))
                FactionRank[faction] = 0;

            FactionRank[faction] += amount;
        }

        public void CompleteQuest()
        {
            Quests.Add(Quests[0]);
        }


    }
}
