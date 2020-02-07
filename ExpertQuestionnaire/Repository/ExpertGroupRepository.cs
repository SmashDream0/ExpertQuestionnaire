using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertQuestionnaire.Repository
{
    public class ExpertGroupRepository : BaseRepository<POCO.ExpertGroup>
    {
        public ExpertGroupRepository(Context.IContext context) : base(context)
        { }
    }
}