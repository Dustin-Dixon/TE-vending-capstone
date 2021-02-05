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
            Balance += dollars;
        }

        //customer making a purchase
        public void UpdateMoney(decimal price)
        {
            Balance -= price;
            TotalSales += price;
        }

        public string MakeChange()
        {
            int quarter = 0;
            int dime = 0;
            int nickel = 0;

            while (Balance != 0)
            {
                if (Balance >= .25M)
                {
                    Balance -= .25M;
                    quarter++;
                }
                else if (Balance >= .1M)
                {
                    Balance -= .1M;
                    dime++;
                }
                else if (Balance >= .05M)
                {
                    Balance -= .05M;
                    nickel++;
                }
            }

            string message = $"Your change is {quarter} quarter(s), {dime} dime(s), and {nickel} nickel(s).";
            return message;
        }
    }
}
