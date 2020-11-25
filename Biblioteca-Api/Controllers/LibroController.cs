using AutoMapper;
using Biblioteca_Api.Data;
using Biblioteca_Api.DTOs;
using Biblioteca_Api.Models;
using Biblioteca_Api.Servicios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca_Api.Controllers
{
    [ApiController]
    [Route("api/libros")]
    public class LibroController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IAlmacenadorArchivos almacenador;
        private readonly string contenedor = "Libros";

        public LibroController(ApplicationDbContext context, IMapper mapper, IAlmacenadorArchivos almacenador)
        {
            this.context = context;
            this.mapper = mapper;
            this.almacenador = almacenador;
        }
        [HttpGet]
        public async Task<ActionResult<List<Libro>>> Get()
        {
            List<Libro> lstLibro = await context.Libros.Where(x => x.Activo)
                .Include(x => x.Genero)
                .Include(x => x.Usuario)
                .ToListAsync();
            //List<LibroDTO> lstLibroDto = mapper.Map<List<LibroDTO>>(lstLibro);

            return lstLibro;
        }

        [HttpGet("{id:int}", Name = "obtenerLibro")]
        public async Task<ActionResult<LibroDTO>> Get(int id)
        {
            Libro libro = await context.Libros.FirstOrDefaultAsync(x => x.Id == id && x.Activo);

            if (libro == null)
            {
                return NotFound();
            }

            LibroDTO libroDTO = mapper.Map<LibroDTO>(libro);
            return libroDTO;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] LibroCreacionDTO libroCreacionDTO)
        {
            Libro libro = mapper.Map<Libro>(libroCreacionDTO);

            if(libroCreacionDTO.RutaLibro != null)
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    await libroCreacionDTO.RutaLibro.CopyToAsync(memoryStream);
                    byte[] contenido = memoryStream.ToArray();
                    string extencion = Path.GetExtension(libroCreacionDTO.RutaLibro.FileName);
                    libro.Ruta = await almacenador.GuardarArchivo(contenido, extencion, contenedor, libroCreacionDTO.RutaLibro.ContentType);
                }
            }

            context.Add(libro);
            await context.SaveChangesAsync();

            LibroDTO libroDTO = mapper.Map<LibroDTO>(libro);

            return new CreatedAtRouteResult("obtenerLibro", new { id = libroDTO.Id }, libroDTO);
        }

        [HttpPut("actualizar/{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] LibroCreacionDTO libroCreacionDTO)
        {
            Libro libroBD = await context.Libros.FirstOrDefaultAsync(x => x.Id == id && x.Activo);

            if(libroBD == null)
            {
                return NotFound();
            }

            libroBD = mapper.Map<Libro>(libroCreacionDTO);

            if (libroCreacionDTO.RutaLibro != null)
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    await libroCreacionDTO.RutaLibro.CopyToAsync(memoryStream);
                    byte[] contenido = memoryStream.ToArray();
                    string extencion = Path.GetExtension(libroCreacionDTO.RutaLibro.FileName);
                    libroBD.Ruta = await almacenador.EditarArchivo(contenido, extencion, contenedor, libroBD.Ruta, libroCreacionDTO.RutaLibro.ContentType);
                }
            }

            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("eliminar/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        public async Task<ActionResult> Eliminacion(int id)
        {
            Libro libro = await context.Libros.FirstOrDefaultAsync(x => x.Id == id && x.Activo);

            if (libro == null)
            {
                return NotFound();
            }

            libro.Activo = false;
            context.Entry(libro).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
