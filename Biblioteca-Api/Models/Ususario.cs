using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca_Api.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string NumeroDocumento { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int EstadoId { get; set; }
        public Estado Estado { get; set; }
        public int RolId { get; set; }
        public Rol Rol { get; set; }
    }
}
