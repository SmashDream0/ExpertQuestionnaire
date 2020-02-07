using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertQuestionnaire.Logic.Calculation
{
    public class Question : NamedKey
    {
        public Question(int key, string name, IList<NamedKey> answers) : base(key, name)
        { Answers = answers; }

        public IList<NamedKey> Answers
        { get; private set; }
    }
}