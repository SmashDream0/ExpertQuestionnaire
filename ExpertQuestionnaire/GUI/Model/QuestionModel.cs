using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpertQuestionnaire.GUI.Entity;

namespace ExpertQuestionnaire.GUI.Model
{
    public class QuestionModel : ItemsModel<Entity.Question, POCO.Question>
    {
        public QuestionModel(Repository.QuestionRepository questionRepository) : base(questionRepository)
        { }

        private Entity.Questionnaire _questionnaire;

        public Entity.Questionnaire Questionnaire
        {
            get => _questionnaire;
            set
            {
                _questionnaire = value;
                Update();
            }
        }

        protected override void InnerUpdate()
        {
            Items.Clear();

            if (Questionnaire != null && Questionnaire.Key > 0)
            {
                var result = (MainRepository as Repository.QuestionRepository).FindByQuestionnaireKey(Questionnaire.Key);

                foreach (var question in result)
                {
                    var newQuestion = CreateItem(question);

                    Items.Add(newQuestion);
                }
            }
        }

        protected override Question CreateItem(params object[] parameters)
        {
            var item = base.CreateItem(parameters);

            item.Questionnaire = Questionnaire;

            return item;
        }
    }
}
