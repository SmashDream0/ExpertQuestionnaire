using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ExpertQuestionnaire.Specification
{
    public abstract class BaseSpecification<TPOCO>
        where TPOCO : POCO.BasePOCO
    {
        public BaseSpecification(Expression<Func<TPOCO, bool>> expression)
        {
            Predicate = expression;
        }

        public Expression<Func<TPOCO, bool>> Predicate
        { get; private set; }

        public static BaseSpecification<TPOCO> operator &(BaseSpecification<TPOCO> specification1, BaseSpecification<TPOCO> specification2)
        {
            var result = specification1.Predicate.And(specification2.Predicate);

            return new InnerSpecification<TPOCO>(result);
        }

        public static BaseSpecification<TPOCO> operator |(BaseSpecification<TPOCO> specification1, BaseSpecification<TPOCO> specification2)
        {
            var result = specification1.Predicate.Or(specification2.Predicate);

            return new InnerSpecification<TPOCO>(result);
        }
    }
}