using AutoMapper;
using Biblioteca_Api.DTOs;
using Biblioteca_Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca_Api.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Usuario, UsuarioDTO>().ReverseMap();
            CreateMap<Libro, LibroDTO>().ReverseMap();
            CreateMap<Estado, EstadoDTO>().ReverseMap();
            CreateMap<Rol, RolDTO>().ReverseMap();
            CreateMap<Autor, AutorDTO>().ReverseMap();
            CreateMap<AutorLibro, AutorLibroDTO>().ReverseMap();
            CreateMap<UsuarioCreacionDTO, Usuario>();
            CreateMap<LibroCreacionDTO, Libro>();
        }
    }
}
