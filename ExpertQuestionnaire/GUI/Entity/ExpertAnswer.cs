using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertQuestionnaire.GUI.Entity
{
    /// <summary>
    /// Ответ эксперта
    /// </summary>
    public partial class ExpertAnswer : BaseTypedDTO<POCO.ExpertAnswer>
    {
        static ExpertAnswer()
        { Initialize(typeof(ExpertAnswer)); }

        public ExpertAnswer() : base()
        { }

        public ExpertAnswer(POCO.ExpertAnswer innerObject, bool allowInnerTypeCreation = false) : base(innerObject, allowInnerTypeCreation)
        { }

        public override void Reset()
        {
            base.Reset();

            if (AllowInnerTypeCreation)
            {
                if (Expert == null)
                { Expert = new User(InnerObject.Expert); }

                if (Answer == null)
                { Answer = new Answer(InnerObject.Answer); }
            }
        }

        public override void Save()
        {
            base.Save();

            if (Expert != null)
            { InnerObject.ExpertKey = Expert.Key; }

            if (WorkQuestionnaire != null)
            { InnerObject.WorkQuestionnaireKey = WorkQuestionnaire.Key; }

            if (Answer != null)
            { InnerObject.AnswerKey = Answer.Key; }
        }

        /// <summary>
        /// Сессия опроса
        /// </summary>
        public WorkQuestionnaire WorkQuestionnaire
        { get; set; }

        /// <summary>
        /// Эксперт
        /// </summary>
        public User Expert
        { get; set; }

        /// <summary>
        /// Выбранный ответ
        /// </summary>
        public Answer Answer
        { get; set; }

        /// <summary>
        /// Это выбранный ответ
        /// </summary>
        public bool IsAnswer
        { get; set; }

        public override string ToString()
        {
            return $"{Expert.Name} - {Answer.Text}";
        }
    }
}
