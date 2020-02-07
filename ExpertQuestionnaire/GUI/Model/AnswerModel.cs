using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpertQuestionnaire.GUI.Entity;

namespace ExpertQuestionnaire.GUI.Model
{
    public class AnswerModel : ItemsModel<Entity.Answer, POCO.Answer>
    {
        public AnswerModel(Repository.AnswerRepository answerRepository) : base(answerRepository)
        { }
    }
}
