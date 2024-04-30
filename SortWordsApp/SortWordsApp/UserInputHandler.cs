using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortWordsApp
{
    /// <summary>
    /// Handles user inputs, ensuring correct arguments.
    /// </summary>
    static class UserInputHandler
    {
        /// <summary>
        /// Retrieves and validates user inputs from the command line arguments and interactive inputs.
        /// </summary>
        /// <param name="args">Command line arguments provided to the program.</param>
        /// <returns>A tuple containing file paths and user specified options for sorting and splitting.</returns>
        /// <exception cref="ArgumentException">Thrown when the number of arguments is less than three.</exception>
        public static (string[], string, string) GetInputs(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("Please provide at least three file paths as arguments.");
                throw new ArgumentException("Incomplete arguments prvided.");
            }

            Console.WriteLine("Enter your sorting option (-a for asceding, -d for desceding):");
            string sortOption = Console.ReadLine();

            Console.WriteLine("Enter your spliting character (-s for space, -c for comma, -n for new line:");
            string splitOption = Console.ReadLine();

            return (args, sortOption, splitOption);
        }
    }
}
