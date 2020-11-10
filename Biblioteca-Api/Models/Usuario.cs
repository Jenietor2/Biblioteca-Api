using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca_Api.Models
{
    public class Usuario
    {
        public int Id { get; set; }
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
        public int RolId { get; set; }
        public Rol Rol { get; set; }
        public bool Activo { get; set; }
        public List<Libro> Libros { get; set; }
    }
}
