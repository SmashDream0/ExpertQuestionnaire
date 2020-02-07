using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpertQuestionnaire.GUI.Entity;

namespace ExpertQuestionnaire.GUI.Model
{
    public class WorkQuestionnaireModel : ItemsModel<Entity.WorkQuestionnaire, POCO.WorkQuestionnaire>
    {
        public WorkQuestionnaireModel(Repository.WorkQuestionnaireRepository workQuestionnaireRepository) : base(workQuestionnaireRepository)
        { }

        protected override WorkQuestionnaire CreateItem(params object[] parameters)
        {
            var item =  base.CreateItem(parameters);

            if (item.InnerObject.ExpertGroup != null)
            { item.ExpertGroup = new ExpertGroup(item.InnerObject.ExpertGroup); }
            if (item.InnerObject.Questionnaire != null)
            { item.Questionnaire = new Questionnaire(item.InnerObject.Questionnaire); }

            return item;
        }
    }
}