using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using DevBuild.Utilities;

namespace DevBuild.NSidedDice
{
    class Program
    {
        private const int NUMBER_OF_DICE = 2;

        static void Main(string[] args)
        {
            while (true)
            {
                var diceNumSides = 0;
                var diceValues = new List<int>();

                //Greet the user
                Console.WriteLine(StringTable.HelloString);
                //Ask the user to enter the number of sides for a pair of dice between 2 (coin) and 120 (disdyakis triacontahedron)
                while (diceNumSides < 2 || diceNumSides > 120)
                {
                    Console.Write(StringTable.DiceSidesPrompt + " ");
                    int.TryParse(Console.ReadLine(), out diceNumSides);
                    if (diceNumSides == 20)
                    {
                        Console.WriteLine(StringTable.NerdAlert);
                    }
                }
                //Prompt user, 'Press any key to roll the dice'
                Console.WriteLine(StringTable.PressKeyToStart);
                Console.ReadKey();
                while (diceValues.Count < NUMBER_OF_DICE)
                {
                    var tmp = RollDice(diceNumSides);
                    //allow time for the generation of a new seed, so that generated number can be a bit more random
                    Thread.Sleep(50);
                    diceValues.Add(tmp);
                }

                //Print the results of the dice roll
                DisplayDiceRollResults(diceValues);
               
                //Ask user if they'd like to roll dice again -- jump to top of the main loop if so, exit the main method if not
                var response = UserInput.GetYesOrNoAnswer(StringTable.RollAgainPrompt);

                switch (response)
                {
                    case YesNoAnswer.Yes: continue;
                    case YesNoAnswer.No: return;
                    default: continue;
                }
            }
        }

        public static int RollDice(int maximumSides) => new Random().Next(1, maximumSides + 1);

        public static void DisplayDiceRollResults(List<int> values)
        {
            //Print header, setting up headings for die numbers and results
            Console.WriteLine("\nDie".PadRight(10) + "Result".PadRight(10) + "\n----".PadRight(10) + "--------".PadRight(10));

            //use this loop to print dice results
            for (int i = 0; i < NUMBER_OF_DICE; i++)
            {
                Console.WriteLine((i + 1).ToString().PadRight(10) + values[i].ToString().PadRight(10));
            }
            #region Checking for special dice rolls
            if (values.Count == 2)
            {
                if (values[0] == 1 && values[1] == 1)
                {
                    Console.WriteLine("SNAKE EYES!");
                }
                else if (values[0] == 6 && values[1] == 6)
                {
                    Console.WriteLine("BOXCARS!");
                }
            }
            #endregion
        }
    }
}