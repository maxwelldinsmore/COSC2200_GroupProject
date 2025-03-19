using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RootRemake_Project.ObjectClasses
{
    
    internal class Item
    {
        private int ItemType;
        private string ItemArt;
        private string ItemName;
        /// <summary>
        /// 0 is not damaged, 1 is exhausted/used, 2 is damaged
        /// </summary>
        private int ItemQuality;

        public Item(int ItemType, string ItemArt, string ItemName)
        {
            this.ItemType = ItemType;
            this.ItemArt = ItemArt;
            this.ItemName = ItemName;
            ItemQuality = 0;
        }

        //TODO: Add code to resize the item art to fit on the board

    }
}
