using Overtime.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Overtime.Services
{
    public interface IDocuments
    {
        IEnumerable<Documents> GetDocuments { get; }
        Documents GetDocument(int id);

        void Add(Documents documents);

        void Remove(int id);
        void Update(Documents documents);
        public IEnumerable<Documents> GetDocumentsList();
    }
}
