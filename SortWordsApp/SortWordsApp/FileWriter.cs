using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortWordsApp
{
    /// <summary>
    /// Provides functionality to asynchronously write content to files.
    /// </summary>
    static class FileWriter
    {
        /// <summary>
        /// Writes content to a file asynchronously.
        /// </summary>
        /// <param name="filePath">The path to the file where the content will be written.</param>
        /// <param name="content">The content to write to the file.</param>
        /// <returns>A Task that represents the asynchronous write operation.</returns>
        public static async Task WriteToFileAsync(string filePath, string content)
        {
            using (StreamWriter outputFile = new StreamWriter(filePath))
            {
                await outputFile.WriteAsync(content);
            }
        }
    }
}
