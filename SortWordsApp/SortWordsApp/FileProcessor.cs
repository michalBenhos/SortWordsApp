using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortWordsApp
{
    /// <summary>
    /// Processes files by reading, combining, sorting, and writing their contents based on user-specified options.
    /// </summary>
    class FileProcessor
    {
        /// <summary>
        /// Manages the file reading, content processing, and file writing tasks.
        /// </summary>
        /// <param name="filePaths">Array of file paths to be processed.</param>
        /// <param name="sortOption">User specified sorting option.</param>
        /// <param name="splitOption">User specified splitting character.</param>
        public static async Task ProcessFilesAsync(string[] filePaths, string sortOption, string splitOption)
        {
            try
            {
                var fileContents = await FileReader.ReadAllFilesAsync(filePaths);
                var contentProcessor = new ContentProcessor(fileContents);
                var formatedContent = contentProcessor.GetFormattedContents(sortOption, splitOption);
                await FileWriter.WriteToFileAsync(@"C:\Users\micha\source\repos\SortWordsApp\F4.txt", formatedContent);
                Console.WriteLine("The file F4 has been created.");

                var (mostFrequentWords, maxCount) = contentProcessor.GetMostFrequentWords();
                Console.WriteLine($"The most frequent word occur {maxCount} times:");
                foreach (var word in mostFrequentWords)
                {
                    Console.WriteLine(word);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
}
