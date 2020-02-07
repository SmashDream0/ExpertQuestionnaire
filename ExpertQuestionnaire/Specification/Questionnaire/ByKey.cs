using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertQuestionnaire.Specification.Questionnaire
{
    public class ByKey : BaseSpecification<POCO.Questionnaire>
    {
        public ByKey(int questionnaireKey) : base(x => x.Key == questionnaireKey)
        { }
    }
}