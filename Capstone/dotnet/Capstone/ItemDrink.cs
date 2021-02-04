using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    public class ItemDrink : Item
    {

        public ItemDrink(string[] itemLine)
            : base(itemLine)
        {

        }

        public override string MakeSound()
        {
            string drinkSound = "Glug Glug, Yum!";
            return drinkSound;
        }


    }
}
