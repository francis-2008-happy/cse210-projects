using System;

namespace ScriptureMemorizer
{
    class Word
    {
        private string _text;
        private bool _isHidden;

        // Constructor
        public Word(string text)
        {
            _text = text;
            _isHidden = false;
        }

        // Method to hide the word
        public void Hide()
        {
            _isHidden = true;
        }

        // Check if the word is hidden
        public bool IsHidden()
        {
            return _isHidden;
        }

        // Display the word (text or underscores)
        public string GetDisplayText()
        {
            if (_isHidden)
            {
                // Replace each letter with an underscore
                return new string('_', _text.Length);
            }
            else
            {
                return _text;
            }
        }
    }
}