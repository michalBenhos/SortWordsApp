using System.Text.RegularExpressions;

namespace SortWordsApp
{
    /// <summary>
    /// Main entry point for the SortWordsApp.
    /// Processes multiple text files to sort and deduplicate the words, and outputs the results into a new file.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Asynchronously executes the main logic of the application, handling command line inputs
        /// and orchestrating the reading, processing, and writing of files.
        /// </summary>
        /// <param name="args">Command line arguments specifying the paths to the input files.</param>
        static async Task Main(string[] args)
        {
            try
            {
                var (filePaths, sortOption, splitOption) = UserInputHandler.GetInputs(args);
                await FileProcessor.ProcessFilesAsync(filePaths, sortOption, splitOption);
            }

            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}