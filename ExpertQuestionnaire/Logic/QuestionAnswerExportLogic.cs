using ExpertQuestionnaire.Logic.Calculation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertQuestionnaire.Logic
{
    public class QuestionAnswerExportLogic : Calculation.BaseCalculation
    {
        public QuestionAnswerExportLogic(IEnumerable<ExpertAnswer> expertAnswers) : base(expertAnswers)
        { }

        public override void Calculate()
        {
            throw new NotImplementedException();
        }

        public void Export(StringBuilder sb)
        {
            var eDict = Experts.ToDictionary(x => x, x => new List<ExpertAnswer>());

            var maxLength = Experts.Max(x => x.Name.Length);

            foreach (var ea in base.ExpertAnswer)
            { eDict[ea.Expert].Add(ea); }

            const string qTitle = "Вопросы";

            string spaces;
            if (maxLength > qTitle.Length)
            { spaces = new string(' ', maxLength - qTitle.Length); }
            else
            { spaces = String.Empty; }
            
            sb.Append(spaces + qTitle);

            int number = 0;
            foreach (var q in Questions)
            { sb.Append('\t'); sb.Append(++number); }

            foreach (var e in eDict)
            {
                sb.AppendLine();

                sb.Append(e.Key.Name);

                var answers = e.Value.OrderBy(x => x.Question.Key).ToArray();

                foreach (var ea in answers)
                { sb.Append('\t'); sb.Append(ea.Answer.Name); }
            }
            sb.AppendLine();
        }
    }
}