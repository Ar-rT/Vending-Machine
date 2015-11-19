using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Vending_Machine.Enums;
using Vending_Machine.ViewModels;

namespace Vending_Machine
{
    public class MaсhineViewModel : BaseViewModel
    {
        private Dictionary<CoinTypes, CoinsViewModel> _userBank;
        private Dictionary<CoinTypes, CoinsViewModel> _VMvallet;
        private Dictionary<GoodsTypes, GoodsViewModel> _goodsList;
        private UserViewModel _user;

        public ICommand ExitCommand { get; private set; }
        public ICommand ReturnMoneyCommand { get; private set; }

        public Dictionary<CoinTypes, CoinsViewModel>.ValueCollection VMvallet
        {
            get 
            {
                return _VMvallet.Values;
            }
        }

        public Dictionary<GoodsTypes, GoodsViewModel>.ValueCollection GoodsList
        {
            get 
            {
                return _goodsList.Values;
            }
        }

        public Int32 UserBank
        {
            get
            {
                int sum = 0;
                foreach (var c in _userBank.Values)
                    sum += c.Value * c.Count;
                return sum;
            }
        }

        public String UserBankString
        {
            get
            {
                return UserBank.ToString();
            }
        }

        public UserViewModel User
        {
            get
            {
                return _user;
            }

            private set
            {
                if (_user == null)
                {
                    _user = value;
                    OnPropertyChanged("User");
                }
            }
        }


        public MaсhineViewModel(Dictionary<CoinTypes, CoinsViewModel> coins, Dictionary<GoodsTypes, GoodsViewModel> goods)
        {
            _VMvallet = new Dictionary<CoinTypes,CoinsViewModel>(coins);
            _userBank = new Dictionary<CoinTypes, CoinsViewModel>();
            _goodsList = new Dictionary<GoodsTypes, GoodsViewModel>(goods);
            ExitCommand = new Command(() => Application.Current.Shutdown());
            ReturnMoneyCommand = new Command(ReturnMoneyToUser);
            foreach (var g in _goodsList.Values)
                g.Choosed += TryBuyGoods;
        }

        private bool BankToVallet(Int32 sum)
        {
            if (sum > UserBank)
                return false;
            var payCoinType = (CoinTypes)_userBank.Keys.Max();
            int payCount = 0;
            while (sum != 0)
            {
                if (_userBank.Keys.Contains(payCoinType))
                {
                    payCount = sum / _userBank[payCoinType].Value;
                    payCount = _userBank[payCoinType].Count >= payCount ? payCount : _userBank[payCoinType].Count;
                    _userBank[payCoinType].TakeItems(payCount);
                    _VMvallet[payCoinType].AddItems(payCount);
                    sum -= payCount * _userBank[payCoinType].Value;
                }
                payCoinType--;
            }
            OnPropertyChanged("UserBankString");
            return true;
        }

        private void TryBuyGoods(GoodsViewModel target)
        {
            if (!BankToVallet(target.Price))
            {
                MessageBox.Show("Недостаточно средств");
                return;
            }
            _goodsList[target.Type].TakeItems(1);
            MessageBox.Show("Спасибо!");
        }

        public void InsertUserCoin(CoinTypes coinType)
        {
            if (_userBank.Keys.Contains(coinType))
                _userBank[coinType].AddItems(1);
            else
                _userBank.Add(coinType, new CoinsViewModel(coinType, 1));
            OnPropertyChanged("UserBankString");
        }

        private int SummaryCoinsNumber(CoinTypes type)
        {
            var valletCount = 0;
            var bankCount = 0;
            if (_VMvallet.Keys.Contains(type))
                valletCount = _VMvallet[type].Count;
            if (_userBank.Keys.Contains(type))
                bankCount = _userBank[type].Count;
            return valletCount + bankCount;
        }

        private bool TryCoinsReturn(CoinTypes type, Int32 count)
        {
            if (SummaryCoinsNumber(type) < count)
                return false;
            if (_userBank.Keys.Contains(type))
            {
                if (_userBank[type].Count >= count)
                {
                    _VMvallet[type].AddItems(_userBank[type].Count - count);
                    _userBank[type].TakeItems(_userBank[type].Count);
                    OnPropertyChanged("UserBankString");
                    return true;
                }
                else
                {
                    count -= _userBank[type].Count;
                    _userBank[type].TakeItems(_userBank[type].Count);
                    OnPropertyChanged("UserBankString");
                }
            }
            _VMvallet[type].TakeItems(count);
            BankToVallet(count * _VMvallet[type].Value);
            return true;
        }

        private void ReturnMoneyToUser()
        {
            if (User == null) return;
            if (UserBank == 0) return;
            var retCoinType = (CoinTypes)_VMvallet.Keys.Max();
            int retCount = 0;
            while(UserBank != 0)
            {
                retCount = UserBank / _VMvallet[retCoinType].Value;
                retCount = SummaryCoinsNumber(retCoinType) >= retCount ? retCount : SummaryCoinsNumber(retCoinType);
                if (TryCoinsReturn(retCoinType, retCount))
                    User.GiveCoins(retCoinType, retCount);
                retCoinType--;
            }
        }

        public void ConnectUser(UserViewModel user)
        {
            if (User == null)
                User = user;
        }

        public void DisconnectUser()
        {
            if (User == null) return;
        }
    }
}
