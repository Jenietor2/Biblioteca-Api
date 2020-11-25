using Biblioteca_Api.Data;
using Biblioteca_Api.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca_Api.Controllers
{
    [ApiController]
    [Route("api/generos")]
    public class GeneroController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public GeneroController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Genero>>>Get()
        {
            List<Genero> lstGeneros = await context.Generos.Where(x => x.Activo).ToListAsync();
            return lstGeneros;
        }

        [HttpGet("{id}", Name = "obtenerGeneros")]
        public async Task<ActionResult<Genero>>Get(int id)
        {
            Genero genero = await context.Generos.FirstOrDefaultAsync(x => x.Id == id && x.Activo == true);

            if(genero == null)
            {
                return NotFound();
            }

            return genero;
        }

        [HttpPost]
        public async Task<ActionResult>Post([FromBody] Genero genero)
        {
            genero.Activo = !genero.Activo ? true : genero.Activo;
            context.Add(genero);
            await context.SaveChangesAsync();
            return new CreatedAtRouteResult("obtenerUsuario", new { id = genero.Id }, genero);
        }

        [HttpPut("eliminar/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        public async Task<ActionResult> Delete(int id)
        {
            Genero genero = await context.Generos.FirstOrDefaultAsync(x => x.Id == id);

            if (genero == null)
            {
                return NotFound();
            }

            genero.Activo = false;
            context.Entry(genero).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
