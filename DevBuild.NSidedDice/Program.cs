using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace DevBuild.NSidedDice
{
    class Program
    {
        public const int NUMBER_OF_DICE = 2;

        static void Main(string[] args)
        {
            string userResponse = "";
            List<int> diceValues = new List<int>();
            int diceNumSides = 0, diceTotal = 0;
            bool userContinuing = true;

            while (userContinuing)
            {
                //Greet the user
                Console.WriteLine(StringTable.HelloString);
                //Ask the user to enter the number of sides for a pair of dice
                // between 2 (coin) and 120 (disdyakis triacontahedron)
                while (diceNumSides < 2 || diceNumSides > 120)
                {
                    Console.Write(StringTable.DiceSidesPrompt + " ");
                    int.TryParse(Console.ReadLine(), out diceNumSides);
                    if (diceNumSides == 20)
                    {
                        Console.WriteLine(StringTable.NerdAlert);
                    }
                }
                //Press X to roll the dice
                Console.WriteLine(StringTable.PressKeyToStart);
                Console.ReadKey();
                //Clear any old values we might have had from previous loops, roll dice, and store results
                diceValues.Clear();
                while (diceValues.Count < NUMBER_OF_DICE)
                {
                    int tmp = RollDice(diceNumSides);
                    Thread.Sleep(50);
                    diceValues.Add(tmp);
                    diceTotal += tmp;
                }

                //Print the results of the dice roll
                DisplayDiceRollResults(diceValues);
               
                //Since we might end up with another set of dice results, let's clear the List of dice results
                diceValues.Clear();
                diceNumSides = 0;
                userResponse = "";
                while (userResponse != "y" && userResponse != "n")
                {
                    Console.Write(StringTable.RollAgainPrompt);
                    userResponse = (Console.ReadLine()).ToLower();
                }
                if (userResponse == "y") { continue; }
                else if (userResponse == "n") { break; }
            }
            //Thread.Sleep(5000);
        }

        public static int RollDice(int maximumSides)
        {
            Random r = new Random();
            int randomResult = r.Next(1, maximumSides + 1);
            return randomResult;
        }

        public static void DisplayDiceRollResults(List<int> diceValues)
        {
            //Print header, setting up headings for die numbers and results
            Console.WriteLine("\n");
            Console.WriteLine("Die".PadRight(10) + "Result".PadRight(10));
            Console.WriteLine("----".PadRight(10) + "--------".PadRight(10));

            //use this loop to print dice results
            for (int i = 0; i < NUMBER_OF_DICE; i++)
            {
                Console.WriteLine((i + 1).ToString().PadRight(10) + diceValues[i].ToString().PadRight(10));
            }
            #region Checking for special dice rolls
            if (diceValues.Count == 2)
            {
                if (diceValues[0] == 1 && diceValues[1] == 1)
                {
                    Console.WriteLine("SNAKE EYES!");
                }
                else if (diceValues[0] == 6 && diceValues[1] == 6)
                {
                    Console.WriteLine("BOXCARS!");
                }

            }
            #endregion
        }

        //public static void AnalyzeAndPrintDiceResults(List<int> diceValues)
        //{
            //Dictionary<int, int> valueFrequencies = new Dictionary<int, int>();
        //}


    }
}
