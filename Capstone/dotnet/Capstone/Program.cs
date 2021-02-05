using System;
using System.Collections.Generic;
using System.IO;

namespace Capstone
{
    class Program
    {
        static void Main(string[] args)
        {

            string inventoryList = Directory.GetCurrentDirectory() + "\\vendingmachine.csv";
            //instantiate vending machine (the best btw)
            VendingMachine bestVendingMachineEver = new VendingMachine();
            Bank bank = new Bank();

            //try at some point the below shorthand
            //Dictionary<string, Item> inventory = bestVendingMachineEver.Inventory;

            try
            {
                using (StreamReader sr = new StreamReader(inventoryList))
                {
                    while (!sr.EndOfStream)
                    {
                        string[] itemInfo = sr.ReadLine().Split('|');

                        //variable for item location
                        string location = itemInfo[0];

                        //[0] - location, [1] - name, [2] - price, [3] - type
                        //instantiate proper item classes and assign to vending machine's dictionary of inventory
                        if (itemInfo[3].ToLower() == "candy")
                        {
                            ItemCandy candy = new ItemCandy(itemInfo);
                            bestVendingMachineEver.Inventory.Add(location, candy);
                        }
                        else if (itemInfo[3].ToLower() == "chip")
                        {
                            ItemChip chip = new ItemChip(itemInfo);
                            bestVendingMachineEver.Inventory.Add(location, chip);
                        }
                        else if (itemInfo[3].ToLower() == "drink")
                        {
                            ItemDrink drink = new ItemDrink(itemInfo);
                            bestVendingMachineEver.Inventory.Add(location, drink);
                        }
                        else if (itemInfo[3].ToLower() == "gum")
                        {
                            ItemGum gum = new ItemGum(itemInfo);
                            bestVendingMachineEver.Inventory.Add(location, gum);
                        }

                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Not a valid file path, yo");
                Console.WriteLine(e.Message);
            }

            //set up vending machine's sales dictionary
            foreach (KeyValuePair<string, Item> item in bestVendingMachineEver.Inventory)
            {
                string productName = item.Value.ProductName;
                bestVendingMachineEver.Sales[productName] = 0;
            }

            MainMenu(bestVendingMachineEver, bank);


        }

        static void MainMenu(VendingMachine machine, Bank bank)
        {
            bool toggle = false;
            while (!toggle)
            {
                string userInput = HelperMethods.PrintMainMenu();

                if (userInput == "1")
                {
                    List<string> inventory = machine.ShowInventory();
                    HelperMethods.PrintList(inventory);
                    Console.WriteLine();
                    Console.WriteLine("Press enter to continue after browsing the wonderful selection.");
                    Console.ReadLine();
                }
                else if (userInput == "2")
                {
                    PurchaseMenu(machine, bank);
                }
                else if (userInput == "3")
                {
                    toggle = true;
                }
                else if (userInput == "4")
                {
                    decimal totalSales = bank.TotalSales;
                    List<string> salesList = machine.GenerateSalesList();
                    HelperMethods.LogSales(salesList, totalSales);
                }
                else
                {
                    HelperMethods.NotAnOption();
                }

            }
        }

        static void PurchaseMenu(VendingMachine machine, Bank bank)
        {
            bool toggle = false;

            while (!toggle)
            {
                decimal balance = bank.Balance;

                string userInput = HelperMethods.PurchaseMenu(balance);

                if (userInput == "1")
                {
                    FeedMoney(bank);
                }
                else if (userInput == "2")
                {
                    List<string> inventory = machine.ShowInventory();
                    HelperMethods.PrintList(inventory);
                    Console.WriteLine($"Please select an item.");
                    string userSelection = Console.ReadLine();
                    PurchaseItem(userSelection, machine, bank);
                }
                else if (userInput == "3")
                {
                    decimal leftoverBalance = bank.Balance;
                    string change = bank.MakeChange();
                    Console.WriteLine(change);
                    string message = $"GIVE CHANGE: {leftoverBalance:C2} {bank.Balance:C2}";
                    HelperMethods.LogMethod(message);
                    toggle = true;
                }
                else
                {
                    HelperMethods.NotAnOption();
                }

            }

        }

        static void FeedMoney(Bank bank)
        {
            bool toggle = false;
            while (!toggle)
            {
                string userInput = HelperMethods.AskForMoney();
                try
                {
                    int userAmount = int.Parse(userInput);
                    bank.FeedMoney(userAmount);
                    string message = $"FEED MONEY: {userAmount:C2} {bank.Balance:C2}";
                    HelperMethods.LogMethod(message);
                    toggle = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine();
                    Console.WriteLine("Whole numbers dummy!");
                }
            }
        }

        static void PurchaseItem(string userInput, VendingMachine machine, Bank bank)
        {
            userInput = userInput.ToUpper();

            if (machine.Inventory.ContainsKey(userInput))
            {
                foreach (KeyValuePair<string, Item> item in machine.Inventory)
                {
                    //if selection is valid
                    if (item.Key == userInput)
                    {
                        if (item.Value.Quantity == 0)
                        {
                            //tell sold out
                            Console.WriteLine("Sorry, that item is sold out.");
                        }
                        //check if balance is enough for item cost
                        else if (item.Value.Price <= bank.Balance)
                        {
                            decimal beforeBalance = bank.Balance;

                            //dispense item and update inventory
                            bank.UpdateMoney(item.Value.Price);
                            Console.WriteLine();
                            Console.WriteLine($"{item.Value.ProductName}|{item.Value.Price}|${bank.Balance} remaining");
                            Console.WriteLine($"{item.Value.MakeSound()}");

                            //log method for purchases
                            //place interpolated string into Log method
                            string auditLine = $"{item.Value.ProductName} {item.Key} ${beforeBalance} ${bank.Balance}";
                            HelperMethods.LogMethod(auditLine);

                            //log for sales of item count
                            machine.Sales[item.Value.ProductName]++;

                            //reduce quantity to reflect purchase
                            item.Value.Quantity--;
                        }
                        //if balance is not enough
                        else
                        {
                            Console.WriteLine($"Please insert more money.  I'm hungry too.");
                        }
                    }
                }
            }
            //inform wrong selection
            else
            {
                HelperMethods.NotAnOption();
            }
        }
    }
}
