using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertQuestionnaire.POCO
{
    public partial class User : BasePOCO
    {
        [Column(Order = 1)]
        public string Name { get; set; }
        public string Password { get; set; }
        [Column(Order = 0)]
        public bool IsAdmin { get; set; }
    }
}