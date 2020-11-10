using Biblioteca_Api.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca_Api.DTOs
{
    public class UsuarioCreacionDTO
    {
        [Required]
        [StringLength(80)]
        public string Nombres { get; set; }
        [Required]
        [StringLength(80)]
        public string Apellidos { get; set; }
        [Required]
        [StringLength(18)]
        public string NumeroDocumento { get; set; }
        public DateTime FechaNacimiento { get; set; }
        [Required]
        [StringLength(80)]
        public string Direccion { get; set; }
        [Required]
        [StringLength(80)]
        public string Email { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int EstadoId { get; set; }
        public Estado Estado { get; set; }
        public int RolId { get; set; }
        public Rol Rol { get; set; }
        public List<Prestamo> Prestamos { get; set; }
    }
}
