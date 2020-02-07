using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertQuestionnaire.GUI.Model
{
    public class UserModel : ItemsModel<Entity.User, POCO.User>
    {
        public UserModel(Repository.UserRepository userRepository) : base(userRepository)
        { }
    }
}