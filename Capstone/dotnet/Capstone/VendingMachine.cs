using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    class VendingMachine
    {

        public Dictionary<string, Item> Inventory { get; set; } = new Dictionary<string, Item>();
        public Dictionary<string, int> Sales { get; set; } = new Dictionary<string, int>();

    }
}
