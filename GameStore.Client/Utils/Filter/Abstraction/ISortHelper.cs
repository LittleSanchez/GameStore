using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStore.Client.Utils.Filter.Abstraction
{
    public interface ISortHelper<T>
    {
        string DESC { get; }
        string ASC { get; }

        Comparison<T> GetComparison(string keyWord);
    }
}