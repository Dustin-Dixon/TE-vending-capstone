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

            //instantiate bank
            Bank bank = new Bank();

            //read inventory file and stock vending machine
            try
            {
                using (StreamReader sr = new StreamReader(inventoryList))
                {
                    while (!sr.EndOfStream)
                    {
                        //separate each line into an array
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

            HelperMethods.Greeting();

            //begin user interface of application
            MainMenu(bestVendingMachineEver, bank);
        }

        static void MainMenu(VendingMachine machine, Bank bank)
        {
            bool toggle = false;
            while (!toggle)
            {
                //print main menu and capture user selection
                string userInput = HelperMethods.PrintMainMenu();

                //print inventory option
                if (userInput == "1")
                {
                    List<string> inventory = machine.ShowInventory();
                    HelperMethods.PrintList(inventory);
                    Console.WriteLine();
                    Console.WriteLine("Press enter to continue after browsing the wonderful selection.");
                    Console.ReadLine();
                }

                //enter purchase menu option
                else if (userInput == "2")
                {
                    PurchaseMenu(machine, bank);
                }

                //exit application option
                else if (userInput == "3")
                {
                    toggle = true;
                }

                //*SECRET* sales report option
                else if (userInput == "4")
                {
                    decimal totalSales = bank.TotalSales;
                    List<string> salesList = machine.GenerateSalesList();
                    HelperMethods.LogSales(salesList, totalSales);
                }
                //if user input is invalid
                else
                {
                    Console.WriteLine();
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

                //print menu and acquire user selection
                string userInput = HelperMethods.PurchaseMenu(balance);

                //feed money option
                if (userInput == "1")
                {
                    FeedMoney(bank);
                }

                //purchase item option
                else if (userInput == "2")
                {
                    List<string> inventory = machine.ShowInventory();
                    HelperMethods.PrintList(inventory);
                    Console.WriteLine();
                    Console.WriteLine($"Please select an item.");
                    string userSelection = Console.ReadLine();
                    PurchaseItem(userSelection, machine, bank);
                }

                //end transaction option
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
                    Console.WriteLine();
                    HelperMethods.NotAnOption();
                }
            }
        }

        static void FeedMoney(Bank bank)
        {
            bool toggle = false;
            while (!toggle)
            {
                //acquire user desired money input
                string userInput = HelperMethods.AskForMoney();
                if (!userInput.Contains('-'))
                {
                    try
                    {
                        //if parse fails throw exception
                        int userAmount = int.Parse(userInput);
                        //add to balance
                        bank.FeedMoney(userAmount);
                        //create message for log and run log method
                        string message = $"FEED MONEY: {userAmount:C2} {bank.Balance:C2}";
                        HelperMethods.LogMethod(message);
                        toggle = true;
                    }
                    catch (Exception e)
                    {
                        //if input has decimal places or not a valid input
                        Console.WriteLine();
                        Console.WriteLine("Whole numbers dummy!");
                    }
                }
                //if input is negative
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Think positive.");
                }
            }
        }

        static void PurchaseItem(string userInput, VendingMachine machine, Bank bank)
        {
            //make input not case sensitive
            userInput = userInput.ToUpper();

            //if input found
            if (machine.Inventory.ContainsKey(userInput))
            {
                //define item for convenience
                Item item = machine.Inventory[userInput];

                if (item.Quantity == 0)
                {
                    //tell sold out
                    Console.WriteLine();
                    Console.WriteLine("Sorry, that item is sold out.");
                }
                //check if balance is enough for item cost
                else if (item.Price <= bank.Balance)
                {
                    //capture balance prior to update
                    decimal beforeBalance = bank.Balance;

                    //dispense item, adjust balance, and update inventory
                    bank.UpdateMoney(item.Price);
                    Console.WriteLine();
                    Console.WriteLine($"{item.ProductName}|{item.Price}|${bank.Balance} remaining");
                    Console.WriteLine($"{item.MakeSound()}");

                    //log method for purchases
                    //place interpolated string into Log method
                    string auditLine = $"{item.ProductName} {userInput} ${beforeBalance} ${bank.Balance}";
                    HelperMethods.LogMethod(auditLine);

                    //adjust item count for sales log
                    machine.Sales[item.ProductName]++;

                    //reduce quantity to reflect purchase
                    item.Quantity--;
                }
                //if balance is not enough
                else
                {
                    Console.WriteLine();
                    Console.WriteLine($"Please insert more money.  I'm hungry too.");
                }
            }
            //inform wrong selection
            else
            {
                Console.WriteLine();
                HelperMethods.NotAnOption();
            }
        }
    }
}
