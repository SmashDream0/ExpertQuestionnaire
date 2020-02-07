using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertQuestionnaire.GUI.Entity
{
    public partial class Answer : BaseTypedDTO<POCO.Answer>
    {
        static Answer()
        { Initialize(typeof(Answer)); }

        public Answer(POCO.Answer innerObject, bool allowInnerTypeCreation) : base(innerObject, allowInnerTypeCreation) { }
        public Answer(POCO.Answer innerObject) : base(innerObject) { }
        public Answer() : base() { }

        public override void Reset()
        {
            base.Reset();

            if (AllowInnerTypeCreation)
            { Question = new Question(InnerObject.Question); }
        }

        public override void Save()
        {
            base.Save();

            if (Question != null)
            { InnerObject.QuestionKey = Question.Key; }
        }

        private string _text;
        private bool _isAnswer;

        /// <summary>
        /// Текст ответа
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
        /// Это есть ответ на вопрос
        /// </summary>
        public bool IsAnswer
        {
            get => _isAnswer;
            set
            {
                if (_isAnswer != value)
                {
                    _isAnswer = value;

                    if (Question != null && Question.Questionnaire != null)
                    {
                        Question.Questionnaire.AnswerAction(this, value);

                        if (IsAnswer)
                        {
                            foreach (var answer in Question.Answers)
                            {
                                if (answer.Key != this.Key)
                                { answer.IsAnswer = false; }
                            }
                        }
                    }

                    PropertyChangedAction("IsAnswer");
                }
            }
        }

        /// <summary>
        /// Вопрос
        /// </summary>
        public Question Question
        { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}