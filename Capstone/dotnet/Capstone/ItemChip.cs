using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    public class ItemChip : Item
    {

        public ItemChip(string[] itemLine)
            : base(itemLine)
        {

        }

        public override string MakeSound()
        {
            string chipSound = "Crunch Crunch, Yum!";
            return chipSound;
        }


    }
}
