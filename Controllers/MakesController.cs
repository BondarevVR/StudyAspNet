using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Controllers.Resourses;
using App.Models;
using App.Persistance;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Controllers
{
    public class MakesController : Controller
    {
        public MakesController(AppDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        private readonly AppDbContext context;
        private readonly IMapper mapper;

        [HttpGet("/api/makes")]
        public async Task<IEnumerable<MakeResourse>> GetMakes() 
        {
            var makes =  await context.Makes.Include(m => m.Models).ToListAsync();
            return mapper.Map<List<Make>, List<MakeResourse>>(makes);
        }
    }
}
