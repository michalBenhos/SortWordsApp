using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortWordsApp
{
    static class FileWriter
    {
        public static async Task WriteToFileAsync(string filePath, string content)
        {
            using (StreamWriter outputFile = new StreamWriter(filePath))
            {
                await outputFile.WriteAsync(content);
            }
        }

    }
}
