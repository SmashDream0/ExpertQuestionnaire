using ExpertQuestionnaire.GUI.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ExpertQuestionnaire.GUI.ViewModel
{
    public abstract class ItemsViewModel<T> : ItemEditViewModel
        where T: IDTO
    {
        public ItemsViewModel()
        { }

        protected override void Initialize()
        {
            base.Initialize();

            AddCommand = new Misc.Command(AddAction);
            EditCommand = new Misc.Command(EditAction, CanDoWithSelection);
            DeleteCommand = new Misc.Command(DeleteAction, CanDoWithSelection);
            SaveCommand = new Misc.Command(SaveAction, CanSaveAction);
            RefreshCommand = new Misc.Command(RefreshAction, CanRefreshAction);

            Items.CollectionChanged += Items_CollectionChanged;
        }

        private void Items_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    IsSaved = false;
                    break;
            }
        }

        protected virtual void AddAction() { }
        protected virtual void EditAction() { }
        protected virtual void DeleteAction()
        {
            if (ItemCurrent != null)
            {
                for (int index = 0; index < Items.Count; index++)
                {
                    if (Items[index].Key == ItemCurrent.Key)
                    {
                        Items.RemoveAt(index);
                        break;
                    }
                }
            }
        }

        private bool CanDoWithSelection()
        { return ItemCurrent != null; }

        private void SaveAction()
        {
            InnerSaveAction();
            IsSaved = true;

            foreach (var item in Items)
            { item.Reset(); }
        }

        protected virtual bool CanSaveAction()
        { return !IsSaved; }

        protected void RefreshAction()
        {
            InnerRefreshAction();
            IsSaved = true;
        }

        protected virtual bool CanRefreshAction()
        { return !IsSaved; }

        protected virtual void InnerSaveAction() { }
        protected virtual void InnerRefreshAction() { }

        private bool _isSaved = true;

        private T _itemCurrent;

        public bool IsSaved
        {
            get => _isSaved;
            protected set
            {
                _isSaved = value;
                PropertyChangedAction("IsSaved");
                SaveCommand.UpdateCanExecute();
                RefreshCommand.UpdateCanExecute();
            }
        }

        public abstract ObservableCollection<T> Items
        { get; }

        public T ItemCurrent
        {
            get => _itemCurrent;
            set
            {
                _itemCurrent = value;
                PropertyChangedAction("ItemCurrent");
                EditCommand.UpdateCanExecute();
                DeleteCommand.UpdateCanExecute();
            }
        }

        public ICommand AddCommand
        { get; private set; }
        public Misc.IExecuteCommand EditCommand
        { get; private set; }
        public Misc.IExecuteCommand DeleteCommand
        { get; private set; }

        public Misc.IExecuteCommand SaveCommand
        { get; private set; }
        public Misc.IExecuteCommand RefreshCommand
        { get; private set; }
    }
}