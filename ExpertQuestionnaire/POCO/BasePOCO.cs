using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertQuestionnaire.POCO
{
    public abstract class BasePOCO
    {
        [Key]
        public int Key
        { get; set; }
    }
}