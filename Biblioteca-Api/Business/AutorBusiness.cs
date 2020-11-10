using AutoMapper;
using Biblioteca_Api.Data;
using Biblioteca_Api.DTOs;
using Biblioteca_Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca_Api.Business
{
    public class AutorBusiness
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public AutorBusiness(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<List<AutorDTO>> GetAutores()
        {
            try
            {
                List<Autor> lstAutor = await context.Autores.ToListAsync();
                List<AutorDTO> lstAutorDTO = mapper.Map<List<AutorDTO>>(lstAutor);
                return lstAutorDTO;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<AutorDTO> GetAutor(int id)
        {
            try
            {
                Autor autor = await context.Autores.FirstOrDefaultAsync(x => x.Id == id);
                AutorDTO autorDTO = mapper.Map<AutorDTO>(autor);

                return autorDTO;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int> InsertAutor(AutorDTO autorDTO)
        {
            try
            {
                Autor autor = mapper.Map<Autor>(autorDTO);
                context.Add(autor);
                await context.SaveChangesAsync();
                return autor.Id;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> UpdateAutor(AutorDTO autorDTO)
        {
            try
            {
                Autor autor = mapper.Map<Autor>(autorDTO);
                context.Entry(autor).State = EntityState.Modified;
                await context.SaveChangesAsync();
                
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
