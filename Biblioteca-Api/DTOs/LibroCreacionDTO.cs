using Biblioteca_Api.Models;
using Biblioteca_Api.Validaciones;
using Microsoft.AspNetCore.Http;
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
        public DateTime FechaCreacion { get; set; }
        public string Autor { get; set; }
        public bool Activo { get; set; } = true;
        public int GeneroId { get; set; }
        public GeneroDTO Genero { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        [PesoArchivoValidacion(pesoMaximoEnMegaBytes: 20)]
        [TipoArchivoValidacion(grupoTipoArchivo: GrupoTipoArchivo.Texto)]
        public IFormFile RutaLibro { get; set; }

    }
}
