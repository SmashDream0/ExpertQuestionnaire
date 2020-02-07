using ExpertQuestionnaire.Logic;
using ExpertQuestionnaire.Logic.Calculation;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ExpertQuestionnaire.GUI.ViewModel.ExpertAnswers
{
    public class ExpertAnswersViewModel : Misc.BaseNotifier
    {
        public ExpertAnswersViewModel(Context.IContext context)
        {
            _context = context;
            Initialize();
        }

        private void InitializeWorkQuestionnaires()
        {
            var repository = Binds.Injector.GetInstance<Repository.WorkQuestionnaireRepository>(null, new object[] { _context }) as Repository.WorkQuestionnaireRepository;
            var items = repository.All();

            var workQuestionnaireList = new List<WorkQuestionnaireViewModel>();

            foreach (var item in items)
            {
                var workQuestionnaireItem = new WorkQuestionnaireViewModel(item);

                workQuestionnaireList.Add(workQuestionnaireItem);
            }

            WorkQuestionnaires = workQuestionnaireList.ToArray();
        }

        private void InitializeWorkQuestionnaire(WorkQuestionnaireViewModel workQuestionnaireCurrent)
        {
            if (workQuestionnaireCurrent.Questions == null && workQuestionnaireCurrent.WorkQuestionnaire.ExpertAnswers != null)
            {
                var questionDictionary = new Dictionary<int, QuestionViewModel>();

                foreach (var q in workQuestionnaireCurrent.WorkQuestionnaire.Questionnaire.Question)
                {
                    var question = new QuestionViewModel(q.Key, q.Text, new List<Entity.ExpertAnswer>());
                    questionDictionary.Add(q.Key, question);

                    foreach (var expert in workQuestionnaireCurrent.WorkQuestionnaire.ExpertGroup.Experts)
                    { (question.Experts as List<Entity.ExpertAnswer>).Add(new Entity.ExpertAnswer() { Expert = new Entity.User(expert.Expert) }); }
                }

                foreach (var expertAnswer in workQuestionnaireCurrent.WorkQuestionnaire.ExpertAnswers)
                {
                    if (expertAnswer.IsAnswer && questionDictionary.ContainsKey(expertAnswer.Answer.QuestionKey))
                    {
                        var q = questionDictionary[expertAnswer.Answer.QuestionKey];

                        foreach (var expert in q.Experts)
                        {
                            if (expert.Expert.Key == expertAnswer.ExpertKey)
                            {
                                expert.Answer = new Entity.Answer(expertAnswer.Answer);
                                expert.IsAnswer = expertAnswer.IsAnswer;
                            }
                        }
                    }
                }

                workQuestionnaireCurrent.Questions = questionDictionary.Values.OrderBy(x => x.Order).ToArray();
            }
        }

        private void Initialize()
        {
            ExportCommand = new Misc.Command(ExportAction, CanExportAction);
            SendCommand = new Misc.Command(SendAction, CanExportAction);
            CloseCommand = new Misc.Command(CloseAction);

            InitializeWorkQuestionnaires();
        }

        private void SendAction()
        {
            if (!String.IsNullOrEmpty(_fileName) || Export())
            {
                var view = ViewManager.GetView<ViewModel.SetValueViewModel>();
                view.ShowDialog();

                var viewModel = view.DataContext as SetValueViewModel;

                if (viewModel.IsContinued)
                {
                    var settingsLogic = new SettingsLogic();

                    var senderLogic = new EmailSendLogic(
                                            settingsLogic.SmtpURL, settingsLogic.SmtpPort, settingsLogic.EnableSSL,
                                            settingsLogic.EMail, settingsLogic.Password
                                            );
                    senderLogic.Send(_fileName, viewModel.Value);
                }
            }
        }

        private void ExportAction()
        {
            if (!String.IsNullOrEmpty(_fileName) || Export())
            { System.Diagnostics.Process.Start(_fileName); }
        }

        private bool Export()
        {
            var sfd = new SaveFileDialog();
            sfd.Filter = "txt (*.txt)|*.txt";

            if (sfd.ShowDialog().Value && ExportToFile(sfd.FileName))
            { return true; }
            else
            { return false; }
        }

        private bool ExportToFile(string fileName)
        {
            var questions = WorkQuestionnaireCurrent.WorkQuestionnaire.Questionnaire
                .Question.Select(q => new Question(q.Key, q.Text, q.Answers.Select(a => new NamedKey(a.Key, a.Text)).OrderBy(x => x.Key).ToList()));

            var qDict = questions.ToDictionary(x => x.Key, x => x);

            var expertAnswers = WorkQuestionnaireCurrent.WorkQuestionnaire
                .ExpertAnswers.Where(ea => ea.IsAnswer)
                        .Select(ea => new ExpertAnswer(
                            qDict[ea.Answer.QuestionKey],
                            new NamedKey(ea.ExpertKey, ea.Expert.Name),
                            new NamedKey(ea.AnswerKey, ea.Answer.Text)));

            var sb = new StringBuilder();

            new QuestionAnswerExportLogic(expertAnswers).Export(sb);

            {
                sb.AppendLine();
                var logic = new ConcordationLogic(expertAnswers);

                new ConcordationExportLogic(logic).Export(sb, 3);
            }

            if (SimpleRankingMethod)
            {
                sb.AppendLine();
                var logic = new SimpleRankingLogic(expertAnswers);

                new SimpleRankingExportLogic(logic).Export(sb, 3);
            }
            if (SettingWeightsMethod)
            {
                sb.AppendLine();
                var logic = new SettingWeightsLogic(expertAnswers);

                new SettingWeightsExportLogic(logic).Export(sb, 3);
            }
            if (PairComparisonMethod)
            {
                sb.AppendLine();
                var logic = new PairComparisonLogic(expertAnswers);

                new PairComparisonExportLogic(logic).Export(sb);
            }
            if (SuccessiveComparisonsMethod)
            {
                sb.AppendLine();
                var weights = new List<KeyValuePair<Question, double>> ();

                foreach (var q in WorkQuestionnaireCurrent.Questions)
                {
                    var findedQ = questions.First(x => x.Key == q.Order);

                    weights.Add(new KeyValuePair<Question, double>(findedQ, q.Weight));
                }

                var logic = new SuccessiveComparisonsLogic(weights.ToArray(), expertAnswers);

                new SuccessiveComparisonsExportLogic(logic).Export(sb, 3);
            }

            using (var fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read))
            {
                fs.SetLength(0);
                var text = sb.ToString();
                var bytes = Encoding.UTF8.GetBytes(text);
                fs.Write(bytes, 0, bytes.Length);
            }

            _fileName = fileName;

            return true;
        }

        private static double Round(double value)
        { return Math.Round(value, 3); }

        private void CloseAction()
        { ViewManager.CloseView(this); }

        private bool CanExportAction()
        {
            return (SimpleRankingMethod || SettingWeightsMethod || PairComparisonMethod || SuccessiveComparisonsMethod) && WorkQuestionnaireCurrent != null && WorkQuestionnaireCurrent.Percent == 100;
        }

        private string _fileName;

        private readonly Context.IContext _context;

        private bool _simpleRankingMethod;
        private bool _settingWeightsMethod;
        private bool _pairComparisonMethod;
        private bool _successiveComparisonsMethod;

        private WorkQuestionnaireViewModel _workQuestionnaireCurrent;

        public IEnumerable<WorkQuestionnaireViewModel> WorkQuestionnaires
        { get; private set; }

        public WorkQuestionnaireViewModel WorkQuestionnaireCurrent
        {
            get => _workQuestionnaireCurrent;
            set
            {
                _workQuestionnaireCurrent = value;

                InitializeWorkQuestionnaire(WorkQuestionnaireCurrent);
                ExportCommand.UpdateCanExecute();
                SendCommand.UpdateCanExecute();
                PropertyChangedAction("WorkQuestionnaireCurrent");

                _fileName = null;
            }
        }

        /// <summary>
        /// Метод простой ранжировки
        /// </summary>
        public bool SimpleRankingMethod
        {
            get => _simpleRankingMethod;
            private set
            {
                _simpleRankingMethod = value;

                ExportCommand.UpdateCanExecute();
                SendCommand.UpdateCanExecute();
                PropertyChangedAction("SimpleRankingMethod");

                _fileName = null;
            }
        }

        /// <summary>
        /// Метод задания весовых коэффициентов
        /// </summary>
        public bool SettingWeightsMethod
        {
            get => _settingWeightsMethod;
            private set
            {
                _settingWeightsMethod = value;

                ExportCommand.UpdateCanExecute();
                SendCommand.UpdateCanExecute();
                PropertyChangedAction("SettingWeightsMethod");

                _fileName = null;
            }
        }

        /// <summary>
        /// Метод парных сравнений
        /// </summary>
        public bool PairComparisonMethod
        {
            get => _pairComparisonMethod;
            private set
            {
                _pairComparisonMethod = value;

                ExportCommand.UpdateCanExecute();
                SendCommand.UpdateCanExecute();
                PropertyChangedAction("PairComparisonMethod");

                _fileName = null;
            }
        }

        /// <summary>
        /// Метод последовательных сравнений
        /// </summary>
        public bool SuccessiveComparisonsMethod
        {
            get => _successiveComparisonsMethod;
            private set
            {
                _successiveComparisonsMethod = value;

                ExportCommand.UpdateCanExecute();
                SendCommand.UpdateCanExecute();
                PropertyChangedAction("SuccessiveComparisonsMethod");

                _fileName = null;
            }
        }

        public Misc.IExecuteCommand ExportCommand
        { get; private set; }

        public Misc.IExecuteCommand SendCommand
        { get; private set; }

        public ICommand CloseCommand
        { get; private set; }
    }
}