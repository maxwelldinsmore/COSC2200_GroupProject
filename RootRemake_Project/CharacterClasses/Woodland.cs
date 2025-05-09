﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RootRemake_Project.CharacterClasses
{
    internal class Woodland : Player
    {

        public Woodland(string playerName) : base()
        {
            UserName = playerName;
            // Implementation of Woodland constructor
            VictoryPointArt = new Uri("pack://application:,,,/Assets/Woodland/VP.png", UriKind.RelativeOrAbsolute);
            BoardArt = new Uri("pack://application:,,,/Assets/Woodland/Board.png", UriKind.RelativeOrAbsolute);
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
            return "Woodland";
        }
    }
}
