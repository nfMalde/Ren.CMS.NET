using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ren.CMS.CORE.Helper
{
    public static class ArrayHelper
    {
        public static int IndexOfElement<T>(this IEnumerable<T> source, T value)
        {
            int index = 0;
            var comparer = EqualityComparer<T>.Default; // or pass in as a parameter
            foreach (T item in source)
            {
                if (comparer.Equals(item, value)) return index;
                index++;
            }
            return -1;
        }
    }
}
