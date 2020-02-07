using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ExpertQuestionnaire.Specification
{
    internal class InnerSpecification<T> : BaseSpecification<T>
        where T : POCO.BasePOCO
    {
        public InnerSpecification(Expression<Func<T, bool>> expression) : base(expression)
        { }
    }
}