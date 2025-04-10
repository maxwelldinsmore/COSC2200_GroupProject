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
        

        //Suit 1 is Wild, Suit 2 is Fox, Suit 3 is Bunny, Suit 4 is Rat.
        public List<int> attackDecree;
        public List<int> moveDecree;
        public List<int> buildDecree;
        public List<int> recruitDecree;

        public int AvailableRoosts { get; set; }
        public Eyrie(string UserName) 
        {
            this.UserName = UserName;
            Hand = new List<Card>();
            VictoryPoints = 0;
            // Implementation of Eyrie constructor
            BannerArt = new Uri("pack://application:,,,/Assets/Eyrie/Banner.png", UriKind.RelativeOrAbsolute);
            VictoryPointArt = new Uri("pack://application:,,,/Assets/Eyrie/VP.png", UriKind.RelativeOrAbsolute);

            BoardArt = new Uri("pack://application:,,,/Assets/Eyrie/Board.png", UriKind.RelativeOrAbsolute);

            WarriorArt = new Uri("pack://application:,,,/Assets/Eyrie/Warrior.png", UriKind.RelativeOrAbsolute);

            
            attackDecree = new List<int>();
            buildDecree = new List<int>();

            moveDecree = new List<int>();
            recruitDecree = new List<int>();

            // TEMP
            moveDecree.Add(1);
            attackDecree.Add(2);
            recruitDecree.Add(3);
            buildDecree.Add(4);
            moveDecree.Add(1);
            moveDecree.Add(2);
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
