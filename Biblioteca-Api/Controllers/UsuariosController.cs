using AutoMapper;
using Biblioteca_Api.Data;
using Biblioteca_Api.DTOs;
using Biblioteca_Api.Helpers;
using Biblioteca_Api.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        private readonly UserManager<Usuario> userManager;
        private Utilidades utilidades;
        public UsuariosController(ApplicationDbContext context, UserManager<Usuario> userManager, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        public async Task<ActionResult<List<Usuario>>> Get()
        {
            List<Usuario> lstUsuarios = await context.Usuarios.Where(x => x.Activo).ToListAsync();
            //List<UsuarioDTO> lstUsuariosDTO = mapper.Map<List<UsuarioDTO>>(lstUsuarios);
            return lstUsuarios;
        }

        [HttpGet("{id}", Name = "obtenerUsuario")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<Usuario>> Get(string id)
        {
            Usuario usuario = await context.Usuarios.FirstOrDefaultAsync(x => x.Id.Equals(id) && x.Activo == true);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Usuario usuario)
        {
            //Usuario usuario = mapper.Map<Usuario>(usuarioCreacionDTO);
            usuario.Activo = !usuario.Activo ? true : usuario.Activo;
            usuario.FechaCreacion = usuario.FechaCreacion <= DateTime.MinValue ? DateTime.Now : usuario.FechaCreacion;
            context.Add(usuario);
            await context.SaveChangesAsync();
            return new CreatedAtRouteResult("obtenerUsuario", new { id = usuario.Id }, usuario);
        }

        [HttpPut("actualizar/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Put(string id, [FromBody] UsuarioActualizacion usuarioActualizacion)
        {
            var usuarioBD = await context.Usuarios.FirstOrDefaultAsync(x => x.Id.Equals(id));
            utilidades = new Utilidades();
            var usuarioUp = utilidades.DifferenceBetweenUsers(usuarioBD, usuarioActualizacion);
            context.Entry(usuarioUp).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult>Patch(string id, [FromBody] JsonPatchDocument<Usuario> patchDocument)
        {
            if(patchDocument == null)
            {
                return BadRequest();
            }

            Usuario usuario = await context.Usuarios.FirstOrDefaultAsync(x => x.Id.Equals(id));

            if(usuario == null)
            {
                return NotFound();
            }

            patchDocument.ApplyTo(usuario, ModelState);

            var isValid = TryValidateModel(usuario);

            if(!isValid)
            {
                return BadRequest(usuario);
            }

            await context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPut("eliminar/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        public async Task<ActionResult> Delete(string id)
        {
            Usuario usuario = await context.Usuarios.FirstOrDefaultAsync(x => x.Id.Equals(id));

            if (usuario == null)
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
