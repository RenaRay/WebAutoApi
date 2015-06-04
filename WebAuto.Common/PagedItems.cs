using System.Collections.Generic;
using System.Linq;

namespace WebAuto.Common
{
    public class PagedItems<T>
    {
        public List<T> Items { get; set; }

        public bool HaveNext { get; set; }

        public bool HavePrevious { get; set; }

        public PagedItems()
            : this(new List<T>())
        {
        }

        public PagedItems(List<T> items)
        {
            Items = items;
        }
    }
}