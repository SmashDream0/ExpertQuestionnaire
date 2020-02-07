using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertQuestionnaire.GUI.ViewModel.ExpertAnswers
{
    public class QuestionViewModel
    {
        public QuestionViewModel(int order, string text, IEnumerable<Entity.ExpertAnswer> experts)
        {
            Order = order;
            Text = text;
            Experts = experts;
        }

        public int Order { get; private set; }

        public double Weight
        { get; set; }

        public string Text
        { get; private set; }

        public IEnumerable<Entity.ExpertAnswer> Experts
        { get; private set; }
    }
}
