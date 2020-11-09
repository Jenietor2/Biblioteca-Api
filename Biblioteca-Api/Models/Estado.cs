using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca_Api.Models
{
    public class Estado
    {
        public int Id { get; set; }
        [Required]
        [StringLength(25)]
        public string Nombre { get; set; }
        [Required]
        [MaxLength(200)]
        public string Descripcion { get; set; }
        public List<Libro> Libros { get; set; }
        public List<Usuario> Usuarios { get; set; }
        public List<Prestamo> Prestamos { get; set; }
        public List<Rol> Roles { get; set; }
    }
}
