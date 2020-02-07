using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertQuestionnaire.Logic.Calculation
{
    public class ExpertAnswer
    {
        public ExpertAnswer(Question question, NamedKey expert, NamedKey answer)
        {
            Question = question;
            Expert = expert;
            Answer = answer;
        }

        public Question Question { get; private set; }
        public NamedKey Expert { get; private set; }
        public NamedKey Answer { get; private set; }

        /// <summary>
        /// Нормализованный ответ
        /// </summary>
        public double NormalizedAnswer { get; set; }

        /// <summary>
        /// Не нормализованный ответ
        /// </summary>
        public double NonNormalizedAnswer { get; set; }

        public override string ToString()
        {
            return $"{Question.Name}-{Expert.Name}-{Answer.Name}";
        }
    }
}