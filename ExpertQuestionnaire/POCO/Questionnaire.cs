using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertQuestionnaire.POCO
{
    /// <summary>
    /// Опросник
    /// </summary>
    [Table("Questionnaire")]
    public partial class Questionnaire : BasePOCO
    {
        /// <summary>
        /// Имя
        /// </summary>
        public string Name
        { get; set; }

        /// <summary>
        /// Цель
        /// </summary>
        public string Purpose
        { get; set; }

        /// <summary>
        /// Задача
        /// </summary>
        public string Task
        { get; set; }

        /// <summary>
        /// Вопросы
        /// </summary>
        public virtual ICollection<Question> Question
        { get; private set; }
    }
}