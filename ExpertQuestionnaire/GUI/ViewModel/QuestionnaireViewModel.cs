using ExpertQuestionnaire.GUI.ViewModel.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ExpertQuestionnaire.GUI.ViewModel
{
    public class QuestionnaireViewModel : ItemEditViewModel
    {
        public QuestionnaireViewModel(Entity.Questionnaire questionnaire, ScrollItemsViewModel scrollItemsViewModel, Context.IContext context) :
            this(scrollItemsViewModel, context)
        { Questionnaire = questionnaire; }

        public QuestionnaireViewModel(ScrollItemsViewModel scrollItemsViewModel, Context.IContext context) : base()
        {
            _context = context;
            ScrollVM = scrollItemsViewModel;

            Initialize();

            Questions.CollectionChanged += Questions_CollectionChanged;
            Answers.CollectionChanged += Answers_CollectionChanged;
            ScrollVM.PropertyChanged += ScrollItemsViewModel_PropertyChanged;
        }

        private Entity.Answer _answerCurrent;
        private Entity.Question _questionCurrent;
        private Entity.Questionnaire _questionnaire;
        private Context.IContext _context;

        private readonly List<Entity.Question> _removeQuestion = new List<Entity.Question>();
        private readonly List<Entity.Answer> _removeAnswer = new List<Entity.Answer>();

        public event Action<Entity.Question> OnQuestionChanged;
        public event Action<Entity.Answer> OnAnswerChanged;

        /// <summary>
        /// Прокрутка
        /// </summary>
        public ScrollItemsViewModel ScrollVM
        { get; private set; }

        /// <summary>
        /// Опрос
        /// </summary>
        public Entity.Questionnaire Questionnaire
        {
            get => _questionnaire;
            set
            {
                _questionnaire = value;

                if (_questionnaire != null)
                {
                    Questions.CollectionChanged -= Questions_CollectionChanged;
                    Questions.Clear();

                    foreach (var question in Questionnaire.Questions)
                    { Questions.Add(question); }

                    Questions.CollectionChanged += Questions_CollectionChanged;

                    ScrollVM.MaxNumber = Questions.Count;
                    ScrollVM.MinNumber = 1;
                    ScrollVM.CurrentNumber = 1;
                }

                PropertyChangedAction("Questions");
                PropertyChangedAction("Questionnaire");
            }
        }

        /// <summary>
        /// Вопросы
        /// </summary>
        public ObservableCollection<Entity.Question> Questions { get; private set; }

        /// <summary>
        /// Ответы
        /// </summary>
        public ObservableCollection<Entity.Answer> Answers { get; private set; }

        /// <summary>
        /// Текущий вопрос
        /// </summary>
        public Entity.Question QuestionCurrent
        {
            get => _questionCurrent;
            private set
            {
                _questionCurrent = value;

                Answers.CollectionChanged -= Answers_CollectionChanged;

                Answers.Clear();
                foreach (var answer in QuestionCurrent.Answers)
                { Answers.Add(answer); }

                Answers.CollectionChanged += Answers_CollectionChanged;

                PropertyChangedAction("Answers");
                PropertyChangedAction("QuestionCurrent");

                OnQuestionChanged?.Invoke(QuestionCurrent);
            }
        }

        public Entity.Answer AnswerCurrent
        {
            get => _answerCurrent;
            set
            {
                _answerCurrent = value;
                PropertyChangedAction("AnswerCurrent");

                OnAnswerChanged?.Invoke(AnswerCurrent);
            }
        }

        public ICommand AddQuestionCommand
        { get; private set; }
        public ICommand DeleteQuestionCommand
        { get; private set; }

        public ICommand AddAnswerCommand
        { get; private set; }
        public ICommand DeleteAnswerCommand
        { get; private set; }

        private void Answers_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        { Save<Entity.Answer, POCO.Answer>(e, QuestionCurrent.Answers as List<Entity.Answer>, _removeAnswer); }

        private void Questions_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Save<Entity.Question, POCO.Question>(e, Questionnaire.Questions as List<Entity.Question>, _removeQuestion);
            ScrollVM.MaxNumber = Questions.Count;
        }

        private void ScrollItemsViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (Questions.Any() && e.PropertyName == "CurrentNumber")
            { QuestionCurrent = Questions[ScrollVM.CurrentNumber - 1]; }
        }

        private static void Save<T1, T2>(NotifyCollectionChangedEventArgs e, IList<T1> list, IList<T1> removeList)
            where T2 : POCO.BasePOCO
            where T1 : GUI.Entity.BaseTypedDTO<T2>
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (var item in e.NewItems)
                    {
                        var typedItem = item as T1;

                        list.Add(typedItem);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (var item in e.OldItems)
                    {
                        var typedItem = item as T1;

                        for (int index = 0; index < list.Count; index++)
                        {
                            if (list[index].Key == typedItem.Key)
                            {
                                list.RemoveAt(index);

                                removeList.Add(typedItem);
                                break;
                            }
                        }
                    }

                    break;
            }
        }

        protected override void Initialize()
        {
            base.Initialize();
            Answers = new ObservableCollection<Entity.Answer>();
            Questions = new ObservableCollection<Entity.Question>();

            AddQuestionCommand = new Misc.Command(AddQuestionAction);
            DeleteQuestionCommand = new Misc.Command(DeleteQuestionAction);

            AddAnswerCommand = new Misc.Command(AddAnswerAction);
            DeleteAnswerCommand = new Misc.Command(DeleteAnswerAction);
        }

        private void AddQuestionAction()
        {
            var newItem = new Entity.Question();
            newItem.Questionnaire = Questionnaire;

            Questions.Add(newItem);
            ScrollVM.CurrentNumber = Questions.Count;
        }

        private void DeleteQuestionAction()
        {
            if (QuestionCurrent != null)
            { Questions.Remove(QuestionCurrent); }
        }

        private void AddAnswerAction()
        {
            if (QuestionCurrent != null)
            {
                var newItem = new Entity.Answer();
                newItem.Question = QuestionCurrent;
                Answers.Add(newItem);
            }
        }

        private void DeleteAnswerAction()
        {
            if (AnswerCurrent != null)
            { Answers.Remove(AnswerCurrent); }
        }

        protected override void OkAction()
        {
            base.OkAction();

            Questionnaire.Save();

            foreach (var question in _removeQuestion)
            { _context.Delete(question.InnerObject); }

            foreach (var answer in _removeAnswer)
            { _context.Delete(answer.InnerObject); }

            foreach (var q in Questionnaire.Questions)
            {
                foreach (var a in q.Answers)
                {
                    a.Save();

                    if (a.Key < 0)
                    { _context.Insert(a.InnerObject); }
                }

                q.Save();

                if (q.Key < 0)
                { _context.Insert(q.InnerObject); }
            }

            if (Questionnaire.Key < 0)
            { _context.Insert(Questionnaire.InnerObject); }
            else
            { _context.Attach(Questionnaire.InnerObject); }
        }

        protected override void CloseAction()
        {
            base.CloseAction();

            Questionnaire.Reset();

            var questionList = Questionnaire.Questions as List<Entity.Question>;

            for (int qi = 0; qi < questionList.Count; qi++)
            {
                var q = questionList[qi];

                if (q.Key < 0)
                {
                    questionList.RemoveAt(qi);
                    qi--;
                }
                else
                {
                    var answerList = q.Answers as List<Entity.Answer>;

                    for (int ai = 0; ai < answerList.Count; ai++)
                    {
                        var a = answerList[ai];

                        if (a.Key < 0)
                        {
                            answerList.RemoveAt(ai);
                            ai--;
                        }
                        else
                        { a.Reset(); }
                    }

                    q.Reset();
                }
            }
        }
    }
}