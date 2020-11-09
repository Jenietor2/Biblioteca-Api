using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca_Api.Models
{
    public class Libro
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Genero { get; set; }
        public List<AutorLibro> AutorLibros { get; set; }
        public int EstadoId { get; set; }
        public Estado Estado { get; set; }
    }
}
