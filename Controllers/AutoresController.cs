using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIAutores.Entidades;
using WebAPIAutores.Filtros;


namespace WebAPIAutores.Controllers
{
    [ApiController]
    [Route("api/autores")]
    public class AutoresController : ControllerBase { 

          
        private readonly ApplicationDbContext context;

        public AutoresController(ApplicationDbContext context, ILogger<AutoresController> logger) {
            this.context = context;

        }


        [HttpGet]
        public async Task<ActionResult<List<Autor>>> Get()
        {
            return await context.Autores.ToListAsync();
        }
        

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Autor>> Get(int id)
        {
            var autor = await context.Autores.FirstOrDefaultAsync(x => x.Id == id);
            if(autor == null)
            {
                return  NotFound();
            }
            return autor;
        }
        [HttpGet("{name}")]
        public async Task<ActionResult<Autor>> Get(String name)
        {
            var autor = await context.Autores.FirstOrDefaultAsync(x => x.Nombre.Contains(name));
            if (autor == null)
            {
                return NotFound();
            }
            return autor;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody]Autor autor)
        {
            var esisteAutorConElMismomNombre = await context.Autores.AnyAsync(x => x.Nombre == autor.Nombre);
            if (esisteAutorConElMismomNombre)
            {
                return BadRequest($"Ya existe un autor con el nombre {autor.Nombre}");
            }
            context.Add(autor);
            await context.SaveChangesAsync();
            return Ok(autor);
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Autor autor, int id)
        {
            if(autor.Id != id)
            {
                return BadRequest("El Autor no coicide con el Id de la URL");
            }
            var existe = await context.Autores.AnyAsync(x => x.Id == id);
            if(!existe)
            {
                return NotFound();
            }
            context.Update(autor);
            await context.SaveChangesAsync();
            return Ok(autor);
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Autores.AnyAsync(x=> x.Id==id);
            if (!existe)
            {
                return NotFound();
            }
            context.Remove(new Autor() { Id = id});
            await context.SaveChangesAsync();
            return Ok();
        }

    }
}