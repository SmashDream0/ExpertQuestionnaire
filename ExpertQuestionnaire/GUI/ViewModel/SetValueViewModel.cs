using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ExpertQuestionnaire.GUI.ViewModel
{
    public class SetValueViewModel
    {
        public SetValueViewModel()
        { Initialize(); }

        private void Initialize()
        { ContinueCommand = new Misc.Command(ContinueAction); }

        private void ContinueAction()
        {
            IsContinued = true;

            GUI.ViewManager.CloseView(this);
        }

        public bool IsContinued
        { get; private set; }

        public string Value
        { get; set; }

        public ICommand ContinueCommand
        { get; private set; }
    }
}