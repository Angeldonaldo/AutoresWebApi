using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIAutores.Entidades;

namespace WebAPIAutores.Controllers
{
    [ApiController]
    [Route("api/libros")]
    public class LibrosController: ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public LibrosController(ApplicationDbContext context) {
            this._context = context;   
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Libro>>  Get(int id) { 
            return await _context.Libros.Include(x => x.Autor).FirstOrDefaultAsync(x => x.Id == id);
        }
        [HttpPost]
        public async Task<ActionResult> Post(Libro libro)
        {
            var autores = await _context.Autores.AnyAsync(x =>x.Id == libro.AutorId);
            if (!autores)
            {
                return BadRequest($"No existe el autor de ID {libro.AutorId}" );
            }
            _context.Libros.Add(libro);
            await _context.SaveChangesAsync();
            return Ok(libro);
        }
    }
}
