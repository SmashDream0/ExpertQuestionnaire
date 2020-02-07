using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertQuestionnaire.GUI.Entity
{
    public interface IDTO
    {
        int Key { get; }
        void Save();
        void Reset();
    }
}