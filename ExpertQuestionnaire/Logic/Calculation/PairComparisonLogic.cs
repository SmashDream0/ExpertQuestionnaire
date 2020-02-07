using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertQuestionnaire.Logic.Calculation
{
    /// <summary>
    /// Метод парных сравнений.
    /// </summary>
    public class PairComparisonLogic : BaseCalculation
    {
        public PairComparisonLogic(IEnumerable<ExpertAnswer> expertAnswers)
            : base(expertAnswers)
        { }

        public override void Calculate()
        {
            var resultDictionary = base.Questions.ToDictionary(x => x, x => 0d);

            double summ = 0;

            foreach (var expertAnswer in base.ExpertAnswer)
            {
                resultDictionary[expertAnswer.Question] += expertAnswer.NormalizedAnswer;
                summ += expertAnswer.NormalizedAnswer;
            }

            var qIndexDictionary = new Dictionary<Question, int>();

            int index = 0;
            foreach (var question in Questions)
            {
                resultDictionary[question] /= summ;

                qIndexDictionary.Add(question, index);
                index++;
            }

            var qResults = resultDictionary.Select(x => new KeyValuePair<Question, double>(x.Key, Math.Round(x.Value, 2))).ToArray();

            Results = new bool?[index, index];

            foreach (var qResult1 in qResults)
            {
                var q1Index = qIndexDictionary[qResult1.Key];

                foreach (var qResult2 in qResults)
                {
                    var q2Index = qIndexDictionary[qResult2.Key];

                    if (qResult1.Value == qResult2.Value)
                    { Results[q1Index, q2Index] = null; }
                    else
                    { Results[q1Index, q2Index] = qResult1.Value > qResult2.Value; }
                }
            }
        }

        /// <summary>
        /// Таблица парных сравнений, ранжир относительно Questions
        /// true - больше
        /// false - меньше
        /// null - равно
        /// </summary>
        public bool?[,] Results
        { get; private set; }
    }
}