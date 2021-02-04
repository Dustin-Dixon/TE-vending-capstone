using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    public class ItemGum : Item
    {

        public ItemGum(string[] itemLine)
            : base(itemLine)
        {

        }

        public override string MakeSound()
        {
            string gumSound = "Chew Chew, Yum!";
            return gumSound;
        }


    }
}
