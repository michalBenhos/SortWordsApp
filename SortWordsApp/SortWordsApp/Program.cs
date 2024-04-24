using System.Text.RegularExpressions;

namespace SortWordsApp
{
    class Pogram
    {
        static async Task Main(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("Please provide at least three file paths as arguments.");
                return;
            }
            /*string[] filePaths = {
                @"C:\Users\micha\source\repos\SortWordsApp\file1.txt",
                @"C:\Users\micha\source\repos\SortWordsApp\file2.txt",
                @"C:\Users\micha\source\repos\SortWordsApp\file3.txt"
            };*/

            string[] filePaths = args;
            Console.WriteLine("Enter your sorting option (-a for asceding, -d for desceding):");
            string sortOption = Console.ReadLine();

            Console.WriteLine("Enter your spliting character (-s for space, -c for comma, -n for new line:");
            string splitOption = Console.ReadLine();

            try
            {
                var fileContents = await FileReader.ReadAllFilesAsync(filePaths);
                var contentProcessor = new ContentProcessor(fileContents);
                var formatedContent = contentProcessor.GetFormattedContents(sortOption, splitOption);
                await FileWriter.WriteToFileAsync(@"C:\Users\micha\source\repos\SortWordsApp\file4.txt", formatedContent);
                Console.WriteLine("Processed content written to file4.txt");
                var (mostFrequentWords, maxCount) = contentProcessor.GetMostFrequentWords();
                Console.WriteLine($"The most frequent word occur {maxCount} times:");
                foreach ( var word in mostFrequentWords )
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