using Biblioteca_Api.DTOs;
using Biblioteca_Api.Helpers;
using Biblioteca_Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentasController : ControllerBase
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly IConfiguration _configuration;

        public CuentasController(UserManager<Usuario> userManager,
            SignInManager<Usuario> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost("Crear")]
        public async Task<ActionResult<UserToken>> CreateUser([FromBody] Usuario model)
        {
            model.UserName = model.Email;
            model.Activo = model.Activo ? model.Activo : true;
            model.FechaCreacion = model.FechaCreacion <= DateTime.MinValue ? DateTime.Now : model.FechaCreacion;

            var result = await _userManager.CreateAsync(model, model.Password);

            if (result.Succeeded)
            {
                UsuarioLogin usuarioLogin = new UsuarioLogin
                {
                    Email = model.Email,
                    Password = model.Password
                };

                EditarRolDTO editRolDTO = new EditarRolDTO {
                    UsuarioId = model.Id,
                    NombreRol = _configuration["RolDefault"].ToString()
                };

                Users user = new Users(_userManager);
                await user.AsignarRolUsuario(editRolDTO);

                List<string> lstRols = new List<string>();
                lstRols.Add(editRolDTO.NombreRol);
                return BuildToken(usuarioLogin, lstRols);
            }
            else
            {
                return BadRequest("Username or password invalid");
            }

        }


        [HttpPost("Login")]
        public async Task<ActionResult<UserToken>> Login([FromBody] Usuario usuario)
        {
            var result = await _signInManager.PasswordSignInAsync(usuario.Email, usuario.Password, isPersistent: false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var usuarioBD = await _userManager.FindByEmailAsync(usuario.Email);
                var roles = await _userManager.GetRolesAsync(usuarioBD);
                UsuarioLogin usuarioLogin = new UsuarioLogin
                {
                    Email = usuario.Email,
                    Password = usuario.Password
                };
                return BuildToken(usuarioLogin, roles);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return BadRequest(ModelState);
            }
        }

        private UserToken BuildToken(UsuarioLogin usuarioLogin, IList<string> roles)
        {
            var claims = new List<Claim>
            {
        new Claim(ClaimTypes.Name, usuarioLogin.Email),
        new Claim(ClaimTypes.Email, usuarioLogin.Email)
    };

            foreach (var rol in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, rol));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Tiempo de expiración del token. En nuestro caso lo hacemos de una hora.
            var expiration = DateTime.Now.AddHours(Convert.ToInt32(_configuration["JWT:expirationTime"]));

            JwtSecurityToken token = new JwtSecurityToken(
               issuer: null,
               audience: null,
               claims: claims,
               expires: expiration,
               signingCredentials: creds);

            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration,
                RolName = roles[0]
            };
        }
        [HttpPost("AsignarUsuarioRol")]
        public async Task<ActionResult> AsignarRolUsuario(EditarRolDTO editarRolDTO)
        {
            Users user = new Users(_userManager);
            string res = await user.AsignarRolUsuario(editarRolDTO);

            if (!res.Equals("Ok"))
            {
                return BadRequest("Error asignando rol a usuario");
            }
            return Ok();
        }

        [HttpPost("RemoverUsuarioRol")]
        public async Task<ActionResult> RemoverRolUsuario(EditarRolDTO editarRolDTO)
        {
            var usuario = await _userManager.FindByIdAsync(editarRolDTO.UsuarioId);

            if (usuario == null)
            {
                return NotFound();
            }

            await _userManager.RemoveClaimAsync(usuario, new Claim(ClaimTypes.Role, editarRolDTO.NombreRol));
            await _userManager.RemoveFromRoleAsync(usuario, editarRolDTO.NombreRol);
            return Ok();
        }
    }
}
