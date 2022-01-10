using System;
using System.Collections.Generic;
using System.Linq;

namespace Task2
{
    public enum DocumentType { text, table, raw }
    public class Document
    {
        private int id;
        private DocumentType typeDocument;
        private int contentLength;
        private string documentName;

        public Document()
        {

        }

        public Document(int i, int length, DocumentType docType, string name)
        {
            id = i;
            contentLength = length;
            typeDocument = docType;
            documentName = name;
        }

        public static void FormReport(List<Document> doclist)
        {
            var GroupByType = doclist.GroupBy(s => s.TypeDocument).OrderByDescending(c => c.Key).Select(std => new
            {
                Key = std.Key,
                Documents = std.OrderBy(x => x.DocumentName),
                TotalNumber = std.Count(),
                TotalSize = std.Sum(y => y.ContentLength)
            });


            foreach (var group in GroupByType)
            {
                Console.WriteLine("Document type: " + group.Key + "; total number: " + group.TotalNumber + "; overal size: " + group.TotalSize + " Mb");
                Console.WriteLine("-----------------------------------------------------------");
                foreach (var doc in group.Documents)
                {
                    Console.WriteLine(doc.DocumentName + ", size: " + doc.ContentLength + ";");
                }
                Console.WriteLine();
            }
        }

        public int Id { 
            get => id; 
            set {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Id must be a positive number!");
                }

                id = value;
            }
        }

        public string DocumentName

        {
            get => documentName;
            set
            {
                if (value == "")
                {
                    throw new Exception("Name must not be empty!");
                }

                documentName = value;
            }
        }

        public int ContentLength
        {
            get => contentLength;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Length must be a positive number!");
                }

                id = value;
            }
        }

        public DocumentType TypeDocument
        {
            get => typeDocument;
            set
            {
                typeDocument = value;
            }
        }
    }
}
