using ExpertQuestionnaire.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertQuestionnaire.Context
{
    public interface IContext
    {
        IQueryable<TPOCO> AsQueryable<TPOCO>() where TPOCO : POCO.BasePOCO;

        void Insert<TPOCO>(TPOCO entity) where TPOCO : POCO.BasePOCO;
        void Insert<TPOCO>(IEnumerable<TPOCO> entities) where TPOCO : POCO.BasePOCO;

        void Delete<TPOCO>(TPOCO entity) where TPOCO : POCO.BasePOCO;
        void Delete<TPOCO>(IEnumerable<TPOCO> entities) where TPOCO : POCO.BasePOCO;

        TPOCO Attach<TPOCO>(TPOCO entity) where TPOCO : POCO.BasePOCO;

        void Save();
    }
}
