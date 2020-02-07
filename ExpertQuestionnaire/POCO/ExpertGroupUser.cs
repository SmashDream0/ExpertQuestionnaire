using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertQuestionnaire.POCO
{
    [Table("ExpertGroupUser")]
    public partial class ExpertGroupUser : BasePOCO
    {
        [ForeignKey("ExpertGroup")]
        public int ExpertGroupKey { get; set; }
        [ForeignKey("Expert")]
        public int ExpertKey { get; set; }

        public virtual User Expert { get; set; }

        public virtual ExpertGroup ExpertGroup { get; set; }
    }
}