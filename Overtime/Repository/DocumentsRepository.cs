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

        public IEnumerable<Documents > GetDocumentsList()
        {
            var query = from u in db.Documents
                        join d in db.Workflows
                          on u.dc_workflow_id equals d.w_id
                        select new Documents
                        {
                            dc_id = u.dc_id,
                            dc_workflow_id = u.dc_workflow_id,
                            dc_active_yn=u.dc_active_yn,
                            dc_cre_date=u.dc_cre_date,
                            dc_cre_by=u.dc_cre_by,
                            dc_description=u.dc_description,
                            doc_workflow_name=d.w_description
                        };

            return query;
        }

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
