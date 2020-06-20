using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BlazorMovies.Server.Helpers;
using BlazorMovies.Shared.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorMovies.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileStorageService _fileStorageService;
        private readonly IMapper _mapper;

        public PeopleController(ApplicationDbContext context, IFileStorageService fileStorageService,IMapper mapper)
        {
            _context = context;
            _fileStorageService = fileStorageService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Person>>> Get()
        {
            return await _context.People.ToListAsync();
        }

        [HttpGet("search/{searchText}")]
        public async Task<ActionResult<List<Person>>> FilterByName(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText)) { return new List<Person>(); }
            return await _context.People.Where(x => x.Name.Contains(searchText))
                .Take(5)
                .ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(Person person)
        {
            if (!string.IsNullOrWhiteSpace(person.Picture))
            {
                var personPicture = Convert.FromBase64String(person.Picture);
                person.Picture = await _fileStorageService.SaveFile(personPicture, "jpg", "people");
            }
            _context.Add(person);
            await _context.SaveChangesAsync();

            return person.Id;
        }
    }
}
