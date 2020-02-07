using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertQuestionnaire.Logic.Calculation
{
    public abstract class BaseCalculation
    {
        public BaseCalculation(IEnumerable<ExpertAnswer> expertAnswers)
        {
            Initialize(expertAnswers);
        }

        private void Initialize(IEnumerable<ExpertAnswer> expertAnswers)
        {
            var questionDict = new Dictionary<int, Question>();
            var oldQuestionDict = new Dictionary<int, Question>();
            var expertDict = new Dictionary<int, NamedKey>();
            var expertAnswerDict = new Dictionary<int, List<NamedKey>>();
            var answerDict = new Dictionary<int, NamedKey>();

            var expertAnswerList = new List<ExpertAnswer>();

            foreach (var expertAnswer in expertAnswers)
            {
                if (!oldQuestionDict.ContainsKey(expertAnswer.Question.Key))
                { oldQuestionDict.Add(expertAnswer.Question.Key, expertAnswer.Question); }

                var expert = expertAnswer.Expert;

                if (expertDict.ContainsKey(expertAnswer.Expert.Key))
                { expert = expertDict[expertAnswer.Expert.Key]; }
                else
                {
                    //expert = new NamedKey(expertDict.Values.Count, expertAnswer.Expert.Name);

                    expertDict.Add(expertAnswer.Expert.Key, expert);
                }

                var question = expertAnswer.Question;

                if (questionDict.ContainsKey(expertAnswer.Question.Key))
                { question = questionDict[expertAnswer.Question.Key]; }
                else
                {
                    var answerList = new List<NamedKey>();

                    //question = new Question(question.Key, expertAnswer.Question.Name, answerList);
                    questionDict.Add(expertAnswer.Question.Key, question);
                    //expertAnswerDict.Add(question.Key, answerList);
                }

                var answer = expertAnswer.Answer;

                if (answerDict.ContainsKey(expertAnswer.Answer.Key))
                { answer = answerDict[expertAnswer.Answer.Key]; }
                else
                {
                    //answer = new NamedKey(answer.Key, expertAnswer.Answer.Name);
                    answerDict.Add(expertAnswer.Answer.Key, answer);

                    //expertAnswerDict[question.Key].Add(answer);
                }

                expertAnswerList.Add(new ExpertAnswer(question, expert, answer));
            }

            Questions = questionDict.OrderBy(x => x.Key).Select(x => x.Value).ToArray();
            Experts = expertDict.OrderBy(x => x.Key).Select(x => x.Value).ToArray();
            ExpertAnswer = expertAnswerList.ToArray();

            MaxNormalizedAnswer = Questions.Max(x => x.Answers.Count());
            MinNormalizedAnswer = Questions.Min(x => x.Answers.Count());

            foreach (var expertAnswer in ExpertAnswer)
            {
                int index = IndexOfKey(expertAnswer.Question.Answers, expertAnswer.Answer.Key);

                expertAnswer.NonNormalizedAnswer = index + 1;
                expertAnswer.NormalizedAnswer 
                    = expertAnswer.NonNormalizedAnswer * ((double)MaxNormalizedAnswer / expertAnswer.Question.Answers.Count());
            }
        }

        private static int IndexOfKey(IList<NamedKey> nameKeys, int key)
        {
            for (int i = 0; i < nameKeys.Count; i++)
            {
                if (nameKeys[i].Key == key)
                { return i; }
            }

            return -1;
        }

        public IEnumerable<Question> Questions
        { get; private set; }
        public IEnumerable<NamedKey> Experts
        { get; private set; }
        public IEnumerable<ExpertAnswer> ExpertAnswer
        { get; private set; }

        /// <summary>
        /// Максимальная нормализовання оценка
        /// </summary>
        public int MaxNormalizedAnswer
        { get; private set; }

        /// <summary>
        /// Минимальная нормализовання оценка
        /// </summary>
        public int MinNormalizedAnswer
        { get; private set; }

        public abstract void Calculate();

        protected Formulator.Formula GetFormula(string formulaText)
        { return new Formulator.Formula() { FormulaText = formulaText }; }
    }
}