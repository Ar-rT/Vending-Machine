using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Vending_Machine.Enums;
using Vending_Machine.Interfaces;

namespace Vending_Machine.ViewModels
{
    public class CoinsViewModel : BaseViewModel, ICoins
    {
        private readonly String _name;
        private Int32 _count;

        public readonly CoinTypes Type;

        public delegate void CoinActivated(CoinsViewModel coin);

        public event CoinActivated Choosed;

        public ICommand ChooseCommand { get; private set; }

        public String CoinName
        {
            get
            {
                return _name ?? Type.ToString();
            }
        }
        public Int32 Value
        {
            get
            {
                switch(Type)
                {
                    case CoinTypes.One:
                        return 1;
                    case CoinTypes.Two:
                        return 2;
                    case CoinTypes.Five:
                        return 5;
                    case CoinTypes.Ten:
                        return 10;
                    default:
                        return 0;
                }
            }
        }

        public Int32 Count {
            get 
            { 
                return _count; 
            }
            private set
            {
                if (value < 0) return;
                _count = value;
                OnPropertyChanged("CountString");
            }
        }

        public String CountString
        {
            get
            {
                return Count.ToString();
            }
        }

        public CoinsViewModel(CoinTypes type, Int32 count, String name = null)
        {
            Type = type;
            _count = count;
            _name = name;
            ChooseCommand = new Command(WhenChoosed);
        }

        private void WhenChoosed()
        {
            if (Choosed != null)
                Choosed(this);
        }

        public bool AddItems(Int32 count)
        {
            Count += count;
            return true;
        }

        public bool TakeItems(Int32 count)
        {
            if (Count < count)
                return false;
            else
            {
                Count -= count;
                return true;
            }
        }
    }
}
