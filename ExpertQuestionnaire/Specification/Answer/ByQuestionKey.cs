using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertQuestionnaire.Specification.Answer
{
    public class ByQuestionKey : BaseSpecification<POCO.Answer>
    {
        public ByQuestionKey(int questionKey) : base(x => x.QuestionKey == questionKey)
        { }
    }
}