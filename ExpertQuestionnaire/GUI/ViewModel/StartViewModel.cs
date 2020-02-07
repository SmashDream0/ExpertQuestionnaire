using ExpertQuestionnaire.GUI.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ExpertQuestionnaire.GUI.ViewModel
{
    public class StartViewModel:Misc.BaseNotifier
    {
        public StartViewModel(Model.UserModel userModel)
        {
            _userModel = userModel;
            ErrorText = String.Empty;
            ContinueCommand = new Misc.Command(ContinueAction);

            _userModel.Update();
            if (!_userModel.Items.Any())
            { CreateDefaultUser(); }
        }

        private void ContinueAction()
        {
            if (SelectedUserItem == null)
            { ErrorText = "Выберите пользователя!"; }
            else if (Equals(SelectedUserItem.Password, _password))
            {
                IsLogin = true;
                GUI.ViewManager.CloseView(this);
            }
            else
            { ErrorText = "Пароль не верный!"; }
        }

        private bool Equals(string value1, string value2)
        {
            value1 = value1 ?? String.Empty;
            value2 = value2 ?? String.Empty;

            return value1.Equals(value2);
        }

        private void CreateDefaultUser()
        {
            var userItem = new User();

            userItem.Name = "Администратор";
            userItem.Password = "1";
            userItem.IsAdmin = true;

            UserItems.Add(userItem);
            _userModel.Save();

        }

        private void SetUserPassword(string password)
        {
            _password = password;
            ErrorText = String.Empty;
        }

        private string GetUserPassword()
        { return _password = ""; }

        private string _password;

        private Model.UserModel _userModel;

        private string _errorText;
        private User _selectedUserItem;

        public string ErrorText
        {
            get => _errorText;
            set
            {
                _errorText = value;
                PropertyChangedAction("ErrorText");
            }
        }

        public ObservableCollection<User> UserItems => _userModel.Items;

        public User SelectedUserItem
        {
            get => _selectedUserItem;
            set
            {
                _selectedUserItem = value;
                PropertyChangedAction("SelectedUserItem");
                ErrorText = String.Empty;
            }
        }

        public ICommand ContinueCommand
        { get; private set; }

        public bool IsLogin
        { get; private set; }
    }
}