using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    public class Bank
    {

        public decimal Balance { get; set; }
        public decimal TotalSales { get; set; }

        //customer inserting money
        public void FeedMoney(int dollars)
        {
            Balance += (decimal)dollars;
        }

        //customer making a purchase
        public void UpdateMoney(decimal price)
        {
            Balance -= price;
            TotalSales += price;
        }

    }
}
