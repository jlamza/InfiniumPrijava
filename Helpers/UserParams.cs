using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt.Helpers
{
    public class UserParams
    {
        private const int MaxpageSize = 50;
        public int pageNumber { get; set; } = 1;
        private int _pageSize = 3;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxpageSize) ? MaxpageSize : value;
        }
    }
}
