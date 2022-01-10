using System;
using System.Collections.Generic;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            Document obj1 = new Document(1, 5, DocumentType.text);
            Document obj2 = new Document(1, 5, DocumentType.text);
            Document obj3 = new Document(3, 7, DocumentType.raw);
            Document obj4 = new Document(3, 7, DocumentType.raw);
            Document obj5 = new Document(4, 9, DocumentType.table);
            Document obj6 = new Document(4, 9, DocumentType.table);
            List<Document> list1 = new List<Document>();
            List<Document> list2 = new List<Document>();
            list1.Add(obj1);
            list1.Add(obj3);
            list1.Add(obj5);
            list2.Add(obj2);
            list2.Add(obj4);
            list2.Add(obj6);
            if (Document.Compare(list1, list2))
            {
                Console.WriteLine("Elements equel");
            }
            else
            {
                Console.WriteLine("Elements aren`t equel");
            }

        }
    }
}
