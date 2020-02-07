using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpertQuestionnaire.GUI.Entity;

namespace ExpertQuestionnaire.GUI.ViewModel
{
    public class WorkQuestionnairesViewModel : ItemsViewModel<Entity.WorkQuestionnaire>
    {
        public WorkQuestionnairesViewModel(Model.WorkQuestionnaireModel workQuestionnaireModel)
        {
            _workQuestionnaireModel = workQuestionnaireModel;
            Initialize();

            RefreshAction();
        }

        private Model.WorkQuestionnaireModel _workQuestionnaireModel;

        public override ObservableCollection<WorkQuestionnaire> Items => _workQuestionnaireModel.Items;

        protected override void AddAction()
        {
            var newWorkQuestionnaire = new Entity.WorkQuestionnaire();

            var view = ViewManager.GetEditWorkQuestionnarie(newWorkQuestionnaire, _workQuestionnaireModel.MainRepository.Context);

            view.ShowDialog();

            var viewModel = view.DataContext as WorkQuestionnaireViewModel;

            if (viewModel.IsOkClicked)
            { Items.Add(newWorkQuestionnaire); }
        }

        protected override void EditAction()
        {
            if (ItemCurrent != null)
            {
                var view = ViewManager.GetEditWorkQuestionnarie(ItemCurrent, _workQuestionnaireModel.MainRepository.Context);

                view.ShowDialog();

                var viewModel = view.DataContext as WorkQuestionnaireViewModel;

                if (viewModel.IsOkClicked)
                { IsSaved = false; }
                else
                { ItemCurrent.Reset(); }
            }
        }

        protected override void InnerSaveAction()
        {
            _workQuestionnaireModel.Save();
        }

        protected override void InnerRefreshAction()
        {
            _workQuestionnaireModel.Update();
        }
    }
}
