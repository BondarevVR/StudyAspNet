using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Extentions
{
    public interface IQueryObject
    {
        public string SortBy { get; set; }

        public bool IsSortAssending { get; set; }

        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
