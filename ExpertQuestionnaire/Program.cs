using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpertQuestionnaire
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            //var timer = new Timer();
            //timer.Interval = 2*60*1000;
            //timer.Tick += (s, e) => System.Environment.Exit(1);
            //timer.Enabled = true;
            //timer.Start();

            Binds.MakeBind();

            GUI.Entity.User user;

            if (SelectUser(out user))
            {
                if (user.IsAdmin)
                {
                    var view = GUI.ViewManager.GetView<GUI.ViewModel.AdminViewModel>();

                    view.ShowDialog();
                }
                else
                {
                    GUI.Entity.WorkQuestionnaire workQuestionnaire;

                    var repository = Binds.Injector.GetInstance<Repository.WorkQuestionnaireRepository>() as Repository.WorkQuestionnaireRepository;
                    var settings = new Logic.SettingsLogic();

                    while (true)
                    {
                        var items = repository.FindByUserKeyAndAnswers(user.Key, settings.AllowReanswer);

                        if (!GUI.ViewManager.SelectItemView(items.Select(x => new GUI.Entity.WorkQuestionnaire(x)).ToArray(), out workQuestionnaire))
                        { break; }

                        var view = GUI.ViewManager.GetReadWorkQuestionnarie(workQuestionnaire, user, repository.Context);

                        view.ShowDialog();
                    }
                }
            }
        }

        private static bool SelectUser(out GUI.Entity.User user)
        {
            var mainView = GUI.ViewManager.GetView<GUI.ViewModel.StartViewModel>();

            mainView.ShowDialog();

            var startViewModel = mainView.DataContext as GUI.ViewModel.StartViewModel;

            if (startViewModel.IsLogin)
            {
                user = startViewModel.SelectedUserItem;
                return true;
            }
            else
            {
                user = null;
                return false;
            }
        }
    }
}