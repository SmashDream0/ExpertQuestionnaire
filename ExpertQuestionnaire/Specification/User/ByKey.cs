using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertQuestionnaire.Specification.User
{
    public class ByKey : BaseSpecification<POCO.User>
    {
        public ByKey(params int[] keys) : base(x => keys.Contains(x.Key))
        { }
    }
}