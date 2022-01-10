using System;
using System.Collections.Generic;
using System.Text;

namespace Task1
{
    public class DocumentComparer : IEqualityComparer<Document>
    {
        public bool Equals(Document x, Document y)
        {
            return x.TypeDocument == y.TypeDocument && x.ContentLength == y.ContentLength;
        }

        public int GetHashCode(Document obj)
        {
            return obj.TypeDocument.GetHashCode() ^ obj.ContentLength.GetHashCode() ^ obj.Id.GetHashCode();
        }
    }
}
