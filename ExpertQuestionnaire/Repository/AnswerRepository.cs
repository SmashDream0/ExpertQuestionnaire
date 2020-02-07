using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpertQuestionnaire.POCO;

namespace ExpertQuestionnaire.Repository
{
    public class AnswerRepository : BaseRepository<POCO.Answer>
    {
        public AnswerRepository(Context.IContext context) : base(context)
        { }

        public IEnumerable<POCO.Answer> FindByQuestionKey(int questionKey)
        {
            var specification = new Specification.Answer.ByQuestionKey(questionKey);

            var result = Find(specification);

            return result;
        }

        protected override IQueryable<Answer> Sort(IQueryable<Answer> queryable)
        {
            return queryable.OrderBy(x => x.Text);
        }
    }
}