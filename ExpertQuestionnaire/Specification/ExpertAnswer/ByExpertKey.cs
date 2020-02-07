using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertQuestionnaire.Specification.ExpertAnswer
{
    public class ByExpertKey : BaseSpecification<POCO.ExpertAnswer>
    {
        public ByExpertKey(int expertKey) : base(x => x.ExpertKey == expertKey)
        { }
    }
}