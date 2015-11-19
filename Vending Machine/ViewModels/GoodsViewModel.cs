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

    public class GoodsViewModel : BaseViewModel, IGoods
    {
        private readonly String _name;
        private readonly Int32 _price;
        private Int32 _count;

        public readonly GoodsTypes Type;

        public delegate void GoodsActivated(GoodsViewModel goods);

        public event GoodsActivated Choosed;

        public ICommand ChooseCommand { get; private set; }

        public String GoodsName
        {
            get
            {
                return _name ?? Type.ToString();
            }
        }
        public Int32 Price
        {
            get
            {
                return _price;
            }
        }

        public String PriceString
        {
            get
            {
                return Price.ToString();
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

        public GoodsViewModel(GoodsTypes type, Int32 price, Int32 count, String name = null)
        {
            Type = type;
            _name = name;
            _price = price;
            _count = count;
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
