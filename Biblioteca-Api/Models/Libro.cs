using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca_Api.Models
{
    public class Libro
    {
        public int Id { get; set; }
        [Required]
        [StringLength(120)]
        public string Titulo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int AutorId { get; set; }
        public Autor Autor { get; set; }
        public bool Activo { get; set; } = true;
        public int GeneroId { get; set; }
        public Genero Genero { get; set; }
        public int UsuarioID { get; set; }
        public Usuario Usuario { get; set; }

    }
}
