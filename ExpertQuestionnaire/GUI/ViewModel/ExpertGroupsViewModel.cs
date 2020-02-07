using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpertQuestionnaire.GUI.Entity;

namespace ExpertQuestionnaire.GUI.ViewModel
{
    public class ExpertGroupsViewModel : ItemsViewModel<Entity.ExpertGroup>
    {
        public ExpertGroupsViewModel(Model.ExpertGroupModel expertGroupModel)
        {
            _expertGroupModel = expertGroupModel;
            Initialize();

            RefreshAction();
        }

        private Model.ExpertGroupModel _expertGroupModel;

        public override ObservableCollection<ExpertGroup> Items => _expertGroupModel.Items;

        protected override void AddAction()
        {
            var newExpertGroup = new Entity.ExpertGroup();

            var view = ViewManager.GetView<ViewModel.ExpertGroupViewModel>(null, true, new object[] { newExpertGroup, _expertGroupModel.MainRepository.Context });

            view.ShowDialog();

            var viewModel = view.DataContext as ViewModel.ExpertGroupViewModel;

            if (viewModel.IsOkClicked)
            {
                Items.Add(newExpertGroup);
            }
        }

        protected override void EditAction()
        {
            if (ItemCurrent != null)
            {
                var view = ViewManager.GetView<ViewModel.ExpertGroupViewModel>(null, true, new object[] { ItemCurrent, _expertGroupModel.MainRepository.Context });

                view.ShowDialog();

                var viewModel = view.DataContext as ViewModel.ExpertGroupViewModel;

                if (viewModel.IsOkClicked)
                {
                    IsSaved = false;
                }
            }
        }

        protected override void DeleteAction()
        {
            if (ItemCurrent != null)
            {
                _expertGroupModel.Items.Remove(ItemCurrent);
            }
        }

        protected override void InnerSaveAction()
        {
            this._expertGroupModel.Save();
        }

        protected override void InnerRefreshAction()
        {
            _expertGroupModel.Update();
        }
    }
}