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
    public class SelectItemViewModel<T> : ItemsViewModel<T>
        where T : IDTO
    {
        public SelectItemViewModel(IEnumerable<T> items)
        {
            _items = new ObservableCollection<T>();

            foreach (var item in items)
            { _items.Add(item); }

            Initialize();
        }

        private ObservableCollection<T> _items;

        public override ObservableCollection<T> Items => _items;

        public bool IsSelected
        { get; private set; }

        public ICommand SelectItemCommand
        { get; private set; }

        protected override void Initialize()
        {
            base.Initialize();
            SelectItemCommand = new Misc.Command(SelectAction);
        }

        protected void SelectAction()
        {
            IsSelected = ItemCurrent != null;

            ViewManager.CloseView(this);
        }
    }
}