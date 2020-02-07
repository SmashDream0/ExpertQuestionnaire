using ExpertQuestionnaire.Specification.ExpertAnswer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertQuestionnaire.Repository
{
    public class ExpertAnswerRepository : BaseRepository<POCO.ExpertAnswer>
    {
        public ExpertAnswerRepository(Context.IContext context) : base(context)
        { }

        public IEnumerable<POCO.ExpertAnswer> FindByExpertKeyAndQuestionnaireKey(int expertKey, int questionnaireKey)
        {
            var specification = new ByExpertKey(expertKey) & new ByQuestionnaireKey(questionnaireKey);

            var result = Find(specification);

            return result;
        }
    }
}