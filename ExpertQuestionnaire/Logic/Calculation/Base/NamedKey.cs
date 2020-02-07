using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertQuestionnaire.Logic.Calculation
{
    public class NamedKey
    {
        public NamedKey(int key, string name)
        {
            Key = key;
            Name = name;
        }

        public int Key { get; private set; }
        public string Name { get; private set; }

        public override string ToString()
        {
            return $"{Key}-{Name}";
        }
    }
}
