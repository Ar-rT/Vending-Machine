using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vending_Machine.Interfaces
{
    interface IGoods : ICountable
    {
        String GoodsName { get; }
        Int32 Price { get; }
    }
}
