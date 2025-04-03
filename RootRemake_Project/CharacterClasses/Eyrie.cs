using RootRemake_Project.ObjectClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RootRemake_Project.CharacterClasses
{
    internal class Eyrie : Player
    {
        public Eyrie(string UserName) 
        {
            this.UserName = UserName;
            Hand = new List<Card>();
            VictoryPoints = 0;
            // Implementation of Eyrie constructor
            BannerArt = new Uri("pack://application:,,,/Assets/Eyrie/Banner.png", UriKind.RelativeOrAbsolute);

        }
        public override void CharacterSetup()
        {
            // Implementation of CharacterSetup method
        }

        public override void BirdSong()
        {
            // Implementation of BirdSong method
        }

        public override void Daylight()
        {
            // Implementation of Daylight method
        }

        public override void Evening()
        {
            // Implementation of Evening method
        }

        public override void Move()
        {
            // Implementation of Move method
        }

        public override string CharacterName()
        {
            return "Eyrie";
        }
    }
}
