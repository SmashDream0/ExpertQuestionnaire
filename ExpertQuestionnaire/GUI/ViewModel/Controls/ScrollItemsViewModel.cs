using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ExpertQuestionnaire.GUI.ViewModel.Controls
{
    public class ScrollItemsViewModel : Misc.BaseNotifier
    {
        protected ScrollItemsViewModel()
        { Initialize(); }

        protected virtual void Initialize()
        {
            ItemNextCommand = new Misc.Command(NextAction);
            ItemPreviousCommand = new Misc.Command(PreviousAction);
            ItemFirstCommand = new Misc.Command(FirstAction);
            ItemLastCommand = new Misc.Command(LastAction);

            MinNumber = MaxNumber = 1;
        }

        private void NextAction()
        { CurrentNumber++; }

        private void PreviousAction()
        { CurrentNumber--; }

        private void FirstAction()
        { CurrentNumber = 1; }

        private void LastAction()
        { CurrentNumber = MaxNumber; }
        
        private int _itemNumber;
        private int _maxNumber;
        private int _minNumber;

        public event Action<int> NumberChanged;

        public int CurrentNumber
        {
            get => _itemNumber;
            set
            {
                if (MaxNumber >= value && value >= MinNumber)
                {
                    _itemNumber = value;
                    PropertyChangedAction("CurrentNumber");

                    NumberChanged?.Invoke(CurrentNumber);
                }
            }
        }

        public int MaxNumber
        {
            get => _maxNumber;
            set
            {
                _maxNumber = value;
                PropertyChangedAction("MaxNumber");

                if (MaxNumber < MinNumber)
                { MinNumber = MaxNumber; }

                if (MaxNumber < CurrentNumber)
                { CurrentNumber = MaxNumber; }
            }
        }

        public int MinNumber
        {
            get => _minNumber;
            set
            {
                _minNumber = value;
                PropertyChangedAction("MinNumber");

                if (MinNumber > MaxNumber)
                { MaxNumber = MinNumber; }

                if (MinNumber > CurrentNumber)
                { CurrentNumber = MinNumber; }
            }
        }

        public ICommand ItemNextCommand
        { get; private set; }
        public ICommand ItemPreviousCommand
        { get; private set; }

        public ICommand ItemFirstCommand
        { get; private set; }
        public ICommand ItemLastCommand
        { get; private set; }
    }
}
