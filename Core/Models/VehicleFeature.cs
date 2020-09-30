using App.Features;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace App.Models
{
    [Table("VehicleFeature")]
    public class VehicleFeature
    {
        public int VehicleID { get; set; }
        public int FeatureID { get; set; }
        public Vehicle Vehicle { get; set; }
        public Feature Feature { get; set; }
    }
}
