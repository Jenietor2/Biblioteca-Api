using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca_Api.DTOs
{
    public class LibroCreacionDTO
    {
        [Required]
        [StringLength(120)]
        public string Titulo { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Genero { get; set; }
        public List<AutorLibroDTO> AutorLibros { get; set; }
        public int EstadoId { get; set; }
        public EstadoDTO Estado { get; set; }
        public List<PrestamoDTO> Prestamos { get; set; }
    }
}
