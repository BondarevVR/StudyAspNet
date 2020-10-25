using App.Controllers.Resourses;
using App.Core.Models;
using App.Persistance;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace App.Controllers
{
    public class PhotoController : Controller
    {
        private readonly IWebHostEnvironment host;
        private readonly IVehicleRepository repository;
        private readonly IUnitOfwork unitOfwork;
        private readonly IMapper mapper;
        private readonly PhotoSettings photoSettings;
        public PhotoController(IWebHostEnvironment host, 
            IVehicleRepository repository, IUnitOfwork unitOfwork, IMapper mapper, 
            IOptionsSnapshot<PhotoSettings> options)
        {
            this.photoSettings = options.Value;
            this.host = host;
            this.repository = repository;
            this.unitOfwork = unitOfwork;
            this.mapper = mapper;
        }

        [HttpPost("/api/vehicles/{vehicleId}/photos")]
        public async Task<IActionResult> Upload(int vehicleId, IFormFile file) {
            var vehicle = await repository.GetVehicle(vehicleId, includeRelated: false);
            if (vehicle == null)
                return NotFound();
            if (file == null) return BadRequest("Null file");
            if (file.Length == 0) return BadRequest("Empty file");
            if (file.Length > photoSettings.MaxLength) return BadRequest("Max file length exceeded");
            if (!photoSettings.IsSupported(file.FileName)) return BadRequest("Invalid file type");

            var uploadsFolderPath = Path.Combine(host.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolderPath))
                Directory.CreateDirectory(uploadsFolderPath);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolderPath, fileName);

            using (var strem = new FileStream(filePath, FileMode.Create)) 
            {
                await file.CopyToAsync(strem);
            }

            var photo = new Photo { FileName = fileName };
            vehicle.Photos.Add(photo);
            await unitOfwork.CompleteAsync();

            var photoResourse = mapper.Map<Photo, PhotoResourse>(photo);
            return Ok(photoResourse);
        }
    }
}
