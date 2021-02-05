using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CapstoneTests
{
    [TestClass]
    public class VendingMachineTests
    {
        [TestMethod]
        public void GenerateSalesListTest()
        {
            Capstone.VendingMachine machine = new Capstone.VendingMachine();
            machine.Sales["NotDoritos"] = 5;
            machine.Sales["NotFritos"] = 0;
            machine.Sales["NotLays"] = 3;
            List<string> actual = machine.GenerateSalesList();
            List<string> expected = new List<string>()
            {
                "NotDoritos|5", "NotFritos|0", "NotLays|3"
            };
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ShowInventoryTest()
        {
            Capstone.VendingMachine machine = new Capstone.VendingMachine();
            string[] drinkArray = new string[] { "A1", "Dr.Salty", "1.05", "drink" };
            string[] candyArray = new string[] { "C2", "Take20", "3.25", "candy" };
            Capstone.ItemDrink drink = new Capstone.ItemDrink(drinkArray);
            Capstone.ItemCandy candy = new Capstone.ItemCandy(candyArray);
            machine.Inventory["A1"] = drink;
            machine.Inventory["C2"] = candy;
            List<string> actual = machine.ShowInventory();
            List<string> expected = new List<string>()
            {
                "A1|Dr.Salty|1.05|5", "C2|Take20|3.25|5"
            };
            CollectionAssert.AreEqual(expected, actual);

        }

    }
}
