using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vending_Machine.Interfaces
{
    interface ICountable
    {
        Int32 Count { get; }
        bool AddItems(Int32 count);
        bool TakeItems(Int32 count);
    }
}
