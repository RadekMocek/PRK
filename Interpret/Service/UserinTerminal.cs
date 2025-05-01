using static System.Console;

namespace Interpret.Service
{
    internal static class UserinTerminal
    {
        /// <summary>
        /// Prompts the user to input a number to stdin. Repeats until given number is in correct format.
        /// </summary>
        /// <returns>Number from stdin in a long format</returns>
        public static long GetUserInput()
        {
            string userin;
            while (true) {
                userin = ReadLine();
                if (long.TryParse(userin, out long result)) {
                    return result;
                }
                else {
                    WriteLine("You must enter a whole number. Please try again.");
                }
            }
        }
    }
}
