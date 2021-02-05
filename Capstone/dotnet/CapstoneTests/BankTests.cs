using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace CapstoneTests
{
    [TestClass]
    public class BankTests
    {
        [TestMethod]
        //if statement before FeedMoneyTest() prevents negative integers
        //try-catch catches failed int.Parse of decimal inputs
        public void FeedMoneyTest()
        {
            Capstone.Bank bank = new Capstone.Bank();
            bank.FeedMoney(5);
            Assert.AreEqual(5.00M, bank.Balance);
        }

        //UpdateMoney methods will not bring the balance below 0, due to if statement
        [TestMethod]
        public void UpdateMoneyTestBalance()
        {
            Capstone.Bank bank = new Capstone.Bank();
            bank.Balance += 10;
            bank.UpdateMoney(5.35M);
            Assert.AreEqual(4.65M, bank.Balance);
        }
        [TestMethod]
        public void UpdateMoneyTestTotalSales()
        {
            Capstone.Bank bank = new Capstone.Bank();
            bank.Balance += 10;
            bank.UpdateMoney(5.35M);
            Assert.AreEqual(5.35M, bank.TotalSales);
        }

        //logic prior to method prevents a negative balance
        [TestMethod]
        public void MakeChangeTest()
        {
            Capstone.Bank bank = new Capstone.Bank();
            bank.Balance += 3.15M;
            string actual = bank.MakeChange();
            string expected = $"Your change is 12 quarter(s), 1 dime(s), and 1 nickel(s).";
            Assert.AreEqual(expected, actual);

            Capstone.Bank bankOne = new Capstone.Bank();
            bankOne.Balance += 1.95M;
            string actualOne = bankOne.MakeChange();
            string expectedOne = $"Your change is 7 quarter(s), 2 dime(s), and 0 nickel(s).";
            Assert.AreEqual(expectedOne, actualOne);
        }

    }
}
