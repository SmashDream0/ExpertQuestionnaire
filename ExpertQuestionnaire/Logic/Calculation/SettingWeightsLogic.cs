using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpertQuestionnaire.Logic.Calculation
{
    /// <summary>
    /// Метод задания весовых коэффициентов.
    /// </summary>
    public class SettingWeightsLogic : BaseCalculation
    {
        public SettingWeightsLogic(IEnumerable<ExpertAnswer> expertAnswers)
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

            var resultDictionary = formulaDictionary.ToDictionary(x => x.Key, x => GetFormula(x.Value));

            SumFormula = GetFormula(String.Join("+", resultDictionary.Values.Select(x => x.Summ).OrderBy(x => x)));

            Results = resultDictionary.Select(x => new KeyValuePair<Question, Formulator.Formula>(x.Key, GetFormula(x.Value, SumFormula))).OrderBy(x => x.Value.Summ).ToArray();
        }

        private Formulator.Formula GetFormula(StringBuilder sb)
        {
            sb.Length--;
            sb.Insert(0, '('); sb.Append(')'); sb.Append('/');
            sb.Append(Experts.Count());
            return GetFormula(sb.ToString());
        }

        private Formulator.Formula GetFormula(Formulator.Formula sumFormula, Formulator.Formula totalSumFormula)
        {
            var newFormula = GetFormula($"s1/s2");
            newFormula.TrySetValue("s1", sumFormula);
            newFormula.TrySetValue("s2", totalSumFormula.Summ);

            return newFormula;
        }

        /// <summary>
        /// Формулы рассчета итоговых оценок по вопросам
        /// </summary>
        public IEnumerable<KeyValuePair<Question, Formulator.Formula>> Results
        { get; private set; }

        /// <summary>
        /// Формула рассчета средней оценки по вопросам
        /// </summary>
        public Formulator.Formula SumFormula
        { get; private set; }
    }
}