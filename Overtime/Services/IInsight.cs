using Overtime.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Overtime.Services
{
    public interface IInsight
    {
        Insight GetInsight(int id);

        IEnumerable<Insight> GetInsights { get; }

        void Add(Insight insight);

        void Remove(int id);

        void Update(Insight insight);

        IEnumerable<Insight> GetInsightsByDocument(int rowid, int doc_id);
    }
}
