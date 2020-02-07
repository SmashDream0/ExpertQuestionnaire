using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpertQuestionnaire.POCO;

namespace ExpertQuestionnaire.Repository
{
    public class ExpertGroupUserRepository : BaseRepository<POCO.ExpertGroupUser>
    {
        public ExpertGroupUserRepository(Context.IContext context) : base(context)
        { }

        public IEnumerable<POCO.ExpertGroupUser> FindByExpertGroupKey(int expertGroupKey)
        {
            var specification = new Specification.ExpertGroupUser.ByExpertGroupKey(expertGroupKey);

            var result = Find(specification);

            return result;
        }

        public int CountByExpertGroupKey(int expertGroupKey)
        {
            var specification = new Specification.ExpertGroupUser.ByExpertGroupKey(expertGroupKey);

            var result = Count(specification);

            return result;
        }

        protected override IQueryable<ExpertGroupUser> Sort(IQueryable<ExpertGroupUser> queryable)
        {
            return queryable.OrderBy(x=>x.Expert.Name);
        }
    }
}