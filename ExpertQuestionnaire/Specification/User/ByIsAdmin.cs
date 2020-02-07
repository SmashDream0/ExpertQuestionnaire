using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertQuestionnaire.Specification.User
{
    public class ByIsAdmin : BaseSpecification<POCO.User>
    {
        public ByIsAdmin(bool isAdmin) : base(x => x.IsAdmin == isAdmin)
        { }
    }
}