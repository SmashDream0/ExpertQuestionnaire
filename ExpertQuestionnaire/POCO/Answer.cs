using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertQuestionnaire.POCO
{
    [Table("Answer")]
    public partial class Answer : BasePOCO
    {
        /// <summary>
        /// Ключ вопроса
        /// </summary>
        [ForeignKey("Question")]
        public int QuestionKey
        { get; set; }

        /// <summary>
        /// Текст ответа
        /// </summary>
        [Column(Order = 0)]
        public string Text { get; set; }

        /// <summary>
        /// Вопрос
        /// </summary>
        public virtual Question Question
        { get; set; }
    }
}