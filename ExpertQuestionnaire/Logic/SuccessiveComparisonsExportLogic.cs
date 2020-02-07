using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertQuestionnaire.Logic
{
    public class SuccessiveComparisonsExportLogic
    {
        public SuccessiveComparisonsExportLogic(Calculation.SuccessiveComparisonsLogic logic)
        { _logic = logic; }

        private Calculation.SuccessiveComparisonsLogic _logic;

        public void Export(StringBuilder sb, int round)
        {
            _logic.Calculate();

            sb.AppendLine("Метод последовательных сравнений");
            sb.AppendLine("Коэффициент значимости(к/з), умноженный на ранг");
            const string qTitle = "Решение";

            var maxLength = _logic.Questions.Max(x => x.Name.Length);

            string spaces;
            if (maxLength > qTitle.Length)
            { spaces = new string(' ', maxLength - qTitle.Length); }
            else
            { spaces = String.Empty; }

            sb.Append(spaces + qTitle + "\tк/з");

            for (int i = 0; i < _logic.MaxNormalizedAnswer; i++)
            { sb.Append('\t'); sb.Append(i + 1);  }

            sb.AppendLine();

            //Получаю суммы ответов по экспертам:
            //          ответ1  ответ2
            //вопрос1   1       2
            //вопрос2   1       2

            var qaDict = new Dictionary<KeyValuePair<int, int>, double>();

            foreach (var ea in _logic.ExpertAnswer)
            {
                var answerIndex = _logic.AnswerKeyIndex[ea.Answer.Key];

                var key = new KeyValuePair<int, int>(ea.Question.Key, answerIndex);

                if (!qaDict.ContainsKey(key))
                { qaDict.Add(key, 0); }

                qaDict[key]++;
            }

            foreach (var q in _logic.Questions)
            {
                sb.Append(q.Name); sb.Append('\t');

                var weight = _logic.Weights.First(x => x.Key.Key == q.Key).Value;

                sb.Append(weight); sb.Append('\t');

                foreach (var a in q.Answers)
                {
                    var answerIndex = _logic.AnswerKeyIndex[a.Key];

                    var key = new KeyValuePair<int, int>(q.Key, answerIndex);

                    if (!qaDict.ContainsKey(key))
                    { qaDict.Add(key, 0); }

                    var value = qaDict[key];

                    sb.Append(value); sb.Append('\t');
                }

                sb.AppendLine();
            }

            const string resultTitle = "Сумма";

            if (maxLength > resultTitle.Length)
            { spaces = new string(' ', maxLength - resultTitle.Length); }
            else
            { spaces = String.Empty; }

            sb.Append(spaces + resultTitle); sb.Append('\t');
            int index = 0;
            int maxIndex = -1;
            double maxValue = -1;

            foreach (var result in _logic.Results)
            {
                var value = Math.Round(result.Value, round);

                if (maxValue < value)
                {
                    maxValue = value;
                    maxIndex = index;
                }
                sb.Append('\t'); sb.Append(value);

                index++;
            }

            var totalResult = new KeyValuePair<int, double>(maxIndex, maxValue);

            sb.AppendLine();
            sb.AppendLine("Наиболее предпочтительным решением по вопросам является: " + (totalResult.Key + 1));
            sb.AppendLine("С результатом: " + totalResult.Value);

        }
    }
}