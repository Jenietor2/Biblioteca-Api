using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca_Api.Models
{
    public class Rol
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(200)]
        public string Descripcion { get; set; }
        public int EstadoId { get; set; }
        public Estado Estado { get; set; }
        public List<Usuario> Usuarios { get; set; }
    }
}
