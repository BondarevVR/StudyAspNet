using App.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Core.Models
{
    public class VehicleQuery: IQueryObject
    {
        public int? MakeId { get; set; }

        public string SortBy { get; set; }
        public bool IsSortAssending{ get; set; }

        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
