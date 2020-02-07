using ExpertQuestionnaire.Repository;
using ExpertQuestionnaire.GUI.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertQuestionnaire.GUI.Model
{
    public class MainModel
    {
        public MainModel(WorkQuestionnaireRepository workQuestionnarieRepository)
        {
            _workQuestionnarieRepository = workQuestionnarieRepository;
            Initialize();
        }

        private void Initialize()
        {
            WorkQuestionnaires = new ObservableCollection<WorkQuestionnaire>();
            WorkQuestionnaires.CollectionChanged += Questionnaires_CollectionChanged;

            Update();
        }

        private IEnumerable<WorkQuestionnaire> GetQuestionnaires()
        {
            var result = _workQuestionnarieRepository.All();

            return result.Select(x => new WorkQuestionnaire(x)).ToArray();
        }

        private void Questionnaires_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    var questionnaries = e.NewItems.Cast<WorkQuestionnaire>();

                    foreach (var questionnarie in questionnaries)
                    { questionnarie.Save(); }

                    _workQuestionnarieRepository.Insert(questionnaries.Select(x => x.InnerObject).ToArray());
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    _workQuestionnarieRepository.Delete(e.NewItems.Cast<WorkQuestionnaire>().Select(x => x.InnerObject).ToArray());
                    break;
            }
        }

        /// <summary>
        /// Обновить список опросов
        /// </summary>
        public void Update()
        {
            WorkQuestionnaires.Clear();

            var result = GetQuestionnaires();

            foreach (var questionnaire in result)
            { WorkQuestionnaires.Add(questionnaire); }
        }

        private readonly WorkQuestionnaireRepository _workQuestionnarieRepository;

        public ObservableCollection<WorkQuestionnaire> WorkQuestionnaires
        { get; private set; }
    }
}
