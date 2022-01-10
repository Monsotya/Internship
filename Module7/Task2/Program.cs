using System;
using System.Collections.Generic;

namespace Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            Document obj1 = new Document(1, 5, DocumentType.text, "File1");
            Document obj2 = new Document(1, 5, DocumentType.table, "File2");
            Document obj3 = new Document(3, 7, DocumentType.raw, "File3");
            Document obj4 = new Document(3, 7, DocumentType.text, "File4");
            Document obj5 = new Document(4, 9, DocumentType.text, "File5");
            Document obj6 = new Document(4, 9, DocumentType.table, "File6");

            List<Document> list1 = new List<Document>();
            List<Document> list2 = new List<Document>();

            list1.Add(obj1);
            list1.Add(obj3);
            list1.Add(obj5);
            list2.Add(obj2);
            list2.Add(obj4);
            list2.Add(obj6);

            Console.WriteLine("First report:\n");
            Document.FormReport(list1);
            Console.WriteLine("\nSecond report:\n");
            Document.FormReport(list2);
        }
    }
}
