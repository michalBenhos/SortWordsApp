using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortWordsApp
{
    static class FileReader
    {
        public static async Task<string[]> ReadAllFilesAsync(string[] filePaths)
        {
            Task<string>[] readTasks = new Task<string>[filePaths.Length];

            for (int i = 0; i < filePaths.Length; i++)
            {
                readTasks[i] = ReadFileAsync(filePaths[i]);
            }

            return await Task.WhenAll(readTasks);
        }

        private static async Task<string> ReadFileAsync(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("File not found at " + path);
            }

            return await File.ReadAllTextAsync(path);
        }
    }
}

