using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertQuestionnaire.Repository
{
    public class QuestionnaireRepository : BaseRepository<POCO.Questionnaire>
    {
        public QuestionnaireRepository(Context.IContext context) : base(context)
        { }

        public POCO.Questionnaire FirstOrDefault(int key)
        {
            var specification = new Specification.Questionnaire.ByKey(key);

            var result = Context.AsQueryable<POCO.Questionnaire>().Where(specification.Predicate).FirstOrDefault();

            return result;
        }
    }
}