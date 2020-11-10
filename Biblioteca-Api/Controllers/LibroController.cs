using AutoMapper;
using Biblioteca_Api.Data;
using Biblioteca_Api.DTOs;
using Biblioteca_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Biblioteca_Api.Controllers
{
    [ApiController]
    [Route("api/libros")]
    public class LibroController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public LibroController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<List<LibroDTO>>> Get()
        {
            List<Libro> lstLibro = await context.Libros.Where(x => x.EstadoId == 1).ToListAsync();
            List<LibroDTO> lstLibroDto = mapper.Map<List<LibroDTO>>(lstLibro);

            return lstLibroDto;
        }

        [HttpGet("{id:int}", Name = "obtenerLibro")]
        public async Task<ActionResult<LibroDTO>> Get(int id)
        {
            Libro libro = await context.Libros.FirstOrDefaultAsync(x => x.Id == id && x.EstadoId == 1);

            if (libro == null)
            {
                return NotFound();
            }

            LibroDTO libroDTO = mapper.Map<LibroDTO>(libro);
            return libroDTO;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] LibroCreacionDTO libroCreacionDTO)
        {
            Libro libro = mapper.Map<Libro>(libroCreacionDTO);
            context.Add(libro);

            await context.SaveChangesAsync();
            LibroDTO libroDTO = mapper.Map<LibroDTO>(libro);

            return new CreatedAtRouteResult("obtenerLibro", new { id = libroDTO.Id }, libroDTO);
        }

        [HttpPut("actualizar/{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] LibroDTO libroDTO)
        {
            Libro libro = mapper.Map<Libro>(libroDTO);
            libro.Id = id;
            context.Entry(libro).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("eliminar/{id}")]
        public async Task<ActionResult> Eliminacion(int id)
        {
            Libro libro = await context.Libros.FirstOrDefaultAsync(x => x.Id == id && x.EstadoId == 1);

            if (libro == null)
            {
                return NotFound();
            }

            libro.EstadoId = 2;
            context.Entry(libro).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
