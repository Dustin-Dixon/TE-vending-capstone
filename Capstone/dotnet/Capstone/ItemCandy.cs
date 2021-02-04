using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    public class ItemCandy : Item
    {

        public ItemCandy (string[] itemLine)
            : base(itemLine)
        {

        }

        public override string MakeSound()
        {
            string candySound = "Munch Munch, Yum!";
            return candySound;
        }

    }
}
