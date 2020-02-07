using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertQuestionnaire.Specification.User
{
    public class ByExceptKey : BaseSpecification<POCO.User>
    {
        public ByExceptKey(params int[] keys) : base(x => !keys.Contains(x.Key))
        { }
    }
}