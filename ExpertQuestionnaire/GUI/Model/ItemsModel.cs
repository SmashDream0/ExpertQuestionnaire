using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertQuestionnaire.GUI.Model
{
    public abstract class ItemsModel<TGUIEntity, TPOCO> : BaseModel<TGUIEntity, TPOCO>
        where TGUIEntity : Entity.BaseTypedDTO<TPOCO>
        where TPOCO : POCO.BasePOCO
    {
        protected ItemsModel(Repository.BaseRepository<TPOCO> itemsRepository) : base(itemsRepository)
        { Initialize(); }

        protected virtual void Initialize()
        {
            Items = new ObservableCollection<TGUIEntity>();
            Items.CollectionChanged += ItemsCollectionChanged;
        }

        public void Update()
        {
            Items.CollectionChanged -= ItemsCollectionChanged;

            Items.Clear();

            InnerUpdate();

            Items.CollectionChanged += ItemsCollectionChanged;
        }

        protected virtual void InnerUpdate()
        {
            var result = MainRepository.All();

            foreach (var item in result)
            {
                var newItem = CreateItem(item);

                Items.Add(newItem);
            }
        }

        protected virtual TGUIEntity CreateItem(params object[] parameters)
        { return Activator.CreateInstance(typeof(TGUIEntity), parameters) as TGUIEntity; }

        private void ItemsCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    foreach (var item in e.OldItems)
                    {
                        var typedItem = item as TGUIEntity;

                        _removeItems.Add(typedItem);
                    }
                    break;
            }
        }

        protected override void InnerSave()
        {
            foreach (var deleteItem in _removeItems)
            { MainRepository.Delete(deleteItem.InnerObject); }

            _removeItems.Clear();

            foreach (var item in Items)
            {
                item.Save();

                if (item.Key < 0)
                { MainRepository.Insert(item.InnerObject); }
                else
                { MainRepository.Attach(item.InnerObject); }
            }
        }

        private TGUIEntity _itemCurrent;

        private readonly List<TGUIEntity> _removeItems = new List<TGUIEntity>();
        public ObservableCollection<TGUIEntity> Items
        { get; private set; }

        public TGUIEntity ItemCurrent
        {
            get => _itemCurrent;
            set
            {
                if (Items.Contains(value))
                { _itemCurrent = value; }
                else if (!Items.Any())
                {
                    _itemCurrent = value;
                    Items.Add(ItemCurrent);
                }
            }
        }
    }
}