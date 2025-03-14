
namespace RootRemake_Project
{

    public class Character
    {
        public string characterName;
        public int TotalWarriors;
        public int TotalBuildings;
        
        /// <summary>
        /// The order in which the characters set up
        /// In game rules set from A to Z but in code
        /// 0 will be A, B would be 1 and so on
        /// </summary>
        public int CharacterSetupOrder;
    }
}
