using ExpertQuestionnaire.Specification;
using SQLite.CodeFirst;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ExpertQuestionnaire.Context
{
    public class Context : DbContext, IContext
    {
        public Context() : base("connection")
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en");

            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<Context>());            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var types = GetEntityTypes();

            foreach (var type in types)
            { modelBuilder.RegisterEntityType(type); }
            
            Database.SetInitializer<Context>(
              new SqliteCreateDatabaseIfNotExists<Context>(modelBuilder)); 
        }

        public static IEnumerable<Type> GetEntityTypes()
        {
            var tables = new List<Type>();

            foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
            {
                var attribute = type.GetCustomAttributes(false).OfType<TableAttribute>().FirstOrDefault();

                if (attribute != null)
                { tables.Add(type); }
            }

            return tables.ToArray();
        }

        public IQueryable<TPOCO> AsQueryable<TPOCO>()
        where TPOCO : POCO.BasePOCO
        { return Set<TPOCO>().AsQueryable(); }

        public void Insert<TPOCO>(TPOCO entity) where TPOCO : POCO.BasePOCO
        {
            var set = Set<TPOCO>();

            set.Add(entity);
        }

        public void Insert<TPOCO>(IEnumerable<TPOCO> entities) where TPOCO : POCO.BasePOCO
        {
            var set = Set<TPOCO>();

            set.AddRange(entities);
        }

        public void Delete<TPOCO>(TPOCO entity) where TPOCO : POCO.BasePOCO
        {
            var set = Set<TPOCO>();

            if (Entry(entity) != null)
            { set.Remove(entity); }
        }

        public TPOCO Attach<TPOCO>(TPOCO entity) where TPOCO : POCO.BasePOCO
        {
            var set = Set<TPOCO>();

            DbEntityEntry<TPOCO> dbEntityEntry = Entry(entity);

            if (dbEntityEntry == null)
            {
                entity = set.Attach(entity);
                dbEntityEntry = Entry(entity);
                dbEntityEntry.State = EntityState.Modified;
            }

            return entity;
        }

        public void Delete<TPOCO>(IEnumerable<TPOCO> entities) where TPOCO : POCO.BasePOCO
        {
            var set = Set<TPOCO>();

            set.RemoveRange(entities);
        }

        public void Save()
        { SaveChanges(); }
    }
}