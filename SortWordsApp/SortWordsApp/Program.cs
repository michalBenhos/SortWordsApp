namespace SortWordsApp
{
    class Pogram
    {
        static async Task Main()
        {
            string[] filePaths = {
                @"C:\Users\micha\source\repos\SortWordsApp\file1.txt",
                @"C:\Users\micha\source\repos\SortWordsApp\file2.txt",
                @"C:\Users\micha\source\repos\SortWordsApp\file3.txt"
            };

            try
            {
                var fileContents = await ReadAllFilesAsync(filePaths);
                DisplayContents(fileContents);
               
            }
            catch (Exception ex) 
            {
                Console.WriteLine("An error occurred: " + ex.Message);
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

        static void DisplayContents(string[] contents) 
        {
            foreach (var item in contents) 
            {
                Console.WriteLine("File content:");
                Console.WriteLine(item);
                Console.WriteLine("----------------------------------------------------");
            }
        }
    }

}