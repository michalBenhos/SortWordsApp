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
    public class ContentProcessor
    {
        private Dictionary<string, int> _wordCount = new();

        public ContentProcessor(string[] contents) 
        {
            ProcessContents(contents);
        }

        public string GetFormattedContents(string sortOption, string splitOption)
        {
            var sortedWords = SortWords(sortOption);
            string delimiter = GetDelimiter(splitOption);

            return string.Join(delimiter, sortedWords);
        }

        public (List<string>, int) GetMostFrequentWords()
        {
            int maxCount = _wordCount.Values.Max();
            List<string> mostFrequentWords = _wordCount.Where(pair => pair.Value == maxCount).
                                                        Select(pair => pair.Key).ToList();
            return (mostFrequentWords, maxCount);
        }

        private void ProcessContents(string[] contents)
        {
            var combinedContent = string.Join(" ", contents).ToLower();
            var normalized = Regex.Replace(combinedContent, @"[\p{P}]", "");
            var words = normalized.Split(new[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            CountWords(words);

        }

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

        private IEnumerable<string> SortWords(string sortOption)
        {
            return sortOption == "-d" ? _wordCount.Keys.OrderByDescending(word => word) :
                                                    _wordCount.Keys.OrderBy(word => word);
        }

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
