using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca_Api.DTOs
{
    public class PrestamoDTO
    {
        public int LibroId { get; set; }
        public int UsuarioId { get; set; }
        public LibroDTO Libro { get; set; }
        public UsuarioDTO Usuario { get; set; }
        public DateTime FechaPrestamo { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public int EstadoId { get; set; }
        public EstadoDTO Estado { get; set; }
    }
}
