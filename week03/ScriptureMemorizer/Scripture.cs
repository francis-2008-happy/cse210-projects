using System;
using System.Collections.Generic;
using System.Linq;

namespace ScriptureMemorizer
{
    class Scripture
    {
        private Reference _reference;
        private List<Word> _words;

        // Constructor
        public Scripture(Reference reference, string text)
        {
            _reference = reference;
            _words = new List<Word>();

            // Split the text into words and create Word objects
            string[] wordArray = text.Split(' ');
            foreach (string wordText in wordArray)
            {
                _words.Add(new Word(wordText));
            }
        }

        // Display the complete scripture
        public string GetDisplayText()
        {
            string referenceText = _reference.GetDisplayText();
            
            // Join all the word display texts with spaces
            string scriptureText = string.Join(" ", _words.Select(w => w.GetDisplayText()));
            
            return $"{referenceText} - {scriptureText}";
        }

        // Hide a specified number of random words
        public void HideRandomWords(int numberToHide)
        {
            // Get a list of words that are not hidden yet
            var wordsNotHidden = _words.Where(w => !w.IsHidden()).ToList();
            
            // If all words are hidden, don't do anything
            if (wordsNotHidden.Count == 0)
            {
                return;
            }

            // Get a random number generator
            Random random = new Random();
            
            // Determine how many words to hide (don't exceed available words)
            int wordsToHide = Math.Min(numberToHide, wordsNotHidden.Count);
            
            // Hide the specified number of random words
            for (int i = 0; i < wordsToHide; i++)
            {
                int randomIndex = random.Next(wordsNotHidden.Count);
                wordsNotHidden[randomIndex].Hide();
                wordsNotHidden.RemoveAt(randomIndex);
            }
        }

        // Check if all words are hidden
        public bool IsCompletelyHidden()
        {
            return _words.All(w => w.IsHidden());
        }
        
        // Get the percentage of words that are hidden
        public int GetPercentHidden()
        {
            if (_words.Count == 0)
            {
                return 0;
            }
            
            int hiddenCount = _words.Count(w => w.IsHidden());
            return (int)Math.Round((double)hiddenCount / _words.Count * 100);
        }
    }
}