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
                var fileContents = await ReadAllFilesAsync(filePaths);
                var (processedContent, mostFrequentWord, maxCount) = ProcessContents(fileContents, sortOption, splitOption);
                await WriteToFileAsync(@"C:\Users\micha\source\repos\SortWordsApp\file4.txt", processedContent);
                Console.WriteLine("Processed content written to file4.txt");
                Console.WriteLine($"The most frequent word is '{mostFrequentWord}' with a count of {maxCount}.");
               
            }
            catch (Exception ex) 
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }

        }

        static (string, string, int) ProcessContents(string[] contents, string sortOption, string splitOption)
        {
            var combinedContent = string.Join(" ", contents).ToLower();
            var normalized = Regex.Replace(combinedContent, @"[\p{P}]", "");
            var words = normalized.Split(new[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            var wordCount = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (wordCount.ContainsKey(word))
                {
                    wordCount[word]++;
                }
                else 
                {
                    wordCount[word] = 1;
                }
            }

            var sortedWords = (sortOption == "-d" ? wordCount.Keys.OrderByDescending(word => word) : 
                                                    words.Distinct().OrderBy(word => word)).ToList();

            int maxCount = wordCount.Values.Max();
            var mostFrequentWord = wordCount.FirstOrDefault(x => x.Value == maxCount).Key;
            string delimiter = GetDelimiter(splitOption);

            return (string.Join(delimiter, sortedWords), mostFrequentWord, maxCount);
        }

        static string GetDelimiter(string splitOption) 
        {
            switch (splitOption) 
            {
                case "-s":
                    return " ";
                case "-c":
                    return ", ";
                case "-n":
                    return "\n";
                default:
                    return ", ";
            }
        }

        static async Task WriteToFileAsync(string filePath, string content)
        {
            using (StreamWriter outputFile = new StreamWriter(filePath)) 
            {
                await outputFile.WriteAsync(content);
            }
        }

        static async Task<string[]> ReadAllFilesAsync(string[] filePaths)
        {
            Task<string>[] readTasks = new Task<string>[filePaths.Length];
            
            for (int i = 0; i < filePaths.Length; i++) 
            {
                readTasks[i] = ReadFileAsync(filePaths[i]);
            }

            return await Task.WhenAll(readTasks);
        }

        static async Task<string> ReadFileAsync(string path)
        {
            if (!File.Exists(path)) 
            {
                throw new FileNotFoundException("File not found at " + path);
            }

            return await File.ReadAllTextAsync(path);
        }
    }

}