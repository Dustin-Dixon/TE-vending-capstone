using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CapstoneTests
{
    [TestClass]
    public class ItemTests
    {
        [TestMethod]
        public void TestCandyMakeSound()
        {
            string[] setType = new string[] { "", "", "0.0", "" };
            Capstone.ItemCandy candy = new Capstone.ItemCandy(setType);
            string actual = candy.MakeSound();
            string expected = "Munch Munch, Yum!";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestChipMakeSound()
        {
            string[] setType = new string[] { "", "", "0.0", "" };
            Capstone.ItemChip chip = new Capstone.ItemChip(setType);
            string actual = chip.MakeSound();
            string expected = "Crunch Crunch, Yum!";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestDrinkMakeSound()
        {
            string[] setType = new string[] { "", "", "0.0", "" };
            Capstone.ItemDrink drink = new Capstone.ItemDrink(setType);
            string actual = drink.MakeSound();
            string expected = "Glug Glug, Yum!";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestGumMakeSound()
        {
            string[] setType = new string[] { "", "", "0.0", "" };
            Capstone.ItemGum gum = new Capstone.ItemGum(setType);
            string actual = gum.MakeSound();
            string expected = "Chew Chew, Yum!";
            Assert.AreEqual(expected, actual);
        }
    }
}
