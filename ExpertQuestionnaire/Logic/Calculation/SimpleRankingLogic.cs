using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertQuestionnaire.Logic.Calculation
{
    /// <summary>
    /// Метод простых рангов.
    /// </summary>
    public class SimpleRankingLogic : BaseCalculation
    {
        public SimpleRankingLogic(IEnumerable<ExpertAnswer> expertAnswers)
            : base(expertAnswers)
        { }

        public override void Calculate()
        {
            var formulaDictionary = base.Questions.ToDictionary(x => x, x => new StringBuilder());

            foreach (var expertAnswer in base.ExpertAnswer)
            {
                formulaDictionary[expertAnswer.Question].Append(expertAnswer.NormalizedAnswer);
                formulaDictionary[expertAnswer.Question].Append('+');
            }

            var resultList = formulaDictionary.Select(x => new KeyValuePair<Question, Formulator.Formula>(x.Key, GetFormula(x.Value)));

            Results = resultList.ToArray();
        }

        private Formulator.Formula GetFormula(StringBuilder sb)
        {
            sb.Length--;
            sb.Insert(0, '('); sb.Append(')'); sb.Append('/');
            sb.Append(Experts.Count());
            return GetFormula(sb.ToString());
        }

        public IEnumerable<KeyValuePair<Question, Formulator.Formula>> Results
        { get; private set; }
    }
}