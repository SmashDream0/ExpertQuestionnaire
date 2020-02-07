using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertQuestionnaire.GUI.Entity
{
    /// <summary>
    /// Опросник
    /// </summary>
    public partial class Questionnaire : BaseTypedDTO<POCO.Questionnaire>
    {
        static Questionnaire()
        { Initialize(typeof(Questionnaire)); }

        public Questionnaire(POCO.Questionnaire innerObject) : base(innerObject) { }
        public Questionnaire() : base() { }

        public override void Save()
        {
            base.Save();

            foreach (var question in Questions)
            {
                question.InnerObject.QuestionnaireKey = this.Key;
            }

            UpdateQuestionsCount();
        }

        public override void Reset()
        {
            base.Reset();

            var questions = new List<Question>();

            if (InnerObject.Question != null)
            {
                foreach (var question in InnerObject.Question)
                {
                    var newQuestion = new Question(question, false);

                    newQuestion.InnerObject.QuestionnaireKey = this.Key;
                    newQuestion.Questionnaire = this;

                    questions.Add(newQuestion);
                }
            }

            this.Questions = questions;

            UpdateQuestionsCount();
        }

        public override string ToString()
        { return Name; }

        public void AnswerAction(Answer answer, bool isAnswer)
        { OnSetAnswer?.Invoke(answer, isAnswer); }

        private void UpdateQuestionsCount()
        {
            var repository = (Binds.Injector.GetInstance<Repository.QuestionRepository>() as Repository.QuestionRepository);

            var count = repository.CountByQuestionnaireKey(InnerObject.Key);

            QuestionsCount = count;
        }

        private string _name;
        private string _purpose;
        private string _task;
        private int _questionsCount;

        /// <summary>
        /// Имя
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                PropertyChangedAction("Name");
            }
        }

        /// <summary>
        /// Цель
        /// </summary>
        public string Purpose
        {
            get { return _purpose; }
            set
            {
                _purpose = value;
                PropertyChangedAction("Purpose");
            }
        }


        /// <summary>
        /// Задача
        /// </summary>
        public string Task
        {
            get { return _task; }
            set
            {
                _task = value;
                PropertyChangedAction("Task");
            }
        }

        /// <summary>
        /// Кол-во вопросов
        /// </summary>
        public int QuestionsCount
        {
            get => _questionsCount;
            private set
            {
                _questionsCount = value;
                PropertyChangedAction("QuestionsCount");
            }
        }

        public IEnumerable<Question> Questions { get; private set; }

        public event Action<Answer, bool> OnSetAnswer;
    }
}