using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertQuestionnaire.POCO
{
    /// <summary>
    /// Сессия опроса
    /// </summary>
    [Table("WorkQuestionnaire")]
    public partial class WorkQuestionnaire : BasePOCO
    {
        /// <summary>
        /// Ключ опросника
        /// </summary>
        [ForeignKey("Questionnaire")]
        public int QuestionnaireKey { get; set; }

        /// <summary>
        /// Ключ опросника
        /// </summary>
        [ForeignKey("ExpertGroup")]
        public int ExpertGroupKey { get; set; }

        /// <summary>
        /// Опрос
        /// </summary>
        public virtual Questionnaire Questionnaire { get; set; }

        /// <summary>
        /// Группа экспертов
        /// </summary>
        public virtual ExpertGroup ExpertGroup { get; set; }

        /// <summary>
        /// Ответы на вопросы
        /// </summary>
        public virtual ICollection<ExpertAnswer> ExpertAnswers { get; set; }
    }
}
