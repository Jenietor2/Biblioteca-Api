using Biblioteca_Api.DTOs;
using Biblioteca_Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca_Api.Helpers
{
    public class Utilidades
    {
        public Usuario DifferenceBetweenUsers(Usuario usuarioBD, UsuarioActualizacion usuario)
        {
            if (usuarioBD.Nombres != usuario.Nombres)
            {
                usuarioBD.Nombres = usuario.Nombres;
            }
            if (usuarioBD.Apellidos != usuario.Apellidos)
            {
                usuarioBD.Apellidos = usuario.Apellidos;
            }
            if (usuarioBD.NumeroDocumento != usuario.NumeroDocumento)
            {
                usuarioBD.NumeroDocumento = usuario.NumeroDocumento;
            }
            if (usuarioBD.Direccion != usuario.Direccion)
            {
                usuarioBD.Direccion = usuario.Direccion;
            }
            return usuarioBD;
        }
    }
}
