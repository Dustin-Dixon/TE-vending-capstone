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
                Console.WriteLine();
                Console.WriteLine($"(1) Display Vending Machine Items");
                Console.WriteLine($"(2) Purchase");
                Console.WriteLine($"(3) Exit");
                string userInput = Console.ReadLine();

                if (userInput == "1")
                {
                    ShowInventory(machine);
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

                }
                else
                {
                    Console.WriteLine("Wtf?  Must be 1 - 3");
                }

            }
        }

        static void ShowInventory(VendingMachine machine)
        {
            foreach (KeyValuePair<string, Item> item in machine.Inventory)
            {
                string location = item.Key;
                string itemName = item.Value.ProductName;
                decimal price = item.Value.Price;
                string quantity = item.Value.Quantity.ToString();
                if (quantity == "0")
                {
                    quantity = "SOLD OUT";
                }

                Console.WriteLine($"{location}|{itemName}|{price}|{quantity}");
            }
            Console.WriteLine("Press enter to continue after browsing the wonderful selection.");
            Console.ReadLine();
        }

        static void PurchaseMenu(VendingMachine machine, Bank bank)
        {
            bool toggle = false;
            while (!toggle)
            {
                Console.WriteLine();
                Console.WriteLine($"(1) Feed Money");
                Console.WriteLine($"(2) Select Product");
                Console.WriteLine($"(3) Finish Transaction");
                Console.WriteLine();
                Console.WriteLine($"Current Money Provided: ${bank.Balance:C2}");
                string userInput = Console.ReadLine();

                if (userInput == "1")
                {
                    FeedMoney(bank);
                }
                else if (userInput == "2")
                {
                    ShowInventory(machine);
                    Console.WriteLine($"Please select an item.");
                    string userSelection = Console.ReadLine();
                    PurchaseItem(userSelection, machine, bank);

                }
                else if (userInput == "3")
                {


                    toggle = true;
                }

            }

        }

        static void FeedMoney(Bank bank)
        {
            bool toggle = false;
            while (!toggle)
            {
                Console.WriteLine();
                Console.WriteLine($"Please insert whole dollar amounts.");
                Console.Write("$");
                string userInput = Console.ReadLine();
                try
                {
                    int userAmount = int.Parse(userInput);
                    bank.Balance += userAmount;
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
                        bank.Balance -= item.Value.Price;
                        Console.WriteLine();
                        Console.WriteLine($"{item.Value.ProductName}|{item.Value.Price}|${bank.Balance} remaining");
                        Console.WriteLine($"{item.Value.MakeSound()}");

                        //log method for purchases
                        //place interpolated string into Log method
                        string auditLine = $"{DateTime.Now.Date:MM/dd/yyyy} {DateTime.Now.Date:hh:mm:ss tt} " +
                            $"{item.Value.ProductName} {item.Key} ${beforeBalance} ${bank.Balance}";
                        LogMethod(auditLine);

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
                //inform wrong selection
                else
                {
                    Console.WriteLine("Please enter a selection shown.");
                }

            }
        }

        static void LogMethod(string message)
        {
            string logFile = Directory.GetCurrentDirectory() + "\\Log.txt";
            try
            {
                using (StreamWriter sw = new StreamWriter (logFile, true))
                {
                    sw.WriteLine(message);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Could not print to Log.txt file.");
                Console.WriteLine(e.Message);
            }
        }

    }
}
