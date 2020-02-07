using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExpertQuestionnaire.GUI.Entity
{
    public abstract class BaseTypedDTO<TInnerObject> : GUI.ViewModel.Misc.BaseNotifier, IDTO
        where TInnerObject : POCO.BasePOCO
    {
        public BaseTypedDTO(TInnerObject innerObject, bool allowInnerTypeCreation)
        {
            AllowInnerTypeCreation = allowInnerTypeCreation;
            InnerObject = innerObject ?? CreateInstance();
            Reset();
        }
        public BaseTypedDTO(TInnerObject innerObject) : this(innerObject, true)
        { }
        public BaseTypedDTO() : this(null)
        { }

        private static TInnerObject CreateInstance()
        {
            var instance = Activator.CreateInstance<TInnerObject>();
            instance.Key = ++_increment;

            return instance;
        }

        private static int _increment = int.MinValue;

        protected static void Initialize(Type inheritateType)
        {
            var pocoProps = typeof(TInnerObject).GetProperties();

            var entityProps = inheritateType.GetProperties();

            _entityPocoProperties = (from entityProp in entityProps
                                     join pocoProp in pocoProps on new { entityProp.PropertyType, entityProp.Name } equals new { pocoProp.PropertyType, pocoProp.Name }
                                     where entityProp.GetMethod != null && entityProp.SetMethod != null && pocoProp.GetMethod != null && pocoProp.SetMethod != null
                                     select new KeyValuePair<PropertyInfo, PropertyInfo>(entityProp, pocoProp)).ToArray();
        }

        protected bool AllowInnerTypeCreation
        { get; private set; }

        public int Key => InnerObject.Key;

        private static IEnumerable<KeyValuePair<PropertyInfo, PropertyInfo>> _entityPocoProperties;

        public TInnerObject InnerObject
        { get; set; }

        /// <summary>
        /// Занести значения в POCO
        /// </summary>
        public virtual void Save()
        {
            foreach (var propPair in _entityPocoProperties)
            { propPair.Value.SetValue(InnerObject, propPair.Key.GetValue(this)); }
        }

        /// <summary>
        /// Получить значения из POCO
        /// </summary>
        public virtual void Reset()
        {
            foreach (var propPair in _entityPocoProperties)
            { propPair.Key.SetValue(this, propPair.Value.GetValue(InnerObject)); }
        }

        public override bool Equals(object obj)
        {
            var typedObj = obj as BaseTypedDTO<TInnerObject>;

            bool result = typedObj != null;

            if (result)
            {
                if (InnerObject.Key < 0 && InnerObject.Key < 0)
                {
                    foreach (var propPair in _entityPocoProperties)
                    {
                        var value1 = propPair.Key.GetValue(this);
                        var value2 = propPair.Key.GetValue(typedObj);

                        result = object.Equals(value1, value2);
                        if (!result)
                        { break; }
                    }
                }
                else
                { result = InnerObject.Key == InnerObject.Key; }
            }

            return result;
        }

        public override int GetHashCode()
        {
            var res = this.Key;

            //foreach (var propPair in _entityPocoProperties)
            //{
            //    var value = propPair.Key.GetValue(this);

            //    if (value != null)
            //    { res ^= 31 ^ value.GetHashCode(); }
            //}

            return res;
        }
    }
}