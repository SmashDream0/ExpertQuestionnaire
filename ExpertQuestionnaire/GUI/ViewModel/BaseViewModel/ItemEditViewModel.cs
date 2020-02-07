using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ExpertQuestionnaire.GUI.ViewModel
{
    public abstract class ItemEditViewModel : Misc.BaseNotifier
    {
        public ItemEditViewModel()
        { }

        protected virtual void Initialize()
        {
            OkCommand = new Misc.Command(OkAction);
            CloseCommand = new Misc.Command(CloseAction);
        }

        /// <summary>
        /// Кнопка ОК нажата
        /// </summary>
        public bool IsOkClicked
        { get; private set; }

        protected virtual void OkAction()
        {
            IsOkClicked = true;
            GUI.ViewManager.CloseView(this);
        }


        protected virtual void CloseAction()
        {
            GUI.ViewManager.CloseView(this);
        }

        public ICommand OkCommand
        { get; private set; }

        public ICommand CloseCommand
        { get; private set; }
    }
}