using App.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.Controllers.Resourses
{
    public class SaveVehicleResource
    {
        public int ID { get; set; }
        public int ModelID { get; set; }
        public bool IsRegistered { get; set; }
        [Required]
        public ContactResource Contact { get; set; }
        public ICollection<int> Features { get; set; }
        public SaveVehicleResource()
        {
            Features = new Collection<int>();
        }
        
    }
}
