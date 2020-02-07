using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ExpertQuestionnaire.GUI.ViewModel
{
    public class UserViewModel : ItemEditViewModel
    {
        public UserViewModel(Entity.User user)
        {
            _user = user;

            Initialize();
        }

        private Entity.User _user
        { get; set; }

        public string Name
        {
            get => _user.Name;
            set { _user.Name = value; }
        }

        public string Password
        {
            get => _user.Password;
            set { _user.Password = value; }
        }

        public bool IsAdmin
        {
            get => _user.IsAdmin;
            set { _user.IsAdmin = value; }
        }
    }
}