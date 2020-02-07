using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpertQuestionnaire.GUI.Entity;

namespace ExpertQuestionnaire.GUI.ViewModel
{
    public class QuestionnairesViewModel : ItemsViewModel<Entity.Questionnaire>
    {
        public QuestionnairesViewModel(Model.QuestionnaireModel questionnaireModel)
        {
            _questionnaireModel = questionnaireModel;
            Initialize();

            RefreshAction();
        }

        private Model.QuestionnaireModel _questionnaireModel;

        public override ObservableCollection<Questionnaire> Items => _questionnaireModel.Items;

        protected override void AddAction()
        {
            var newQuestionnaire = new Entity.Questionnaire();

            var view = ViewManager.GetEditQuestionnarie(newQuestionnaire, _questionnaireModel.MainRepository.Context);

            view.ShowDialog();

            var viewModel = view.DataContext as ViewModel.ItemEditViewModel;

            if (viewModel.IsOkClicked)
            { Items.Add(newQuestionnaire); }
        }

        protected override void EditAction()
        {
            if (ItemCurrent != null)
            {
                var view = ViewManager.GetEditQuestionnarie(ItemCurrent, _questionnaireModel.MainRepository.Context);

                view.ShowDialog();

                var viewModel = view.DataContext as ViewModel.ItemEditViewModel;

                if (viewModel.IsOkClicked)
                { IsSaved = false; }
            }
        }

        protected override void InnerSaveAction()
        {
            _questionnaireModel.Save();
        }

        protected override void InnerRefreshAction()
        {
            _questionnaireModel.Update();
        }
    }
}
