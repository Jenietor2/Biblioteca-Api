using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca_Api.Models
{
    public class Usuario : IdentityUser
    {
        [StringLength(80)]
        public string Nombres { get; set; }
        [StringLength(80)]
        public string Apellidos { get; set; }
        [StringLength(18)]
        public string NumeroDocumento { get; set; }
        [StringLength(80)]
        public string Direccion { get; set; }
        public string Password { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool Activo { get; set; }
        public List<Libro> Libros { get; set; }
    }
}
