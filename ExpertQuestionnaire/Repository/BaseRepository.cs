using ExpertQuestionnaire.Specification;
using ExpertQuestionnaire.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertQuestionnaire.Repository
{
    public abstract class BaseRepository<TPOCO>
        where TPOCO : POCO.BasePOCO
    {
        public BaseRepository(IContext context)
        { Context = context; }

        /// <summary>
        /// Контекст
        /// </summary>
        public IContext Context
        { get; private set; }

        protected virtual IQueryable<TPOCO> Sort(IQueryable<TPOCO> queryable)
        { return queryable; }

        protected virtual IQueryable<TPOCO> InnerFind(BaseSpecification<TPOCO> specification)
        {
            var result = Context.AsQueryable<TPOCO>().Where(specification.Predicate);

            return Sort(result);
        }

        public TPOCO Attach(TPOCO poco)
        { return Context.Attach(poco); }

        public TPOCO FirstByKey(int key)
        {
            var q = Context.AsQueryable<TPOCO>();

            var result = q.FirstOrDefault(x => x.Key == key);

            return result;
        }

        /// <summary>
        /// Найти все записи таблицы
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TPOCO> All()
        {
            var result = Sort(Context.AsQueryable<TPOCO>());

            return result.ToList();
        }

        /// <summary>
        /// Найти элементы используя спецификацию
        /// </summary>
        /// <param name="specification"></param>
        /// <returns></returns>
        public IEnumerable<TPOCO> Find(BaseSpecification<TPOCO> specification)
        {
            var result = InnerFind(specification);

            return result.ToArray();
        }

        /// <summary>
        /// Показать кол-во элементов используя спецификацию
        /// </summary>
        /// <param name="specification"></param>
        /// <returns></returns>
        public int Count(BaseSpecification<TPOCO> specification)
        {
            var result = Context.AsQueryable<TPOCO>().Count(specification.Predicate);

            return result;
        }

        /// <summary>
        /// Добавить элемент
        /// </summary>
        /// <param name="pocos"></param>
        public void Insert(params TPOCO[] pocos)
        { Context.Insert((IEnumerable<TPOCO>)pocos); }

        /// <summary>
        /// Удалить элемент
        /// </summary>
        /// <param name="pocos"></param>
        public void Delete(params TPOCO[] pocos)
        { Context.Delete((IEnumerable<TPOCO>)pocos); }

        /// <summary>
        /// Сохранить
        /// </summary>
        public void Save()
        { Context.Save(); }
    }
}
