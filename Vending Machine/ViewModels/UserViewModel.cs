using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vending_Machine.Enums;
using Vending_Machine.ViewModels;

namespace Vending_Machine
{
    public class UserViewModel : BaseViewModel
    {
        private Dictionary<CoinTypes, CoinsViewModel> _vallet;
        private MaсhineViewModel _machine;
        public Dictionary<CoinTypes, CoinsViewModel>.ValueCollection Vallet
        {
            get
            {
                return _vallet.Values;
            }
        }

        public UserViewModel(Dictionary<CoinTypes, CoinsViewModel> coins, MaсhineViewModel machine)
        {
            _vallet = new Dictionary<CoinTypes,CoinsViewModel>(coins);
            _machine = machine;
            foreach (var c in _vallet)
                c.Value.Choosed += InsertCoin;
        }


        /// <summary>
        /// Inserting coin into vending machine
        /// </summary>
        /// <param name="coin">Coin type</param>
        private void InsertCoin(CoinsViewModel coin)
        {
            if (coin.TakeItems(1))
                _machine.InsertUserCoin(coin.Type);
            
        }


        /// <summary>
        /// Adding coins to user's vallet
        /// </summary>
        /// <param name="type">Coins type</param>
        /// <param name="count">Number of coins</param>
        public void GiveCoins(CoinTypes type, int count)
        {
            _vallet[type].AddItems(count);
        }
    }
}
