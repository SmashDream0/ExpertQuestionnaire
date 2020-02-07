using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpertQuestionnaire.GUI.Entity;

namespace ExpertQuestionnaire.GUI.Model
{
    public class ExpertGroupModel : ItemsModel<Entity.ExpertGroup, POCO.ExpertGroup>
    {
        public ExpertGroupModel(Repository.ExpertGroupRepository expertGroupRepository) : base(expertGroupRepository)
        { }
    }
}
