using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertQuestionnaire.GUI.Entity
{
    public partial class User : BaseTypedDTO<POCO.User>
    {
        static User()
        { Initialize(typeof(User)); }

        public User(POCO.User user) : base(user) { }
        public User() : base() { }

        private string _name;
        private string _password;
        private bool _isAdmin;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                PropertyChangedAction("Name");
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                PropertyChangedAction("Password");
            }
        }
        public bool IsAdmin
        {
            get => _isAdmin;
            set
            {
                _isAdmin = value;
                PropertyChangedAction("IsAdmin");
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}