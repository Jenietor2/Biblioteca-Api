using AutoMapper;
using Biblioteca_Api.Data;
using Biblioteca_Api.DTOs;
using Biblioteca_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Biblioteca_Api.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    public class UsuariosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public UsuariosController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<UsuarioDTO>>> Get()
        {
            List<Usuario> lstUsuarios = await context.Usuarios.ToListAsync();
            List<UsuarioDTO> lstUsuariosDTO = mapper.Map<List<UsuarioDTO>>(lstUsuarios);
            return lstUsuariosDTO;
        }

        [HttpGet("{id:int}", Name = "obtenerUsuario")]
        public async Task<ActionResult<UsuarioDTO>> Get(int id)
        {
            Usuario usuario = await context.Usuarios.Include(x => x.Rol).
                FirstOrDefaultAsync(x => x.Id == id && x.Activo == true);

            if (usuario == null)
            {
                return NotFound();
            }

            UsuarioDTO usuarioDTO = mapper.Map<UsuarioDTO>(usuario);
            return usuarioDTO;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UsuarioCreacionDTO usuarioCreacionDTO)
        {
            Usuario usuario = mapper.Map<Usuario>(usuarioCreacionDTO);
            context.Add(usuario);
            await context.SaveChangesAsync();
            UsuarioDTO usuarioDTO = mapper.Map<UsuarioDTO>(usuario);
            return new CreatedAtRouteResult("obtenerUsuario", new { id = usuarioDTO.Id }, usuarioDTO);
        }

        [HttpPut("actualizar/{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] UsuarioDTO usuarioDTO )
        {
            Usuario usuario = mapper.Map<Usuario>(usuarioDTO);
            usuario.Id = id;
            context.Entry(usuario).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPut("eliminar/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            Usuario usuario = await context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);

            if(usuario == null)
            {
                return NotFound();
            }

            usuario.Activo = false;
            context.Entry(usuario).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
