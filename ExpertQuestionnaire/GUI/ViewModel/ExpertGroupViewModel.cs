using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpertQuestionnaire.GUI.Entity;

namespace ExpertQuestionnaire.GUI.ViewModel
{
    public class ExpertGroupViewModel : ItemsViewModel<Entity.ExpertGroupUser>
    {
        public ExpertGroupViewModel(Entity.ExpertGroup expertGroup, Model.ExpertGroupUserModel expertGroupUserModel, Repository.UserRepository userRepository):base()
        {
            _expertGroupUserModel = expertGroupUserModel;
            _userRepository = userRepository;

            Initialize();
            ExpertGroup = expertGroup;
        }

        private readonly Model.ExpertGroupUserModel _expertGroupUserModel;
        private readonly Repository.UserRepository _userRepository;
        private Entity.ExpertGroup _expertGroup;

        public Entity.ExpertGroup ExpertGroup
        {
            get => _expertGroup;
            set
            {
                _expertGroup = value;
                RefreshAction();
            }
        }

        public override ObservableCollection<ExpertGroupUser> Items => _expertGroupUserModel.Items;

        protected override void AddAction()
        {
            var users = _userRepository.FindByIsAdminAndExceptKeys(false, Items.Select(x => x.Expert.Key).ToArray())
                .Select(x => new User(x)).ToArray();

            var selectItemView = ViewManager.GetView<ViewModel.SelectItemViewModel<User>>(null, new object[] { users });

            selectItemView.ShowDialog();

            var viewModel = selectItemView.DataContext as ViewModel.SelectItemViewModel<User>;

            if (viewModel.IsSelected && Items.FirstOrDefault(x=>x.Expert.InnerObject == viewModel.ItemCurrent.InnerObject) == null)
            {
                var newExpert = new ExpertGroupUser() { Expert = viewModel.ItemCurrent, ExpertGroup = ExpertGroup };

                (ExpertGroup.Experts as List<ExpertGroupUser>).Add(newExpert);
                Items.Add(newExpert);
            }
        }

        protected override void DeleteAction()
        {
            if (ItemCurrent != null)
            { Items.Remove(ItemCurrent); }
        }

        protected override void InnerRefreshAction()
        {
            _expertGroupUserModel.ExpertGroup = ExpertGroup;
        }

        protected override void OkAction()
        {
            base.OkAction();

            foreach (var expert in ExpertGroup.Experts)
            {
                if (expert.Key < 0)
                {
                    expert.Save();
                    _expertGroupUserModel.MainRepository.Insert(expert.InnerObject);
                }
            }
        }
    }
}