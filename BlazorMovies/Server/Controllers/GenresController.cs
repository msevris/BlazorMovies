using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorMovies.Shared.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorMovies.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class GenresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GenresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET api/<GenresController>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<Genre>>> Get()
        {
            return await _context.Genres.ToListAsync();
        }

        // GET api/<GenresController>{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Genre>> Get(int id)
        {
            var genre = await _context.Genres.FirstOrDefaultAsync(x => x.Id == id);
            if (genre == null) { return NotFound(); }
            return genre;
        }

        // POST api/<GenresController>
        [HttpPost]
        public async Task<ActionResult<int>> Post(Genre genre)
        {
            _context.Add(genre);
            await _context.SaveChangesAsync();
            return genre.Id;
        }

        // PUT api/<GenresController>
        [HttpPut]
        public async Task<ActionResult> Put(Genre genre)
        {
            _context.Attach(genre).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        //DELETE api/<GenresController>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var genre = await _context.Genres.FirstOrDefaultAsync(x => x.Id == id);
            if (genre == null) { return NotFound(); }
            _context.Remove(genre);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
