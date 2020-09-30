using App.Controllers.Resourses;
using App.Models;
using App.Persistance;
using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Controllers
{
    //[Microsoft.AspNetCore.Components.Route("/api/vehicles")]
    public class VehiclesController : Controller
    {
        private readonly IMapper mapper;
        private readonly IVehicleRepository repository;
        private readonly IUnitOfwork unitOfwork;

        public VehiclesController(IMapper mapper, IVehicleRepository repository, IUnitOfwork unitOfwork)
        {
            this.mapper = mapper;
            this.repository = repository;
            this.unitOfwork = unitOfwork;
        }

        [HttpPost("/api/vehicles")]
        public async Task<IActionResult> CreateVehicle([FromBody] SaveVehicleResource vehicleResourve)
        {
            //chek request fields
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //save vehicle to the DB
            var vehicle = mapper.Map<SaveVehicleResource, Vehicle>(vehicleResourve);
            vehicle.LastUpdate = DateTime.Now;
            repository.Add(vehicle);
            await unitOfwork.CompleteAsync();

            //return saved vehicle
            vehicle = await repository.GetVehicle(vehicle.ID);
            var resoult = mapper.Map<Vehicle, VehicleResource>(vehicle);
            return Ok(resoult);
        }

        [HttpPut("/api/vehicles/{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody] SaveVehicleResource vehicleResourve)
        {
            //chek request fields
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //save changes to the DB
            var vehicle = await repository.GetVehicle(id);

            if (vehicle == null)
                return NotFound();

            mapper.Map<SaveVehicleResource, Vehicle>(vehicleResourve, vehicle);
            vehicle.LastUpdate = DateTime.Now;
            await unitOfwork.CompleteAsync();

            //return saved vehicle
            var resoult = mapper.Map<Vehicle, VehicleResource>(vehicle);
            return Ok(resoult);
        }

        [HttpDelete("/api/vehicles/{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var vehicle = await repository.GetVehicle(id, includeRelated: false);

            if (vehicle == null)
                return NotFound();

            repository.Remove(vehicle);
            await unitOfwork.CompleteAsync();

            return Ok(id);
        }

        [HttpGet("/api/vehicles/{id}")]
        public async Task<IActionResult> GetVehicle(int id)
        {
            var vehicle = await repository.GetVehicle(id);

            if (vehicle == null)
                return NotFound();

            var vehicleResource = mapper.Map<Vehicle, VehicleResource>(vehicle);
            return Ok(vehicleResource);
        }
    }
}
