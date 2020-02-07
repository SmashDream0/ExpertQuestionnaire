using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertQuestionnaire.GUI.ViewModel.ExpertAnswers
{
    public class QuestionnaireViewModel
    {
        public QuestionnaireViewModel(string name, IEnumerable<QuestionViewModel> questions)
        {
            Name = name;
            Questions = questions;
        }

        public string Name
        { get; private set; }

        public IEnumerable<QuestionViewModel> Questions
        { get; private set; }
    }
}
