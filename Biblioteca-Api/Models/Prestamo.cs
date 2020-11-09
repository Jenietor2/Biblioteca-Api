using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca_Api.Models
{
    public class Prestamo
    {
        public int LibroId { get; set; }
        public int UsuarioId { get; set; }
        public Libro Libro { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime FechaPrestamo { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public int EstadoId { get; set; }
        public Estado Estado { get; set; }
    }
}
