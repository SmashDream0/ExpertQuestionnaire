using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertQuestionnaire.Specification.WorkQuestionnaire
{
    public class ByAnswered : BaseSpecification<POCO.WorkQuestionnaire>
    {
        public ByAnswered(bool answered, int expertKey)
            : base(x => answered ?
                            true :
                            x.ExpertAnswers
                                .Where(ea => ea.ExpertKey == expertKey && ea.IsAnswer)
                                .GroupBy(a => a.Answer.Question)
                                .Count() < x.Questionnaire.Question.Count)
        { }
    }
}