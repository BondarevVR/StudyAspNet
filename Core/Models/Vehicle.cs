using App.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace App.Models
{
    [Table("Vehicles")]
    public class Vehicle
    {
        public int ID { get; set; }
        [Required]
        public int ModelID { get; set; }
        public Model Model { get; set; }
        public bool IsRegistered { get; set; }
        public DateTime LastUpdate { get; set; }
        public ICollection<VehicleFeature> VehicleFeatures { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public Vehicle()
        {
            VehicleFeatures = new Collection<VehicleFeature>();
            Photos = new Collection<Photo>();
        }

        //contacts
        [Required]
        [StringLength(255)]
        public string ContactName { get; set; }
        [StringLength(255)]
        public string ContactEmail { get; set; }
        [Required]
        [StringLength(255)]
        public string ContactPhone { get; set; }
    }
}
