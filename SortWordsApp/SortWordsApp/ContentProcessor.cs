using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SortWordsApp
{
    /// <summary>
    /// Processes textual content for sorting and deduplication based on specified criteria.
    /// </summary>
    public class ContentProcessor
    {
        private Dictionary<string, int> _wordCount = new();

        /// <summary>
        /// Initializes the processor and processes the content of the provided files.
        /// </summary>
        /// <param name="contents">Array of file contents to process.</param>
        public ContentProcessor(string[] contents) 
        {
            ProcessContents(contents);
        }


        /// <summary>
        /// Returns formatted content as a single string based on the sorting and splitting options.
        /// </summary>
        /// <param name="sortOption">Sorting option (-a or -d).</param>
        /// <param name="splitOption">Splitting character option (-s, -c, or -n).</param>
        /// <returns>Formatted string of processed content.</returns>
        public string GetFormattedContents(string sortOption, string splitOption)
        {
            var sortedWords = SortWords(sortOption);
            string delimiter = GetDelimiter(splitOption);

            return string.Join(delimiter, sortedWords);
        }

        /// <summary>
        /// Calculates and returns the most frequently occurring words and their count.
        /// </summary>
        /// <returns>A tuple containing a list of the most frequent words and their count.</returns>
        public (List<string>, int) GetMostFrequentWords()
        {
            int maxCount = _wordCount.Values.Max();
            List<string> mostFrequentWords = _wordCount.Where(pair => pair.Value == maxCount).
                                                        Select(pair => pair.Key).ToList();
            return (mostFrequentWords, maxCount);
        }

        /// <summary>
        /// Combines, normalizes, and counts words from multiple file contents.
        /// </summary>
        /// <param name="contents">Array of strings containing the file contents.</param>
        private void ProcessContents(string[] contents)
        {
            var combinedContent = string.Join(" ", contents).ToLower();
            var normalized = Regex.Replace(combinedContent, @"[\p{P}]", "");
            var words = normalized.Split(new[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            CountWords(words);

        }

        /// <summary>
        /// Counts occurrences of each word in the provided array.
        /// </summary>
        /// <param name="words">Array of words to count.</param>
        private void CountWords(string[] words)
        {
            foreach (var word in words)
            {
                if (_wordCount.ContainsKey(word))
                {
                    _wordCount[word]++;
                }
                else
                {
                    _wordCount[word] = 1;
                }
            }
        }

        /// <summary>
        /// Sorts words in either ascending or descending order based on the specified sort option.
        /// </summary>
        /// <param name="sortOption">The sort option indicating the desired order of the results:
        /// "-a" for ascending order, "-d" for descending order.</param>
        /// <returns>An enumerable collection of strings, sorted according to the specified order.</returns>
        private IEnumerable<string> SortWords(string sortOption)
        {
            return sortOption == "-d" ? _wordCount.Keys.OrderByDescending(word => word) :
                                                    _wordCount.Keys.OrderBy(word => word);
        }

        /// <summary>
        /// Determines the delimiter based on user selection for formatting the output.
        /// </summary>
        /// <param name="splitOption">The user-selected option for splitting.</param>
        /// <returns>The delimiter as a string.</returns>
        private string GetDelimiter(string splitOption)
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
    }
}
