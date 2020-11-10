using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca_Api.DTOs
{
    public class EstadoDTO
    {
        public int Id { get; set; }
        [Required]
        [StringLength(25)]
        public string Nombre { get; set; }
        [Required]
        [MaxLength(200)]
        public string Descripcion { get; set; }
        public List<LibroDTO> Libros { get; set; }
        public List<UsuarioDTO> Usuarios { get; set; }
        public List<PrestamoDTO> Prestamos { get; set; }
        public List<RolDTO> Roles { get; set; }
    }
}
