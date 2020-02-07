using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertQuestionnaire.GUI.Model
{
    public class QuestionnaireModel : ItemsModel<Entity.Questionnaire, POCO.Questionnaire>
    {
        public QuestionnaireModel(Repository.QuestionnaireRepository questionnaireRepository) : base(questionnaireRepository)
        { }
    }
}