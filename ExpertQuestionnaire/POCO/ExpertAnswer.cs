using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertQuestionnaire.POCO
{
    /// <summary>
    /// Ответ эксперта
    /// </summary>
    [Table("ExpertAnswer")]
    public partial class ExpertAnswer : BasePOCO
    {
        /// <summary>
        /// Ключ сессии опроса
        /// </summary>
        [ForeignKey("WorkQuestionnaire")]
        public int WorkQuestionnaireKey
        { get; set; }

        /// <summary>
        /// Ключ сессии опроса
        /// </summary>
        [ForeignKey("Expert")]
        public int ExpertKey
        { get; set; }

        /// <summary>
        /// Ключ ответа на вопрос
        /// </summary>
        [ForeignKey("Answer")]
        public int AnswerKey
        { get; set; }

        /// <summary>
        /// Сессия опроса
        /// </summary>
        public virtual WorkQuestionnaire WorkQuestionnaire
        { get; set; }

        /// <summary>
        /// Эксперт
        /// </summary>
        public virtual User Expert
        { get; set; }

        /// <summary>
        /// Выбранный ответ
        /// </summary>
        public virtual Answer Answer
        { get; set; }

        /// <summary>
        /// Это выбранный ответ
        /// </summary>
        public bool IsAnswer
        { get; set; }
    }
}
