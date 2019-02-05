using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevBuild.Utilities {
    class MenuHandling {

        /// <summary>
        /// Displays a list of items to console, optionally numbers the list if optional menuMode parameter is set to true
        /// </summary>
        /// <param name="menuOptions">List containing items to be displayed to console output</param>
        /// <param name="menuMode">If true, printed list will be numbered sequentially. If false, printed list will be unordered.</param>
        /// <param name="messagePrompt">Use this string to print a heading at the top of the list (ex. "Please select one of the following options: "</param>
        public static void PrintMenuOptions(IList<object> menuOptions, [Optional]bool menuMode, [Optional]string messagePrompt) {
            Console.WriteLine();
            Console.Write(String.IsNullOrEmpty(messagePrompt) ? "" : (messagePrompt + "\n"));
            for (int i = 0; i < menuOptions.Count; i++) {
                Console.WriteLine((menuMode ? $"{i + 1}.) " : "") + menuOptions[i].ToString());
            }
        }
    }
}