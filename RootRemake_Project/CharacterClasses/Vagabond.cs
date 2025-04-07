using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RootRemake_Project.CharacterClasses
{
    public class Vagabond : Player
    {

        public string Name { get; set; }
        public int RuinItems { get; set; }
        public Dictionary<string, int> FactionRank { get; set; }
        public List<string> Quests { get; set; }

        public Vagabond(string name) : base()
        {
            Name = name;
            RuinItems = 0;
            FactionRank = new Dictionary<string, int>();
            Quests = new List<string>();
            VictoryPointArt = new Uri("pack://application:,,,/Assets/Vagabond/VP.png", UriKind.RelativeOrAbsolute);

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

        public override void CharacterSetup()
        {
            throw new NotImplementedException();
        }

        public override void BirdSong()
        {
            throw new NotImplementedException();
        }

        public override void Daylight()
        {
            throw new NotImplementedException();
        }

        public override void Evening()
        {
            throw new NotImplementedException();
        }

        public override void Move()
        {
            throw new NotImplementedException();
        }

        public override string CharacterName()
        {
            throw new NotImplementedException();
        }
    }
}
