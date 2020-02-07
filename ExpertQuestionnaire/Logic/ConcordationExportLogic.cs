using ExpertQuestionnaire.Logic.Calculation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertQuestionnaire.Logic
{
    public class ConcordationExportLogic
    {
        public ConcordationExportLogic(Calculation.ConcordationLogic logic)
        { _logic = logic; }

        private Calculation.ConcordationLogic _logic;

        public void Export(StringBuilder sb, int round)
        {
            _logic.Calculate();

            sb.AppendLine("Коэфициент конкордации");
            sb.AppendLine("Нормализованные ранги");

            var maxLength = _logic.Experts.Max(x => x.Name.Length);

            {
                const string qTitle = "Вопросы";

                string spaces = GetSpaces(qTitle, maxLength);

                sb.Append(spaces + qTitle);
            }

            int number = 0;
            foreach (var q in _logic.Questions)
            { sb.Append('\t'); sb.Append(++number); }

            int eIndex = 0;
            foreach (var expert in _logic.Experts)
            {
                sb.AppendLine();
                sb.Append(expert.Name);

                for (int i = 0; i < _logic.QCount; i++)
                { sb.Append('\t'); sb.Append(_logic.GetRang(eIndex, i)); }

                eIndex++;
            }

            sb.AppendLine();
            {
                const string qTitle = "Суммы";
                string spaces = GetSpaces(qTitle, maxLength);
                sb.Append(spaces + qTitle);
            }
            foreach (var sum in _logic.QSumms)
            { sb.Append('\t'); sb.Append(sum); }
            sb.Append('\t'); sb.Append(Math.Round(_logic.QSumms.Sum() / _logic.QCount, round));

            sb.AppendLine();
            {
                const string qTitle = "от стредн.";
                sb.AppendLine("Отклонен.");
                string spaces = GetSpaces(qTitle, maxLength);
                sb.Append(spaces + qTitle);
            }
            foreach (var sum in _logic.QDSumms)
            { sb.Append('\t'); sb.Append(sum); }

            sb.AppendLine();
            {
                const string qTitle = "Кв.ср.откл.";
                string spaces = GetSpaces(qTitle, maxLength);
                sb.Append(spaces + qTitle);
            }
            foreach (var sum in _logic.QDoubleSumms)
            { sb.Append('\t'); sb.Append(sum); }
            sb.Append('\t'); sb.Append(_logic.QDoubleSumms.Sum());

            sb.AppendLine();
            sb.AppendLine(_logic.CoefficientOfConcordance.CleanDigits); sb.Append("Итого: "); sb.Append(Math.Round(_logic.CoefficientOfConcordance.Summ, round).ToString());
            sb.AppendLine();
        }

        private static string GetSpaces(string title, int maxLength)
        {
            string spaces;
            if (maxLength > title.Length)
            { spaces = new string(' ', maxLength - title.Length); }
            else
            { spaces = String.Empty; }

            return spaces;
        }
    }
}
