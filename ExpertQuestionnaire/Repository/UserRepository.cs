using System.Collections.Generic;
using System.Linq;

namespace ExpertQuestionnaire.Repository
{
    public class UserRepository : BaseRepository<POCO.User>
    {
        public UserRepository(Context.IContext context) : base(context)
        { }

        public POCO.User FirstOrDefault(int key)
        {
            var specification = new Specification.User.ByKey(key);

            var result = Context.AsQueryable<POCO.User>().Where(specification.Predicate).FirstOrDefault();

            return result;
        }

        public IEnumerable<POCO.User> FindByIsAdminAndExceptKeys(bool isAdmin, params int[] keys)
        {
            var specification = new Specification.User.ByExceptKey(keys) & new Specification.User.ByIsAdmin(isAdmin);

            var result = Find(specification);

            return result;
        }

        public IEnumerable<POCO.User> FindByKeys(params int[] keys)
        {
            var specification = new Specification.User.ByKey(keys);

            var result = Find(specification);

            return result;
        }
    }
}