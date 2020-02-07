using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpertQuestionnaire.GUI.Entity;

namespace ExpertQuestionnaire.GUI.Model
{
    public class ExpertGroupUserModel : ItemsModel<Entity.ExpertGroupUser, POCO.ExpertGroupUser>
    {
        public ExpertGroupUserModel(Repository.ExpertGroupUserRepository expertGroupUserRepository, Repository.UserRepository userRepository) : base(expertGroupUserRepository)
        { _userRepository = userRepository; }

        private Entity.ExpertGroup _expertGroup;
        private Repository.UserRepository _userRepository;

        public Entity.ExpertGroup ExpertGroup
        {
            get => _expertGroup;
            set
            {
                _expertGroup = value;
                Update();
            }
        }

        protected override void InnerUpdate()
        {
            Items.Clear();

            if (_expertGroup != null && _expertGroup.Key > 0)
            {
                var result = (MainRepository as Repository.ExpertGroupUserRepository).FindByExpertGroupKey(_expertGroup.Key);

                foreach (var expertGroupUser in result)
                {
                    var newExpertGroupUser =  CreateItem(expertGroupUser);

                    Items.Add(newExpertGroupUser);
                }
            }
        }

        protected override ExpertGroupUser CreateItem(params object[] parameters)
        {
            var item = base.CreateItem(parameters);

            item.ExpertGroup = ExpertGroup;
            var user = _userRepository.FirstOrDefault(item.InnerObject.ExpertKey);

            item.Expert = new User(user);

            return item;
        }
    }
}
