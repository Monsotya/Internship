using System;

namespace Module3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter file pathes");
            String[] pathes = Console.ReadLine().Split(",");
            Console.WriteLine("Enter words you want to find");
            String[] words = Console.ReadLine().Split(',');
            WordSearchEngine.FindAllMatches(pathes, words);
        }
    }
}
