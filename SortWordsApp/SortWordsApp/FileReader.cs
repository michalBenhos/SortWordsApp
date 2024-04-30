using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortWordsApp
{
    /// <summary>
    /// Provides functionality to asynchronously read files.
    /// </summary>
    static class FileReader
    {
        /// <summary>
        /// Reads all files specified in the filePaths array asynchronously and returns their contents.
        /// </summary>
        /// <param name="filePaths">An array of file paths to read from.</param>
        /// <returns>An array of strings where each string is the content of one file.</returns>
        public static async Task<string[]> ReadAllFilesAsync(string[] filePaths)
        {
            Task<string>[] readTasks = new Task<string>[filePaths.Length];

            for (int i = 0; i < filePaths.Length; i++)
            {
                string path = filePaths[i];
                readTasks[i] = Task.Run(() => ReadFile(path));
            }

            return await Task.WhenAll(readTasks);
        }

        /// <summary>
        /// Reads the content of a single file.
        /// </summary>
        /// <param name="path">The path to the file to read.</param>
        /// <returns>The content of the file as a string.</returns>
        /// <exception cref="FileNotFoundException">Thrown when the file at the specified path does not exist.</exception>
        private static string ReadFile(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("File not found at " + path);
            }

            return  File.ReadAllText(path);
        }
    }
}

