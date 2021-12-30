using System;
using System.IO;
using System.Linq;


namespace Module3
{
    static class WordSearchEngine
    {
        public delegate void FoundWord(String file, String word, int line);
        public static event FoundWord Notify;

        public static void DisplayMessage(String file, String word, int line)
        {
            Console.WriteLine($"Scanning {file}, found {word} in the {line} line");
        }

        public static void FindAllMatchesOneWord(String[] pathes, String word)
        {
            WordSearchEngine.Notify = new FoundWord(DisplayMessage);
            CheckPathes(pathes);
            int[] countWords = new int[pathes.Length];
            for (int i = 0; i < pathes.Length; i++)
            {
                countWords[i] = FindWord(pathes[i], word);
                Console.WriteLine($"\nFile name: {Path.GetFileName(pathes[i])}, word: {word}, count: {countWords[i]}\n");
            }
            Console.WriteLine($"\nTotal finds: {countWords.Sum()}\nTotal files scanned: {pathes.Length}");
        }

        public static void FindAllMatches(String[] pathes, String[] words)
        {
            WordSearchEngine.Notify = new FoundWord(DisplayMessage);
            CheckPathes(pathes);
            CheckWords(words);

            if (words.Length != pathes.Length)
            {
                throw new Exception("Quantity of words and pathes should be equal!");
            }

            int[] countWords = new int[pathes.Length];
            for (int i = 0; i < pathes.Length; i++)
            {
                countWords[i] = FindWord(pathes[i], words[i]);

            }
            Console.WriteLine("\n");
            for (int i = 0; i < pathes.Length; i++)
            {
                Console.WriteLine($"File name: {Path.GetFileName(pathes[i])}, word: {words[i]}, count: {countWords[i]}");
            }

            Console.WriteLine($"\nTotal finds: {countWords.Sum()}\nTotal files scanned: {pathes.Length}\n");
        }

        private static int FindWord(String path, String word)
        {
            String[] text;
            try
            {
                text = File.ReadAllLines(path);
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                return 0;
            }
            int counter = 0;
            int countWords = 0;
            foreach (String line in text)
            {
                if (line.Contains(word))
                {
                    Notify.Invoke(Path.GetFileName(path), word, counter);
                    countWords++;
                    counter--;
                }
                counter++;
            }
            return counter;
        }
        public static void CheckWords(String[] words)
        {
            for (int i = 0; i < words.Length; i++)
            {
                words[i] = words[i].Replace(" ", string.Empty);
                if (words[i] == string.Empty)
                {
                    throw new Exception("Word must not be empty!");
                }
            }
        }
        public static void CheckPathes(String[] pathes)
        {
            for (int i = 0; i < pathes.Length; i++)
            {
                pathes[i] = pathes[i].Replace(" ", string.Empty);
                try
                {
                    if (!File.Exists(pathes[i]))
                    {
                        throw new Exception("File path does not exist!");
                    }
                    String fileType = Path.GetExtension(pathes[i]);
                    if (!(fileType == ".txt" || fileType == ".xml" || fileType == ".html"))
                    {
                        throw new Exception("Wrong type of file!");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("{0} Exception caught.", e);
                    pathes[i] = "";
                }

            }
        }
    }
}
