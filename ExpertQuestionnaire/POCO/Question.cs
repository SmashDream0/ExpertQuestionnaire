using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertQuestionnaire.POCO
{
    [Table("Question")]
    public partial class Question : BasePOCO
    {
        /// <summary>
        /// Ключ опроса
        /// </summary>
        [ForeignKey("Questionnaire")]
        public int QuestionnaireKey
        { get; set; }

        /// <summary>
        /// Текст вопроса
        /// </summary>
        public string Text
        { get; set; }

        /// <summary>
        /// Ответы
        /// </summary>
        public virtual ICollection<Answer> Answers
        { get; private set; }

        /// <summary>
        /// Опрос
        /// </summary>
        public virtual Questionnaire Questionnaire
        { get; set; }
    }
}
