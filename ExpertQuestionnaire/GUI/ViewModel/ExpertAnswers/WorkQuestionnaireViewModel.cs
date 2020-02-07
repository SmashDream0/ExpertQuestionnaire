using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertQuestionnaire.GUI.ViewModel.ExpertAnswers
{
    public class WorkQuestionnaireViewModel
    {
        public WorkQuestionnaireViewModel(POCO.WorkQuestionnaire workQuestionnaire)
        {
            WorkQuestionnaire = workQuestionnaire;
            QuestionnaireName = WorkQuestionnaire.Questionnaire.Name;
            QuestionnairePurpose = WorkQuestionnaire.Questionnaire.Purpose;
            QuestionnaireTask = WorkQuestionnaire.Questionnaire.Task;

            ExpertGroupName = WorkQuestionnaire.ExpertGroup.Name;
        }

        private int? endedPercent = null;

        public POCO.WorkQuestionnaire WorkQuestionnaire
        { get; private set; }

        public string Name
            => $"{WorkQuestionnaire.Questionnaire.Name}-{WorkQuestionnaire.ExpertGroup.Name}";

        public string QuestionnaireName
        { get; private set; }
        public string QuestionnairePurpose
        { get; private set; }
        public string QuestionnaireTask
        { get; private set; }
        public string ExpertGroupName
        { get; private set; }

        public string Status
        {
            get
            {
                if (Percent == 100)
                { return "Завершен"; }
                else if (Percent == 0)
                { return "Не начат"; }
                else
                { return "Начат"; }
            }
        }

        /// <summary>
        /// Процент завершения
        /// </summary>
        public int Percent
        {
            get
            {
                if (!endedPercent.HasValue)
                {
                    int eCount = 0;
                    int eaCount = 0;

                    foreach (var q in Questions)
                    {
                        foreach (var expert in q.Experts)
                        {
                            eCount++;

                            if (expert.IsAnswer)
                            { eaCount++; }
                        }
                    }

                    double result = (double)eaCount / eCount;

                    endedPercent = (int)(result * 100);
                }

                return endedPercent.Value;
            }
        }

        public IEnumerable<QuestionViewModel> Questions
        { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}