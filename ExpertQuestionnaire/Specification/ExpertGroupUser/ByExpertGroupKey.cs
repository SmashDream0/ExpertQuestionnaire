using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertQuestionnaire.Specification.ExpertGroupUser
{
    public class ByExpertGroupKey : BaseSpecification<POCO.ExpertGroupUser>
    {
        public ByExpertGroupKey(int expertGroupKey) : base(x => x.ExpertGroupKey == expertGroupKey)
        { }
    }
}