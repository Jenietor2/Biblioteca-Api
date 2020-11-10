using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca_Api.DTOs
{
    public class AutorDTO
    {
        public int Id { get; set; }
        [Required]
        [StringLength(70)]
        public string Nombre { get; set; }
        public List<LibroDTO> Libros { get; set; }
    }
}
