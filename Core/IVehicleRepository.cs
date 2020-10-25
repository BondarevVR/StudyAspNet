using App.Core.Models;
using App.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Persistance
{
    public interface IVehicleRepository
    {
        Task<Vehicle> GetVehicle(int id, bool includeRelated = true);
        Task<QueryResoult<Vehicle>> GetVehicles(VehicleQuery filter);
        void Add(Vehicle vehicle);
        void Remove(Vehicle vehicle);
    }
}