using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Vending_Machine
{
    public delegate void ItemActivated();

    public class Command:ICommand
    {
        private ItemActivated _callback;
        public Command(ItemActivated callback)
        {
            _callback = callback;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            if(_callback != null)
                _callback();
        }
    }
}
