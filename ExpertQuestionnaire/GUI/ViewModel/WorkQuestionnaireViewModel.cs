using ExpertQuestionnaire.GUI.ViewModel.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ExpertQuestionnaire.GUI.ViewModel
{
    public class WorkQuestionnaireViewModel : ItemEditViewModel
    {
        public WorkQuestionnaireViewModel(Entity.WorkQuestionnaire workQuestionnaire, Entity.User user, QuestionnaireViewModel questionnaireVM, Model.QuestionnaireModel questionnaireModel, Model.ExpertGroupModel expertGroupModel, Model.WorkQuestionnaireModel workQuestionnaireModel)
            : this(questionnaireVM, questionnaireModel, expertGroupModel, workQuestionnaireModel)
        {
            WorkQuestionnaire = workQuestionnaire;
            Expert = user;

            if (WorkQuestionnaire.Questionnaire != null)
            {
                WorkQuestionnaire.Questionnaire.OnSetAnswer += Questionnaire_OnSetAnswer;

                var expertAnswerRepositiory = Binds.Injector.GetInstance<Repository.ExpertAnswerRepository>(null, questionnaireModel.MainRepository.Context) as Repository.ExpertAnswerRepository;

                var expertAnswers = expertAnswerRepositiory.FindByExpertKeyAndQuestionnaireKey(Expert.Key, this.WorkQuestionnaire.Questionnaire.Key);

                _expertAnswers.Clear();

                foreach (var expertAnswer in expertAnswers)
                {
                    var newExpertAnswer = new Entity.ExpertAnswer(expertAnswer);
                    newExpertAnswer.Expert = Expert;
                    newExpertAnswer.WorkQuestionnaire = WorkQuestionnaire;

                    _expertAnswers.Add(expertAnswer.Answer.Key, newExpertAnswer);
                }

                QuestionnaireVM.OnQuestionChanged += QuestionnaireVM_OnQuestionChanged;
            }

            QuestionnaireVM.Questionnaire = WorkQuestionnaire.Questionnaire;
        }

        public WorkQuestionnaireViewModel(Entity.WorkQuestionnaire workQuestionnaire, QuestionnaireViewModel questionnaireVM, Model.QuestionnaireModel questionnaireModel, Model.ExpertGroupModel expertGroupModel, Model.WorkQuestionnaireModel workQuestionnaireModel)
            : this(questionnaireVM, questionnaireModel, expertGroupModel, workQuestionnaireModel)
        {
            WorkQuestionnaire = workQuestionnaire;
            QuestionnaireVM.Questionnaire = WorkQuestionnaire.Questionnaire;
        }

        private WorkQuestionnaireViewModel(QuestionnaireViewModel questionnaireVM, Model.QuestionnaireModel questionnaireModel, Model.ExpertGroupModel expertGroupModel, Model.WorkQuestionnaireModel workQuestionnaireModel) : base()
        {
            _workQuestionnaireModel = workQuestionnaireModel;
            _expertGroupModel = expertGroupModel;
            _questionnaireModel = questionnaireModel;

            Initialize();

            QuestionnaireVM = questionnaireVM;
        }

        private Entity.User _expert;
        private readonly Model.WorkQuestionnaireModel _workQuestionnaireModel;
        private readonly Model.ExpertGroupModel _expertGroupModel;
        private readonly Model.QuestionnaireModel _questionnaireModel;
        private readonly Dictionary<int, Entity.ExpertAnswer> _expertAnswers = new Dictionary<int, Entity.ExpertAnswer>();

        /// <summary>
        /// Объект опроса
        /// </summary>
        public QuestionnaireViewModel QuestionnaireVM
        { get; private set; }

        /// <summary>
        /// Текущий опрос
        /// </summary>
        public Entity.WorkQuestionnaire WorkQuestionnaire
        { get; private set; }

        /// <summary>
        /// Текущий эксперт
        /// </summary>
        public Entity.User Expert
        {
            get => _expert;
            private set
            {
                _expert = value;
                PropertyChangedAction("Expert");
            }
        }

        private void Questionnaire_OnSetAnswer(Entity.Answer answer, bool isAnswer)
        {
            if (!_expertAnswers.ContainsKey(answer.Key))
            {
                if (!isAnswer)
                { return; }

                var expertAnswer = new Entity.ExpertAnswer();
                expertAnswer.Answer = answer;
                expertAnswer.Expert = this.Expert;
                expertAnswer.WorkQuestionnaire = WorkQuestionnaire;

                _expertAnswers.Add(answer.Key, expertAnswer);
            }

            _expertAnswers[answer.Key].IsAnswer = isAnswer;
        }

        private void QuestionnaireVM_OnQuestionChanged(Entity.Question question)
        {
            if (_expertAnswers.Any())
            {
                foreach (var answer in question.Answers)
                {
                    if (_expertAnswers.ContainsKey(answer.Key) && _expertAnswers[answer.Key].Answer == null)
                    {
                        _expertAnswers[answer.Key].Answer = answer;
                        answer.IsAnswer = _expertAnswers[answer.Key].IsAnswer;
                    }
                }
            }
        }

        protected override void Initialize()
        {
            base.Initialize();

            SelectQuestionnaireCommand = new Misc.Command(SelectQuestionnaireAction);
            SelectExpertCommand = new Misc.Command(SelectExpertGroupAction);
        }

        private void SelectQuestionnaireAction()
        {
            Entity.Questionnaire questionnaire;

            _questionnaireModel.Update();
            if (ViewManager.SelectItemView(_questionnaireModel.Items, out questionnaire))
            {
                QuestionnaireVM.Questionnaire = WorkQuestionnaire.Questionnaire = questionnaire;
                PropertyChangedAction("QuestionnaireViewModel");
            }
        }

        private void SelectExpertGroupAction()
        {
            Entity.ExpertGroup expertGroup;

            _expertGroupModel.Update();
            if (ViewManager.SelectItemView(_expertGroupModel.Items, out expertGroup))
            {
                WorkQuestionnaire.ExpertGroup = expertGroup;
                PropertyChangedAction("WorkQuestionnaire");
            }
        }

        protected override void OkAction()
        {
            base.OkAction();

            foreach (var expertAnswer in _expertAnswers.Values)
            {
                expertAnswer.Save();

                if (expertAnswer.Key > 0)
                { _workQuestionnaireModel.MainRepository.Context.Attach(expertAnswer.InnerObject); }
                else
                { _workQuestionnaireModel.MainRepository.Context.Insert(expertAnswer.InnerObject); }
            }

            _workQuestionnaireModel.Save();
        }

        public ICommand SelectQuestionnaireCommand
        { get; private set; }

        public ICommand SelectExpertCommand
        { get; private set; }
    }
}