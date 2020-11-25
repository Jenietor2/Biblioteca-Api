using Biblioteca_Api.DTOs;
using Biblioteca_Api.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Biblioteca_Api.Helpers
{
    public class Users
    {
        private readonly UserManager<Usuario> _userManager;
        public Users(UserManager<Usuario> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> AsignarRolUsuario(EditarRolDTO editarRolDTO)
        {
            var usuario = await _userManager.FindByIdAsync(editarRolDTO.UsuarioId.ToString());

            if (usuario == null)
            {
                return null;
            }

            await _userManager.AddClaimAsync(usuario, new Claim(ClaimTypes.Role, editarRolDTO.NombreRol));
            await _userManager.AddToRoleAsync(usuario, editarRolDTO.NombreRol);

            return "Ok";
        }
    }
}
