using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertQuestionnaire.GUI.Entity
{
    /// <summary>
    /// Сессия опроса
    /// </summary>
    public partial class WorkQuestionnaire : BaseTypedDTO<POCO.WorkQuestionnaire>
    {
       static WorkQuestionnaire()
       { Initialize(typeof(WorkQuestionnaire)); }

       public WorkQuestionnaire(POCO.WorkQuestionnaire innerObject) : base(innerObject) { }
       public WorkQuestionnaire() : base() { }

        public override void Save()
        {
            base.Save();

            InnerObject.ExpertGroupKey = this.ExpertGroup.Key;
            InnerObject.QuestionnaireKey = this.Questionnaire.Key;
        }

        public override void Reset()
        {
            base.Reset();

            if (InnerObject.Questionnaire != null)
            { Questionnaire = new Questionnaire(InnerObject.Questionnaire); }
            if (InnerObject.ExpertGroup != null)
            { ExpertGroup = new ExpertGroup(InnerObject.ExpertGroup); }
        }

        /// <summary>
        /// Опрос
        /// </summary>
        public virtual Questionnaire Questionnaire { get; set; }

        public virtual ExpertGroup ExpertGroup { get; set; }

        public virtual ICollection<ExpertAnswer> ExpertAnswers { get; set; }
    }
}