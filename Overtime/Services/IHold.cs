using Overtime.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Overtime.Services
{
    public interface IHold
    {
        IEnumerable<Hold> GetHolds { get; }
        public Hold GetHold(int id);

        void Add(Hold role);

        void Remove(int id);
        void Update(Hold hold);

        IEnumerable<Hold> GetHoldsbyDocument(int rowid, int doc_id);
    }
}
