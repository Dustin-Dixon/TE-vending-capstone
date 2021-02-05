using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Capstone
{
    public static class HelperMethods
    {
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
            Console.WriteLine("Not a valid option homie. Let's select an actual option this time.");
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

                Console.WriteLine($"Something ain't right about that there file path.");
            }
        }

        public static void LogMethod(string message)
        {
            string logFile = Directory.GetCurrentDirectory() + "\\Log.txt";
            try
            {
                using (StreamWriter sw = new StreamWriter(logFile, true))
                {
                    string fullMessage = $"{DateTime.Now:MM/dd/yyyy} {DateTime.Now:hh:mm:ss tt} {message}";
                    sw.WriteLine(fullMessage);
                }
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
