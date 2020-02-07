using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertQuestionnaire.GUI.Entity
{
    public partial class Question : BaseTypedDTO<POCO.Question>
    {
        static Question()
        { Initialize(typeof(Question)); }

        public Question(POCO.Question innerObject) : base(innerObject) { }
        public Question(POCO.Question innerObject, bool allowInnerTypeCreation) : base(innerObject, allowInnerTypeCreation) { }
        public Question() : base() { }

        public override void Save()
        {
            base.Save();

            foreach (var answer in Answers)
            {
                answer.InnerObject.QuestionKey = this.Key;
            }

            if (Questionnaire != null)
            { InnerObject.QuestionnaireKey = Questionnaire.Key; }
        }

        public override void Reset()
        {
            base.Reset();

            var answers = new List<Answer>();

            if (InnerObject.Answers != null)
            {
                foreach (var answer in InnerObject.Answers)
                {
                    var newAnswer = new Answer(answer, false);
                    newAnswer.Question = this;
                    answers.Add(newAnswer);
                }
            }

            Answers = answers;
        }

        private string _text;

        /// <summary>
        /// Текст вопроса
        /// </summary>
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                //PropertyChanged("Text");
            }
        }

        /// <summary>
        /// Ответы
        /// </summary>
        public IEnumerable<Answer> Answers
        { get; private set; }

        /// <summary>
        /// Опрос
        /// </summary>
        public Questionnaire Questionnaire
        { get; set; }
    }
}