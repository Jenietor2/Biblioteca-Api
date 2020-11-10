using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca_Api.DTOs
{
    public class AutorLibroDTO
    {
        public int IdAutor { get; set; }
        public int IdLibro { get; set; }
        public AutorDTO Autor { get; set; }
        public LibroDTO Libro { get; set; }
    }
}
