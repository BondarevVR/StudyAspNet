using App.Core.Models;
using App.Extentions;
using App.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace App.Persistance
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly AppDbContext context;

        public VehicleRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<Vehicle> GetVehicle(int id, bool includeRelated = true)
        {
            if (!includeRelated)
                return await context.Vehicles.FindAsync(id);

            return await context.Vehicles
                .Include(f => f.VehicleFeatures).ThenInclude(vf => vf.Feature)
                .Include(m => m.Model).ThenInclude(m => m.Make)
                .SingleOrDefaultAsync(v => v.ID == id);
        }

        public async Task<QueryResoult<Vehicle>> GetVehicles(VehicleQuery queryObj) {
            var resoult = new QueryResoult<Vehicle>();
            
            var query =  context.Vehicles
                .Include(v => v.VehicleFeatures).ThenInclude(vf => vf.Feature)
                .Include(v => v.Model).ThenInclude(m => m.Make)
                .AsQueryable();

            if (queryObj.MakeId.HasValue)
                query = query.Where(v => v.Model.MakeID == queryObj.MakeId.Value);

            var columnsMap = new Dictionary<string, Expression<Func<Vehicle, object>>>()
            {
                ["make"] = v => v.Model.Make.Name
            };

            query = query.ApplyOrdering(queryObj, columnsMap);

            resoult.TotalItems = await query.CountAsync();
            query = query.ApplyPaging(queryObj);

            resoult.Items = await query.ToListAsync();

            return resoult;
        }
        
        public void Add(Vehicle vehicle)
        {
            context.Vehicles.Add(vehicle);
        }

        public void Remove(Vehicle vehicle)
        {
            context.Vehicles.Remove(vehicle);
        }
    }
}
