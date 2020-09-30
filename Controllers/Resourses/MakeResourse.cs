using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace App.Controllers.Resourses
{
    public class MakeResourse : KeyValuePairResource
    {
        public ICollection<KeyValuePairResource> Models { get; set; }
        public MakeResourse()
        {
            Models = new Collection<KeyValuePairResource>();
        }
    }
}
