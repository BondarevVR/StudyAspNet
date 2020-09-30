using App.Controllers.Resourses;
using App.Features;
using App.Persistance;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Controllers
{
    public class FeaturesController : Controller
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;

        public FeaturesController(AppDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("/api/features")]

        public async Task<IEnumerable<KeyValuePairResource>> GetFeatures() 
        {
            var features =  await context.Features.ToListAsync();
            return mapper.Map<List<Feature>, List<KeyValuePairResource>>(features);
        }
    }
}
