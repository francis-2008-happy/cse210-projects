using System;

namespace ScriptureMemorizer
{
    class Reference
    {
        private string _book;
        private int _chapter;
        private int _startVerse;
        private int _endVerse;

        // Constructor for a single verse reference (e.g., "John 3:16")
        public Reference(string book, int chapter, int verse)
        {
            _book = book;
            _chapter = chapter;
            _startVerse = verse;
            _endVerse = verse; // Same as start verse for a single verse
        }

        // Constructor for a verse range reference (e.g., "Proverbs 3:5-6")
        public Reference(string book, int chapter, int startVerse, int endVerse)
        {
            _book = book;
            _chapter = chapter;
            _startVerse = startVerse;
            _endVerse = endVerse;
        }

        // Get the formatted reference string
        public string GetDisplayText()
        {
            if (_startVerse == _endVerse)
            {
                // Single verse
                return $"{_book} {_chapter}:{_startVerse}";
            }
            else
            {
                // Verse range
                return $"{_book} {_chapter}:{_startVerse}-{_endVerse}";
            }
        }
    }
}