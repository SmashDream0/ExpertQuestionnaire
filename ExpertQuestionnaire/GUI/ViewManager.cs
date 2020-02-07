using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ExpertQuestionnaire.GUI
{
    public static class ViewManager
    {
        public const string ReadOnlyFlag = "Read";
        public const string EditFlag = "Edit";

        private static Dictionary<Window, string> _windowFlagDict = new Dictionary<Window, string>();
        private static Dictionary<object, Window> _viewModelFlagDict = new Dictionary<object, Window>();

        private static void View_Closed(object sender, EventArgs e)
        {
            string flag;
            if (_windowFlagDict.TryGetValue(sender as Window, out flag))
            { _windowFlagDict.Remove(sender as Window); }

            var viewModel = (sender as Window).DataContext;

            _viewModelFlagDict.Remove(viewModel);

            Binds.Injector.DestroyInstance(viewModel.GetType());
            Binds.Injector.DestroyInstance(viewModel.GetType(), flag);
        }

        public static void CloseView<TViewModel>(TViewModel viewModel)
        {
            Window view;

            if (_viewModelFlagDict.TryGetValue(viewModel, out view))
            { view.Close(); }
        }

        public static Window GetView<TViewModel>(string flag = null)
        { return GetView<TViewModel>(flag, false, null); }

        public static Window GetView<TViewModel>(string flag, object[] parameters)
        { return GetView<TViewModel>(flag, false, parameters); }

        public static Window GetView<TViewModel>(string flag, bool randomizeParams, object[] parameters)
        {
            flag = flag ?? Binds.ViewBindFlag;

            var view = Binds.Injector.GetInstance<TViewModel>(flag) as Window;

            if (view.DataContext == null)
            {
                object viewModel;
                if (randomizeParams)
                { viewModel = Binds.Injector.GetInstanceWithRandomParameters<TViewModel>(null, parameters); }
                else
                { viewModel = Binds.Injector.GetInstance<TViewModel>(null, parameters); }
                view.DataContext = viewModel;
                _windowFlagDict.Add(view, flag);
                _viewModelFlagDict.Add(viewModel, view);
            }

            view.Closed -= View_Closed;
            view.Closed += View_Closed;

            return view;
        }

        public static Window GetEditQuestionnarie(Entity.Questionnaire questionnaire, Context.IContext context = null)
        {
            return GetView<ViewModel.QuestionnaireViewModel>(null, true, new object[] { questionnaire, context ?? new Context.Context() });
        }

        public static Window GetEditWorkQuestionnarie(Entity.WorkQuestionnaire workQuestionnaire, Context.IContext context = null)
        {
            return GetView<ViewModel.WorkQuestionnaireViewModel>(EditFlag, true, new object[] { workQuestionnaire, context ?? new Context.Context() });
        }

        public static Window GetReadWorkQuestionnarie(Entity.WorkQuestionnaire workQuestionnaire, Entity.User expert, Context.IContext context = null)
        {
            return GetView<ViewModel.WorkQuestionnaireViewModel>(ReadOnlyFlag, true, new object[] { workQuestionnaire, expert, context ?? new Context.Context() });
        }

        public static bool SelectItemView<T>(IEnumerable<T> items, out T item)
            where T : Entity.IDTO
        {
            var view = GetView<GUI.ViewModel.SelectItemViewModel<T>>(null, true, new object[] { items, new Context.Context() });

            view.ShowDialog();

            var viewModel = view.DataContext as ViewModel.SelectItemViewModel<T>;

            if (viewModel.IsSelected)
            {
                item = viewModel.ItemCurrent;
                return true;
            }
            else
            {
                item = default(T);
                return false;
            }
        }
    }
}