using System;
using System.Collections.Generic;
using System.IO;

namespace ScriptureMemorizer
{
    class ScriptureLibrary
    {
        private List<ScriptureInfo> _scriptures;
        private Random _random;

        // Class to hold scripture information
        public class ScriptureInfo
        {
            public string Book { get; set; }
            public int Chapter { get; set; }
            public int StartVerse { get; set; }
            public int EndVerse { get; set; }
            public string Text { get; set; }
        }

        public ScriptureLibrary()
        {
            _scriptures = new List<ScriptureInfo>();
            _random = new Random();
            LoadScriptures();
        }

        // Load scriptures from a file
        private void LoadScriptures()
        {
            // Create default scriptures if the file doesn't exist
            if (!File.Exists("scriptures.txt"))
            {
                CreateDefaultScripturesFile();
            }

            try
            {
                string[] lines = File.ReadAllLines("scriptures.txt");
                foreach (string line in lines)
                {
                    string[] parts = line.Split('|');
                    if (parts.Length >= 2)
                    {
                        ScriptureInfo info = new ScriptureInfo();
                        string[] refParts = parts[0].Split(' ');
                        
                        // Parse book (may contain multiple words)
                        string book = "";
                        for (int i = 0; i < refParts.Length - 1; i++)
                        {
                            book += refParts[i] + " ";
                        }
                        info.Book = book.Trim();
                        
                        // Parse chapter and verse
                        string[] chapterVerse = refParts[refParts.Length - 1].Split(':');
                        info.Chapter = int.Parse(chapterVerse[0]);
                        
                        // Check if it's a verse range
                        if (chapterVerse[1].Contains("-"))
                        {
                            string[] verses = chapterVerse[1].Split('-');
                            info.StartVerse = int.Parse(verses[0]);
                            info.EndVerse = int.Parse(verses[1]);
                        }
                        else
                        {
                            info.StartVerse = int.Parse(chapterVerse[1]);
                            info.EndVerse = info.StartVerse;
                        }
                        
                        info.Text = parts[1];
                        _scriptures.Add(info);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading scriptures: {ex.Message}");
                CreateDefaultScripturesFile();
            }
        }

        // Create a default scriptures file
        private void CreateDefaultScripturesFile()
        {
            List<string> defaultScriptures = new List<string>
            {
                "John 3:16|For God so loved the world, that he gave his only begotten Son, that whosoever believeth in him should not perish, but have everlasting life.",
                "Proverbs 3:5-6|Trust in the Lord with all thine heart; and lean not unto thine own understanding. In all thy ways acknowledge him, and he shall direct thy paths.",
                "1 Nephi 3:7|And it came to pass that I, Nephi, said unto my father: I will go and do the things which the Lord hath commanded, for I know that the Lord giveth no commandments unto the children of men, save he shall prepare a way for them that they may accomplish the thing which he commandeth them."
            };

            File.WriteAllLines("scriptures.txt", defaultScriptures);
            
            // Now load the default scriptures
            LoadScriptures();
        }

        // Get a random scripture from the library
        public Scripture GetRandomScripture()
        {
            if (_scriptures.Count == 0)
            {
                // Fallback if no scriptures are loaded
                Reference reference = new Reference("John", 3, 16);
                return new Scripture(reference, "For God so loved the world, that he gave his only begotten Son, that whosoever believeth in him should not perish, but have everlasting life.");
            }

            ScriptureInfo info = _scriptures[_random.Next(_scriptures.Count)];
            Reference reference;
            
            if (info.StartVerse == info.EndVerse)
            {
                reference = new Reference(info.Book, info.Chapter, info.StartVerse);
            }
            else
            {
                reference = new Reference(info.Book, info.Chapter, info.StartVerse, info.EndVerse);
            }
            
            return new Scripture(reference, info.Text);
        }
    }
}