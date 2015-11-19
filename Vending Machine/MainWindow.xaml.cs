using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Vending_Machine.Enums;
using Vending_Machine.ViewModels;

namespace Vending_Machine
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MaсhineViewModel Machine { get; set; }

        public MainWindow()
        {
            //Типы монет,их названия и достоинство, товары,их названия и цена в идеале должны храниться в БД
            InitializeComponent();
            var coins = new Dictionary<CoinTypes, CoinsViewModel>();
            coins.Add(CoinTypes.One, new CoinsViewModel(CoinTypes.One, 100, "1 рубль"));
            coins.Add(CoinTypes.Two, new CoinsViewModel(CoinTypes.Two, 100, "2 рубля"));
            coins.Add(CoinTypes.Five, new CoinsViewModel(CoinTypes.Five, 100, "5 рублей"));
            coins.Add(CoinTypes.Ten, new CoinsViewModel(CoinTypes.Ten, 100, "10 рублей"));
            var goods = new Dictionary<GoodsTypes, GoodsViewModel>();
            goods.Add(GoodsTypes.Tea, new GoodsViewModel(GoodsTypes.Tea, 13, 10, "Чай"));
            goods.Add(GoodsTypes.Coffee, new GoodsViewModel(GoodsTypes.Coffee, 18, 20, "Кофе"));
            goods.Add(GoodsTypes.CoffeeMilk, new GoodsViewModel(GoodsTypes.CoffeeMilk, 21, 20, "Кофе с молоком"));
            goods.Add(GoodsTypes.Juice, new GoodsViewModel(GoodsTypes.Juice, 35, 15, "Сок"));
            Machine = new MaсhineViewModel(coins, goods);
            var userCoins = new Dictionary<CoinTypes, CoinsViewModel>();
            userCoins.Add(CoinTypes.One, new CoinsViewModel(CoinTypes.One, 10, "1 рубль"));
            userCoins.Add(CoinTypes.Two, new CoinsViewModel(CoinTypes.Two, 30, "2 рубля"));
            userCoins.Add(CoinTypes.Five, new CoinsViewModel(CoinTypes.Five, 20, "5 рублей"));
            userCoins.Add(CoinTypes.Ten, new CoinsViewModel(CoinTypes.Ten, 15, "10 рублей"));
            Machine.ConnectUser(new UserViewModel(userCoins,Machine));
            MainPanel.DataContext = Machine;
        }
    }
}
