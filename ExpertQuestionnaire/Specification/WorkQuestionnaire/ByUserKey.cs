using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertQuestionnaire.Specification.WorkQuestionnaire
{
    public class ByUserKey : BaseSpecification<POCO.WorkQuestionnaire>
    {
        public ByUserKey(int userKey) : base(x => x.ExpertGroup.Experts.FirstOrDefault(u => u.ExpertKey == userKey) != null)
        { }
    }
}