using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertQuestionnaire.Specification.ExpertAnswer
{
    public class ByQuestionnaireKey : BaseSpecification<POCO.ExpertAnswer>
    {
        public ByQuestionnaireKey(int questionnaireKey) : base(x => x.Answer.Question.QuestionnaireKey == questionnaireKey)
        { }
    }
}