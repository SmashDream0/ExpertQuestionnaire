namespace ExpertQuestionnaire
{
    public static class Binds
    {
        static Binds()
        { Injector = new DependencyInjector.SimpleDI(); }

        public static void MakeBind()
        {
            Injector.Bind<GUI.ViewModel.ExpertAnswers.ExpertAnswersViewModel>().To<GUI.View.ExpertAnswersView>().WithFlag(ViewBindFlag);
            Injector.Bind<GUI.ViewModel.SetValueViewModel>().To<GUI.View.SetValueView>().WithFlag(ViewBindFlag);
            Injector.Bind<GUI.ViewModel.SelectItemViewModel<GUI.Entity.User>>().To<GUI.View.SelectItemView>().WithFlag(ViewBindFlag);
            Injector.Bind<GUI.ViewModel.SelectItemViewModel<GUI.Entity.Questionnaire>>().To<GUI.View.SelectItemView>().WithFlag(ViewBindFlag);
            Injector.Bind<GUI.ViewModel.SelectItemViewModel<GUI.Entity.WorkQuestionnaire>>().To<GUI.View.WorkQuestionnaireSelectView>().WithFlag(ViewBindFlag);
            Injector.Bind<GUI.ViewModel.SelectItemViewModel<GUI.Entity.ExpertGroup>>().To<GUI.View.SelectItemView>().WithFlag(ViewBindFlag);

            Injector.Bind<GUI.ViewModel.UserViewModel>().To<GUI.View.UserEditView>().WithFlag(ViewBindFlag);
            Injector.Bind<GUI.ViewModel.UsersViewModel>().To<GUI.View.UsersEditView>().WithFlag(ViewBindFlag);
            Injector.Bind<GUI.ViewModel.ExpertGroupViewModel>().To<GUI.View.ExpertGroupView>().WithFlag(ViewBindFlag);
            Injector.Bind<GUI.ViewModel.ExpertGroupsViewModel>().To<GUI.View.ExpertGroupsEditView>().WithFlag(ViewBindFlag);
            Injector.Bind<GUI.ViewModel.QuestionnaireViewModel>().To<GUI.View.QuestionnaireEditView>().WithFlag(ViewBindFlag);
            Injector.Bind<GUI.ViewModel.QuestionnairesViewModel>().To<GUI.View.QuestionnairesEditView>().WithFlag(ViewBindFlag);
            Injector.Bind<GUI.ViewModel.AdminViewModel>().To<GUI.View.AdminView>().WithFlag(ViewBindFlag);
            Injector.Bind<GUI.ViewModel.StartViewModel>().To<GUI.View.StartView>().WithFlag(ViewBindFlag);

            Injector.Bind<GUI.ViewModel.WorkQuestionnairesViewModel>().To<GUI.View.WorkQuestionnairesEditView>().WithFlag(ViewBindFlag);
            //Injector.Bind<GUI.ViewModel.WorkQuestionnairesViewModel>().To<GUI.View.WorkQuestionnairesView>(GUI.ViewManager.ReadOnlyFlag);

            Injector.Bind<GUI.ViewModel.WorkQuestionnaireViewModel>().To<GUI.View.WorkQuestionnaireEditView>().WithFlag(GUI.ViewManager.EditFlag);
            Injector.Bind<GUI.ViewModel.WorkQuestionnaireViewModel>().To<GUI.View.WorkQuestionnaireDoView>().WithFlag(GUI.ViewManager.ReadOnlyFlag);

            Injector.Bind<GUI.ViewModel.SetValueViewModel>().To<GUI.ViewModel.SetValueViewModel>()
                .NotSaveInstance();
            Injector.Bind<GUI.ViewModel.ExpertAnswers.ExpertAnswersViewModel>().To<GUI.ViewModel.ExpertAnswers.ExpertAnswersViewModel>()
                .NotSaveInstance();
            Injector.Bind<GUI.ViewModel.Controls.ScrollItemsViewModel>().To<GUI.ViewModel.Controls.ScrollItemsViewModel>()
                .NotSaveInstance();
            Injector.Bind<GUI.ViewModel.SelectItemViewModel<GUI.Entity.ExpertGroup>>().To<GUI.ViewModel.SelectItemViewModel<GUI.Entity.ExpertGroup>>()
                .NotSaveInstance();
            Injector.Bind<GUI.ViewModel.StartViewModel>().To<GUI.ViewModel.StartViewModel>()
                .NotSaveInstance();
            Injector.Bind<GUI.ViewModel.AdminViewModel>().To<GUI.ViewModel.AdminViewModel>()
                .NotSaveInstance();
            Injector.Bind<GUI.ViewModel.SelectItemViewModel<GUI.Entity.User>>().To<GUI.ViewModel.SelectItemViewModel<GUI.Entity.User>>()
                .NotSaveInstance();
            Injector.Bind<GUI.ViewModel.SelectItemViewModel<GUI.Entity.Questionnaire>>().To<GUI.ViewModel.SelectItemViewModel<GUI.Entity.Questionnaire>>()
                .NotSaveInstance();
            Injector.Bind<GUI.ViewModel.SelectItemViewModel<GUI.Entity.WorkQuestionnaire>>().To<GUI.ViewModel.SelectItemViewModel<GUI.Entity.WorkQuestionnaire>>()
                .NotSaveInstance();
            Injector.Bind<GUI.ViewModel.ExpertGroupViewModel>().To<GUI.ViewModel.ExpertGroupViewModel>()
                .NotSaveInstance();
            Injector.Bind<GUI.ViewModel.ExpertGroupsViewModel>().To<GUI.ViewModel.ExpertGroupsViewModel>()
                .NotSaveInstance();
            Injector.Bind<GUI.ViewModel.QuestionnaireViewModel>().To<GUI.ViewModel.QuestionnaireViewModel>()
                .NotSaveInstance();
            Injector.Bind<GUI.ViewModel.QuestionnairesViewModel>().To<GUI.ViewModel.QuestionnairesViewModel>()
                .NotSaveInstance();
            Injector.Bind<GUI.ViewModel.UserViewModel>().To<GUI.ViewModel.UserViewModel>()
                .NotSaveInstance();
            Injector.Bind<GUI.ViewModel.UsersViewModel>().To<GUI.ViewModel.UsersViewModel>()
                .NotSaveInstance();

            Injector.Bind<GUI.ViewModel.WorkQuestionnairesViewModel>().To<GUI.ViewModel.WorkQuestionnairesViewModel>()
                .NotSaveInstance();
            Injector.Bind<GUI.ViewModel.WorkQuestionnaireViewModel>().To<GUI.ViewModel.WorkQuestionnaireViewModel>()
                .NotSaveInstance();

            Injector.Bind<Context.IContext>().To<Context.Context>()
                .NotSaveInstance();

            Injector.Bind<GUI.Model.WorkQuestionnaireModel>().To<GUI.Model.WorkQuestionnaireModel>()
                .NotSaveInstance();
            Injector.Bind<GUI.Model.AnswerModel>().To<GUI.Model.AnswerModel>()
                .NotSaveInstance();
            Injector.Bind<GUI.Model.MainModel>().To<GUI.Model.MainModel>()
                .NotSaveInstance();
            Injector.Bind<GUI.Model.QuestionModel>().To<GUI.Model.QuestionModel>()
                .NotSaveInstance();
            Injector.Bind<GUI.Model.QuestionnaireModel>().To<GUI.Model.QuestionnaireModel>()
                .NotSaveInstance();
            Injector.Bind<GUI.Model.ExpertGroupModel>().To<GUI.Model.ExpertGroupModel>()
                .NotSaveInstance();
            Injector.Bind<GUI.Model.ExpertGroupUserModel>().To<GUI.Model.ExpertGroupUserModel>()
                .NotSaveInstance();
            Injector.Bind<GUI.Model.UserModel>().To<GUI.Model.UserModel>()
                .NotSaveInstance();

            Injector.Bind<Repository.UserRepository>().To<Repository.UserRepository>()
                .NotSaveInstance();
            Injector.Bind<Repository.ExpertGroupUserRepository>().To<Repository.ExpertGroupUserRepository>()
                .NotSaveInstance();
            Injector.Bind<Repository.ExpertGroupRepository>().To<Repository.ExpertGroupRepository>()
                .NotSaveInstance();
            Injector.Bind<Repository.AnswerRepository>().To<Repository.AnswerRepository>()
                .NotSaveInstance();
            Injector.Bind<Repository.QuestionnaireRepository>().To<Repository.QuestionnaireRepository>()
                .NotSaveInstance();
            Injector.Bind<Repository.QuestionRepository>().To<Repository.QuestionRepository>()
                .NotSaveInstance();
            Injector.Bind<Repository.WorkQuestionnaireRepository>().To<Repository.WorkQuestionnaireRepository>()
                .NotSaveInstance();
            Injector.Bind<Repository.ExpertAnswerRepository>().To<Repository.ExpertAnswerRepository>()
                .NotSaveInstance();
        }

        public const string SeflBindFlag = "Self";
        public const string ViewBindFlag = "View";

        /// <summary>
        /// Инжектор зависимостей
        /// </summary>
        public static DependencyInjector.SimpleDI Injector
        { get; private set; }
    }
}
