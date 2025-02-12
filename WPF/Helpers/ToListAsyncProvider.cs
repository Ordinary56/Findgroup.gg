using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Helpers
{
    public static ValueTask<IList<TSource>> ToListAsync<TSource>(this IAsyncEnumerable<TSource>? source)
    {
        //
    }
}
