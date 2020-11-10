using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca_Api.DTOs
{
    public class LibroDTO
    {
        public int Id { get; set; }
        [Required]
        [StringLength(120)]
        public string Titulo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int AutorId { get; set; }
        public AutorDTO Autor { get; set; }
        public bool Activo { get; set; } = true;
        public int GeneroId { get; set; }
        public GeneroDTO Genero { get; set; }
        public int UsuarioID { get; set; }
        public UsuarioDTO Usuario { get; set; }
    }
}
