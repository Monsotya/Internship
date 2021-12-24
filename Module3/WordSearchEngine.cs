using System;
using System.IO;
using System.Linq;


namespace Module3
{
    class WordSearchEngine
    {
        private String[] words;
        private String[] pathes;
        private int[] countWords;
        public delegate void FoundWord(String file, String word, int line);
        public event FoundWord Notify;
        public String[] Words { get => words;
            set
            {
                for (int i = 0; i < value.Length; i++)
                {
                    value[i] = value[i].Replace(" ", string.Empty);
                    if (value[i] == string.Empty)
                    {
                        throw new Exception();
                    }
                }
                words = value;
            }
        }

        public String[] Pathes { get => pathes;
            set
            {
                for (int i = 0; i <  value.Length; i++)
                {
                    value[i] = value[i].Replace(" ", string.Empty);
                    if (!File.Exists(value[i]))
                    {
                        throw new Exception();
                    }
                    String fileType = Path.GetExtension(value[i]);
                    if (!(fileType == ".txt" || fileType == ".xml" || fileType == ".html"))
                    {
                        throw new Exception();
                    }
                }
                pathes = value;
            }        
        }
        public WordSearchEngine(String[] words, String[] pathes)
        {
            this.Words = words;
            this.Pathes = pathes;
            if (words.Length != pathes.Length)
            {
                throw new Exception();
            }
            this.countWords = new int[words.Length];
            this.Notify = new FoundWord(DisplayMessage);
        }

        public void DisplayMessage(String file, String word, int line)
        {
            Console.WriteLine($"Scanning {file}, found {word} in the {line} line");
        }

        public void FindAllMatches()
        {
            for(int i = 0; i < pathes.Length; i++)
            {
                FindWord(i);
            }
            Console.WriteLine("\n");
            for (int i = 0; i < pathes.Length; i++)
            {
                Console.WriteLine($"File name: {Path.GetFileName(pathes[i])}, word: {words[i]}, count: {countWords[i]}");
            }

            Console.WriteLine($"\nTotal finds: {CountAllWords()}");
        }

        private void FindWord(int index)
        {
            String[] text = File.ReadAllLines(pathes[index]);
            int counter = 0;
            countWords[index] = 0;
            foreach(String line in text)
            {
                if (line.Contains(words[index]))
                {
                    Notify.Invoke(Path.GetFileName(pathes[index]), words[index], counter);
                    countWords[index]++;
                    counter--;
                }
                counter++;
            }
        }

        private int CountAllWords()
        {
            int result = 0;
            foreach(int i in countWords)
            {
                result += i;
            }
            return result;
        }
    }
}
