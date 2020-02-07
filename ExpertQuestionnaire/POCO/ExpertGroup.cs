using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertQuestionnaire.POCO
{
    [Table("ExpertGroup")]
    public partial class ExpertGroup : BasePOCO
    {
        public string Name { get; set; }

        public virtual ICollection<ExpertGroupUser> Experts { get; set; }
    }
}