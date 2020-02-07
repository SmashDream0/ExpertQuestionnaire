using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertQuestionnaire.GUI.Entity
{
    public partial class ExpertGroupUser : BaseTypedDTO<POCO.ExpertGroupUser>
    {
        static ExpertGroupUser()
        { Initialize(typeof(ExpertGroupUser)); }

        public ExpertGroupUser(POCO.ExpertGroupUser innerObject) : base(innerObject)
        { }
        public ExpertGroupUser() : base()
        { }

        public override void Reset()
        {
            base.Reset();
        }

        public override void Save()
        {
            base.Save();

            InnerObject.ExpertGroupKey = ExpertGroup.Key;
            InnerObject.ExpertKey = Expert.Key;
        }

        public User Expert { get; set; }
        public ExpertGroup ExpertGroup { get; set; }
    }
}