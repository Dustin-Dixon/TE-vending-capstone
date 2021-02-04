using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    abstract public class Item
    {

        public string ProductName { get; private set; }
        public decimal Price { get; private set; }
        public string Type { get; private set; }
        public int Quantity { get; set; }

        public Item (string[] itemLine)
        {

            ProductName = itemLine[1];
            Price = decimal.Parse(itemLine[2]);
            Type = itemLine[3];
            Quantity = 5;

        }

        abstract public string MakeSound();

    }
}
