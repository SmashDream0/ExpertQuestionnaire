using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertQuestionnaire.Logic.Calculation
{
    public class ConcordationLogic : BaseCalculation
    {
        public ConcordationLogic(IEnumerable<ExpertAnswer> expertAnswers)
            : base(expertAnswers)
        { }

        public override void Calculate()
        {
            var qCount = Questions.Count();
            var eCount = Experts.Count();

            var qDict = Questions.ToDictionary(x => x, x => 0d);
            int index = 0;
            var qIndexDict = Questions.ToDictionary(x => x, x => index++);
            index = 0;
            var eIndexDict = Experts.ToDictionary(x => x, x => index++);

            var matrix = new double[eCount, qCount];

            var coeffitient = (double)qCount / MaxNormalizedAnswer;

            //Нормализую ответы, относительно максимума, чтобы результат был меньше кол-ва вопросов
            foreach (var answer in ExpertAnswer)
            {
                var qIndex = qIndexDict[answer.Question];
                var eIndex = eIndexDict[answer.Expert];

                matrix[eIndex, qIndex] = answer.NormalizedAnswer * coeffitient;
            }

            //Нормализую ответы, убирая одинаковые ответы, не соотносящиеся с рангами
            Normalize(matrix);

            _normalizedMartix = matrix;

            //Сумма ответов по вопросам
            var totalSum = 0d;
            //Среднее значение ответов от сумм ответов по вопросам
            var middleSum = 0d;
            //Суммы ответов по вопросам
            var qSumm = new double[qCount]; QSumms = qSumm;
            //Отклонение от средней суммы ответов по вопросам
            var qDSumm = new double[qCount]; QDSumms = qDSumm;
            //Квадраты ответов по вопросам
            var qDoubleSumm = new double[qCount]; QDoubleSumms = qDoubleSumm;

            for (int q = 0; q < qCount; q++)
            {
                var qSum = 0d;

                for (int e = 0; e < eCount; e++)
                { qSum += matrix[e, q]; }

                totalSum += qSum;

                qSumm[q] = qSum;
            }

            middleSum = qSumm.Sum() / qSumm.Length;

            for (int q = 0; q < qDSumm.Length; q++)
            { qDSumm[q] = qSumm[q] - middleSum; }

            for (int q = 0; q < qDoubleSumm.Length; q++)
            { qDoubleSumm[q] = qDSumm[q] * qDSumm[q]; }

            S = qDoubleSumm.Sum();

            const string sName = "S";
            const string expName = "exp";
            const string queName = "que";

            CoefficientOfConcordance = GetFormula($"(12*{sName})/(({expName}*{expName})*({queName}*{queName}*{queName}-{queName}))");
            CoefficientOfConcordance.TrySetValue(sName, (decimal)S);
            CoefficientOfConcordance.TrySetValue(expName, eCount);
            CoefficientOfConcordance.TrySetValue(queName, qCount);
        }

        private static void Normalize(double[,] matrix)
        {
            var eCount = matrix.GetLength(0);
            var qCount = matrix.GetLength(1);

            for (int e = 0; e < eCount; e++)
            {
                var qIndexes = new int[qCount];
                for (int qIndex = 0; qIndex < qIndexes.Length; qIndex++)
                { qIndexes[qIndex] = qIndex; }

                qIndexes = qIndexes.OrderBy(x => matrix[e, x]).ToArray();

                var prevValue = matrix[e, qIndexes[0]];
                double sum = 0d;
                int indexFrom = -1;

                for (int q1 = 1; q1 < qCount; q1++)
                {
                    if (prevValue == matrix[e, qIndexes[q1]])
                    {
                        if (indexFrom < 0)
                        { indexFrom = q1 - 1; }

                        sum += q1;
                    }
                    else if (indexFrom > -1)
                    {
                        var middleSumM = (sum + q1) / (q1 - indexFrom);

                        for (int q2 = indexFrom; q2 < q1; q2++)
                        { matrix[e, qIndexes[q2]] = middleSumM; }

                        indexFrom = -1;
                        sum = 0d;
                    }

                    prevValue = matrix[e, qIndexes[q1]];
                }

                if (indexFrom > -1)
                {
                    var middleSumM = (sum + qCount) / (qCount - indexFrom);

                    for (int q2 = indexFrom; q2 < qCount; q2++)
                    { matrix[e, qIndexes[q2]] = middleSumM; }

                    indexFrom = -1;
                    sum = 0d;
                }
            }
        }

        private double[,] _normalizedMartix;

        public int ECount => _normalizedMartix.GetLength(0);
        public int QCount => _normalizedMartix.GetLength(1);

        public double GetRang(int eIndex, int qIndex)
        { return _normalizedMartix[eIndex, qIndex]; }

        /// <summary>
        /// Сумма квадратов ответов по вопросам
        /// </summary>
        public double S
        { get; private set; }

        /// <summary>
        /// Суммы ответов по вопросам
        /// </summary>
        public IEnumerable<double> QSumms
        { get; private set; }

        /// <summary>
        /// Отклонение от средней суммы ответов по вопросам
        /// </summary>
        public IEnumerable<double> QDSumms
        { get; private set; }

        /// <summary>
        /// Квадраты ответов по вопросам
        /// </summary>
        public IEnumerable<double> QDoubleSumms
        { get; private set; }

        /// <summary>
        /// Коэффициент конкордации
        /// </summary>
        public Formulator.Formula CoefficientOfConcordance
        { get; private set; }
    }
}