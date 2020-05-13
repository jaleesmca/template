using Overtime.Models;
using Overtime.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Overtime.Repository
{
    public class DocumentsRepository : IDocuments
    {
        private DBContext db;

        public DocumentsRepository(DBContext _db)
        {
            db = _db;
        }
        public IEnumerable<Documents> GetDocuments => db.Documents;

        public void Add(Documents documents)
        {
            db.Documents.Add(documents);
            db.SaveChanges();
        }

        public Documents GetDocument(int id)
        {
            Documents documents = db.Documents.Find(id);
            return documents;
        }

        public void Remove(int id)
        {
            Documents documents = db.Documents.Find(id);
            db.Documents.Remove(documents);
            db.SaveChanges();
        }

        public void Update(Documents documents)
        {
            db.Documents.Update(documents);
            db.SaveChanges();
        }
    }
}
