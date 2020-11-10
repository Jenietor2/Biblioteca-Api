using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca_Api.Models
{
    public class Autor
    {
        public int Id { get; set; }
        [Required]
        [StringLength(70)]
        public string Nombre { get; set; }
        public List<Libro> Libros { get; set; }
    }
}
