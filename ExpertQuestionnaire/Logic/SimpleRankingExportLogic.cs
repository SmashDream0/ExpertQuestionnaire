using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertQuestionnaire.Logic
{
    public class SimpleRankingExportLogic
    {
        public SimpleRankingExportLogic(Calculation.SimpleRankingLogic logic)
        { _logic = logic; }

        private Calculation.SimpleRankingLogic _logic;

        public void Export(StringBuilder sb, int round)
        {
            _logic.Calculate();

            sb.AppendLine("Метод простой ранжировки");
            sb.AppendLine("Средняя оценка по вопросам");

            sb.AppendLine("Вопрос\tРассчет\tРезультат");
            foreach (var result in _logic.Results)
            { sb.AppendLine($"{result.Key.Name}\t{result.Value.CleanDigits}={Math.Round(result.Value.Summ, round)}"); }
        }
    }
}