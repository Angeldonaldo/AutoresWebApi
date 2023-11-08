using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIAutores.Entidades;
using WebAPIAutores.Filtros;
using WebAPIAutores.Servicios;

namespace WebAPIAutores.Controllers
{
    [ApiController]
    [Route("api/autores")]
    //[Authorize]
    public class AutoresController : ControllerBase { 

          
        private readonly ApplicationDbContext context;
        private readonly IServicio servicio;
        private readonly ServicioTransient servicioTransient;
        private readonly ServicioScoped servicioScoped;
        private readonly ServicioSingleton servicioSingleton;
        private readonly ILogger<AutoresController> logger;

        public AutoresController(ApplicationDbContext context, IServicio servicio,ServicioTransient servicioTransient,
            ServicioScoped servicioScoped,ServicioSingleton servicioSingleton, ILogger<AutoresController> logger) {
            this.context = context;
            this.servicio = servicio;
            this.servicioTransient = servicioTransient;
            this.servicioScoped = servicioScoped;
            this.servicioSingleton = servicioSingleton;
            this.logger = logger;
        }
        [HttpGet("GUID")]
        //[ResponseCache(Duration = 10)]
        [ServiceFilter(typeof(MiFiltroDeAccion))]
        public ActionResult ObtenerGuids()
        {
            return Ok(new
            {
                AutoresController_Tansient = servicioTransient.Guid,
                ServicioA_Transient = servicio.ObtenerTransient(),
                AutoresController_Scoped = servicioScoped.Guid,
                ServicioA_Scoped = servicio.ObtenerScoped(),
                AutoresController_Singleton = servicioSingleton.Guid,
                ServicioA_Singleton = servicio.ObtenerSingleton()
            });
        }

        [HttpGet]
        [HttpGet("listado")]
        [HttpGet("/listado")]
        [ResponseCache(Duration = 10)]
        [ServiceFilter(typeof(MiFiltroDeAccion))]

        public async Task<ActionResult<List<Autor>>> Get()
        {
            //throw new NotImplementedException();
            logger.LogInformation("Estamos obteniedo los autoes");
            logger.LogWarning("Este es un mensaje de prueba");
            servicio.RealizarTarea();
            return await context.Autores.Include(x=>x.Libros).ToListAsync();
        }
        [HttpGet("primero")]
        public async Task<ActionResult<Autor>> primerAutor()
        {
            return await context.Autores.FirstOrDefaultAsync();
        }

        [HttpGet("{id:int}/{param2?}")]
        public async Task<ActionResult<Autor>> Get(int id,String param2)
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