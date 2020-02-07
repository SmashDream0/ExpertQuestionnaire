using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertQuestionnaire.Logic
{
    public class PairComparisonExportLogic
    {
        public PairComparisonExportLogic(Calculation.PairComparisonLogic logic)
        { _logic = logic; }

        private Calculation.PairComparisonLogic _logic;

        public void Export(StringBuilder sb)
        {
            _logic.Calculate();

            sb.AppendLine("Метод парных сравнений");
            sb.AppendLine("Сравнение рангов голосов по вопросам друг с другом");

            sb.Append('\t');
            int qCount = 0;
            foreach (var question in _logic.Questions)
            {
                qCount++;

                sb.Append(qCount); sb.Append('\t');
            }

            qCount = 0;
            for (int i = 0; i < _logic.Results.GetLength(0); i++)
            {
                sb.AppendLine();
                qCount++;
                sb.Append(qCount); sb.Append('\t');

                for (int j = 0; j < _logic.Results.GetLength(1); j++)
                {
                    if (_logic.Results[i, j].HasValue)
                    { sb.Append(_logic.Results[i, j].Value ? ">" : "<"); sb.Append('\t'); }
                    else
                    { sb.Append("=\t"); }
                }
            }
            sb.AppendLine();
        }
    }
}
