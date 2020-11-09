using Biblioteca_Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca_Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AutorLibro>().HasKey(x => new { x.IdAutor, x.IdLibro });
            modelBuilder.Entity<Prestamo>().HasKey(x => new { x.LibroId, x.UsuarioId });

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Autor> Autores { get; set; }
        public DbSet<AutorLibro> AutorLibros { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Libro> Libros { get; set; }
        public DbSet<Prestamo> Prestamos { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
