using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertQuestionnaire.Logic
{
    public class SettingWeightsExportLogic
    {
        public SettingWeightsExportLogic(Calculation.SettingWeightsLogic logic)
        { _logic = logic; }

        private Calculation.SettingWeightsLogic _logic;

        public void Export(StringBuilder sb, int round)
        {
            _logic.Calculate();

            sb.AppendLine("Метод задания весовых коэффициентов");
            sb.AppendLine("Средняя оценка по вопросу, деленная на сумму средних оценок по вопросам");

            sb.AppendLine("Вопрос\tРассчет\tРезультат");
            foreach (var result in _logic.Results)
            { sb.AppendLine($"{result.Key.Name}\t{result.Value.CleanDigits}\t{Math.Round(result.Value.Summ, round)}"); }

            sb.Append("\t\t"); sb.Append(_logic.SumFormula.CleanDigits); sb.Append('='); sb.Append(_logic.SumFormula.Summ);
            sb.AppendLine();
            sb.AppendLine("Вопросы ранжированы согласно расчету");
        }
    }
}
