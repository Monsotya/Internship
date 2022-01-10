using System;
using System.Collections.Generic;
using System.Linq;

namespace Task1
{
    public enum DocumentType { text, table, raw }
    public class Document
    {
        private int id;
        private DocumentType typeDocument;
        private int contentLength;

        public Document()
        {

        }

        public Document(int i, int length, DocumentType docType)
        {
            id = i;
            contentLength = length;
            typeDocument = docType;
        }

        public static bool Compare(List<Document> doc1, List<Document> doc2)
        {
            DocumentComparer documentComparer = new DocumentComparer();
            return doc1.SequenceEqual(doc2, documentComparer);
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
