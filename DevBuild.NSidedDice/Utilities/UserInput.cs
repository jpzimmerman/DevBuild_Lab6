using System;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevBuild.Utilities
{
    public enum YesNoAnswer { No = 0, Yes = 1, AnswerNotGiven = 2, } //for convention's sake, let's make sure Yes = 1 and No = 0;
    
    class UserInput
    {
        /// <summary>
        /// This function prompts the user to type "y", "yes", "n", or "no" to provide a yes-or-no answer. Function stays in a loop until 
        /// user enters something we recognize as a yes or no answer.
        /// </summary>
        /// <returns>A yes-or-no answer enum set to Yes, No, or Answer Not Given. </returns>
        public static YesNoAnswer GetYesOrNoAnswer(string questionPrompt)
        {
            YesNoAnswer tmp = YesNoAnswer.AnswerNotGiven;
            string userResponse = "";

            while ( userResponse != "y" && userResponse != "yes" && 
                    userResponse != "n" && userResponse != "no")
            {
                Console.Write(questionPrompt);
                userResponse = Console.ReadLine().Trim().ToLower();
            }
            switch (userResponse)
            {
                case "y":
                case "yes": tmp = YesNoAnswer.Yes; break;
                case "n":
                case "no":  tmp = YesNoAnswer.No; break;
                default: tmp = YesNoAnswer.AnswerNotGiven; break;
            }
            return tmp;
        }

        /// <summary>
        /// (Deprecated) Keeps user in a loop at the input until user enters a string that passes the provided validation filters (information type for name, email address, phone number, etc.)
        /// </summary>
        /// <param name="promptQuestion">String containing a prompt question for the user's entry</param>
        /// <param name="inputValidationFilters">List of InformationType enumerated types representing filters to check the user's entry against</param>
        /// <returns></returns>
        [Obsolete("This method has been deprecated. Please use new method PromptUntilValidEntry(string questionPrompt, out string response, params InformationType[] inputValidationFilters")]
        public static string PromptUntilValidEntry(string promptQuestion, params InformationType[] inputValidationFilters) {
            string response = "";
            Console.Write(promptQuestion);
            while (String.IsNullOrEmpty(response)) {
                response = Console.ReadLine().Trim();
                if (String.IsNullOrEmpty(response)) {
                    Console.Write(promptQuestion);
                    continue;
                }
                foreach (InformationType infoType in inputValidationFilters) {
                    if (!Validation.ValidateInfo(infoType, response)) {
                        response = "";
                    }
                }
            }
            return response;
        }

        /// <summary>
        /// Keeps user in a loop at the input until user enters a string that passes the provided validation filters (information type for name, email address, phone number, etc.)
        /// </summary>
        /// <param name="promptQuestion">String containing a prompt question for the user's entry</param>
        /// <param name="inputValidationFilters">List of InformationType enumerated types representing filters to check the user's entry against</param>
        /// <returns></returns>
        public static void PromptUntilValidEntry(string questionPrompt, out string response, params InformationType[] inputValidationFilters)
        {
            response = "";
            Console.Write(questionPrompt);
            while (String.IsNullOrEmpty(response))
            {
                response = Console.ReadLine().Trim();
                if (String.IsNullOrEmpty(response))
                {
                    Console.Write(questionPrompt);
                    continue;
                }
                foreach (InformationType infoType in inputValidationFilters)
                {
                    if (!Validation.ValidateInfo(infoType, response))
                    {
                        response = "";
                    }
                }
            }
            return;
        }

        public static uint SelectMenuOption(int numberOfOptions) {
            uint userSelection = 0;
            string userEntry = "";

            while (!uint.TryParse(userEntry, out userSelection) ||
                    userSelection < 0 ||
                    userSelection > numberOfOptions) {
                PromptUntilValidEntry($"Please enter a selection, 1-{numberOfOptions}: ", out userEntry, InformationType.Numeric);
            }
            return userSelection;
        }
    }
}
