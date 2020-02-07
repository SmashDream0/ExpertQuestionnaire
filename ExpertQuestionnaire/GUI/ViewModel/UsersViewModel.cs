using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpertQuestionnaire.GUI.Entity;

namespace ExpertQuestionnaire.GUI.ViewModel
{
    public class UsersViewModel : ItemsViewModel<Entity.User>
    {
        public UsersViewModel(Model.UserModel userModel)
        {
            _userModel = userModel;
            Initialize();

            RefreshAction();
        }

        private Model.UserModel _userModel;

        public override ObservableCollection<User> Items => _userModel.Items;

        protected override void AddAction()
        {
            var newUser = new Entity.User();

            var view = ViewManager.GetView<ViewModel.UserViewModel>(null, new object[] { newUser });

            view.ShowDialog();

            var viewModel = view.DataContext as ViewModel.UserViewModel;

            if (viewModel.IsOkClicked)
            { Items.Add(newUser); }
        }

        protected override void EditAction()
        {
            if (ItemCurrent != null)
            {
                var view = ViewManager.GetView<ViewModel.UserViewModel>(null, new object[] { ItemCurrent });

                view.ShowDialog();

                var viewModel = view.DataContext as ViewModel.UserViewModel;

                if (viewModel.IsOkClicked)
                {
                    IsSaved = false;
                }
            }
        }

        protected override void InnerSaveAction()
        {
            this._userModel.Save();
        }

        protected override void InnerRefreshAction()
        {
            _userModel.Update();
        }
    }
}
