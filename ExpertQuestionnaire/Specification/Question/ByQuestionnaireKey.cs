using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertQuestionnaire.Specification.Question
{
    public class ByQuestionnaireKey : BaseSpecification<POCO.Question>
    {
        public ByQuestionnaireKey(int questionnaireKey) : base(x => x.QuestionnaireKey == questionnaireKey)
        { }
    }
}