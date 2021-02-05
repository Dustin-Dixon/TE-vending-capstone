using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    public class VendingMachine
    {

        public Dictionary<string, Item> Inventory { get; set; } = new Dictionary<string, Item>();
        public Dictionary<string, int> Sales { get; set; } = new Dictionary<string, int>();

        public List<string> GenerateSalesList()
        {
            List<string> salesList = new List<string>();

            foreach (KeyValuePair<string, int> sale in Sales)
            {
                string message = $"{sale.Key}|{sale.Value}";
                salesList.Add(message);
            }
            return salesList;
        }

        public List<string> ShowInventory()
        {
            List<string> inventory = new List<string>();
            foreach (KeyValuePair<string, Item> item in Inventory)
            {
                string location = item.Key;
                string itemName = item.Value.ProductName;
                decimal price = item.Value.Price;
                string quantity = item.Value.Quantity.ToString();
                if (quantity == "0")
                {
                    quantity = "SOLD OUT";
                }

                inventory.Add($"{location}|{itemName}|{price}|{quantity}");
            }
            return inventory;
        }
    }
}
