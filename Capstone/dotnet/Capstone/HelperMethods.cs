using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Capstone
{
    public static class HelperMethods
    {
        static string logFile = Directory.GetCurrentDirectory() + "\\Log.txt";
        static StreamWriter sw = new StreamWriter(logFile);

        public static void Greeting()
        {
            Console.WriteLine("*** Welcome to the BEST VENDING MACHINE ever! ***");
            Console.WriteLine();
            Console.WriteLine("Please select an option.");
        }
        public static void PrintList(List<string> list)
        {
            foreach (string line in list)
            {
                Console.WriteLine($"{line}");
            }
        }

        public static string PrintMainMenu()
        {
            Console.WriteLine();
            Console.WriteLine($"(1) Display Vending Machine Items");
            Console.WriteLine($"(2) Purchase");
            Console.WriteLine($"(3) Exit");
            string userInput = Console.ReadLine();
            return userInput;
        }

        public static string PurchaseMenu(decimal balance)
        {
            Console.WriteLine();
            Console.WriteLine($"(1) Feed Money");
            Console.WriteLine($"(2) Select Product");
            Console.WriteLine($"(3) Finish Transaction");
            Console.WriteLine();
            Console.WriteLine($"Current Money Provided: {balance:C2}");
            string userInput = Console.ReadLine();
            return userInput;
        }

        public static void NotAnOption()
        {
            Console.WriteLine("Not a valid option. Please select an option listed.");
        }

        public static void LogSales(List<string> salesList, decimal sales)
        {
            string fullPath = Directory.GetCurrentDirectory() + $"\\{DateTime.Now:MM-dd-yyyy}_{DateTime.Now:hh-mm-sstt}_Sales_Log.txt";
            try
            {
                using (StreamWriter sw = new StreamWriter(fullPath))
                {
                    foreach (string line in salesList)
                    {
                        sw.WriteLine(line);
                    }
                    sw.WriteLine();
                    sw.WriteLine($"**TOTAL SALES** {sales:C2}");
                }
            }
            catch (Exception e)
            {

                Console.WriteLine($"Failed to create/find file path.");
            }
        }

        public static void LogMethod(string message)
        {            
            try
            {
                    string fullMessage = $"{DateTime.Now:MM/dd/yyyy} {DateTime.Now:hh:mm:ss tt} {message}";
                    sw.WriteLine(fullMessage);
                    sw.Flush();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Could not print to Log.txt file.");
                Console.WriteLine(e.Message);
            }
        }

        public static string AskForMoney()
        {
            Console.WriteLine();
            Console.WriteLine($"Please insert whole dollar amounts.");
            Console.Write("$");
            string userInput = Console.ReadLine();
            return userInput;
        }
    }
}
