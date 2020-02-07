using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertQuestionnaire.Logic.Calculation
{
    /// <summary>
    /// Метод последовательных сравнений.
    /// </summary>
    public class SuccessiveComparisonsLogic : BaseCalculation
    {
        public SuccessiveComparisonsLogic(IEnumerable<KeyValuePair<Question, double>> weights, IEnumerable<ExpertAnswer> expertAnswers)
            : base(expertAnswers)
        {
            Weights = weights;
        }

        public override void Calculate()
        {
            var resultDictionary = new Dictionary<int, double>();

            var answerIndexDict = new Dictionary<int, int>();

            AnswerKeyIndex = answerIndexDict;

            foreach (var q in Questions)
            {
                int index = 0;

                foreach (var a in q.Answers)
                {
                    answerIndexDict.Add(a.Key, index);
                    index++;
                }
            }

            foreach (var expertAnswer in base.ExpertAnswer)
            {
                var answerIndex = answerIndexDict[expertAnswer.Answer.Key];

                if (!resultDictionary.ContainsKey(answerIndex))
                { resultDictionary.Add(answerIndex, 0); }

                var value = Weights.First(x => x.Key.Key == expertAnswer.Question.Key).Value;

                resultDictionary[answerIndex] += value;
            }

            Results = resultDictionary.ToArray();
        }

        public IEnumerable<KeyValuePair<Question, double>> Weights
        { get; private set; }

        public IReadOnlyDictionary<int, int> AnswerKeyIndex
        { get; private set; }

        public IEnumerable<KeyValuePair<int, double>> Results
        { get; private set; }
    }
}