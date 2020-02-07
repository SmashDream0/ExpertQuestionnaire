using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ExpertQuestionnaire.GUI.ViewModel
{
    public class AdminViewModel : Misc.BaseNotifier
    {
        public AdminViewModel()
        { Initialize(); }

        private void Initialize()
        {
            ShowUsersViewCommand = new Misc.Command(ShowUsersAction);
            ShowExpertGroupsViewCommand = new Misc.Command(ShowExpertGroupsAction);
            ShowQuestionnairesViewCommand = new Misc.Command(ShowQuestionnairesAction);
            ShowWorkQuestionnairesViewCommand = new Misc.Command(ShowWorkQuestionnairesViewAction);
            ShowExpertAnswersCommand= new Misc.Command(ShowExpertAnswersAction);
        }

        private void ShowWorkQuestionnairesViewAction()
        {
            var view = ViewManager.GetView<ViewModel.WorkQuestionnairesViewModel>(null, true, new object[] { new Context.Context() });

            view.ShowDialog();
        }

        private void ShowUsersAction()
        {
            var view = ViewManager.GetView<ViewModel.UsersViewModel>(null, true, new object[] { new Context.Context() });

            view.ShowDialog();
        }

        private void ShowExpertGroupsAction()
        {
            var view = ViewManager.GetView<ViewModel.ExpertGroupsViewModel>(null, true, new object[] { new Context.Context() });

            view.ShowDialog();
        }

        private void ShowQuestionnairesAction()
        {
            var view = ViewManager.GetView<ViewModel.QuestionnairesViewModel>(null, true, new object[] { new Context.Context() });

            view.ShowDialog();
        }

        private void ShowExpertAnswersAction()
        {
            var view = ViewManager.GetView<ViewModel.ExpertAnswers.ExpertAnswersViewModel>(null, true, new object[] { new Context.Context() });

            view.ShowDialog();
        }

        public ICommand ShowUsersViewCommand
        { get; private set; }

        public ICommand ShowExpertGroupsViewCommand
        { get; private set; }

        public ICommand ShowQuestionnairesViewCommand
        { get; private set; }

        public ICommand ShowWorkQuestionnairesViewCommand
        { get; private set; }

        public ICommand ShowExpertAnswersCommand
        { get; private set; }
    }
}