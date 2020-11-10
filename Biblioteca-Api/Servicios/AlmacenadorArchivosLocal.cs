using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca_Api.Servicios
{
    public class AlmacenadorArchivosLocal : IAlmacenadorArchivos
    {
        private readonly IWebHostEnvironment env;
        private readonly IHttpContextAccessor httpContextAccesor;

        public AlmacenadorArchivosLocal(IWebHostEnvironment env, IHttpContextAccessor httpContextAccesor)
        {
            this.env = env;
            this.httpContextAccesor = httpContextAccesor;
        }
        public Task BorrarArchivo(string ruta, string contenedor)
        {
            if (ruta != null)
            {
                string nombreArchivo = Path.GetFileName(ruta);
                string directorioArchivo = Path.Combine(env.WebRootPath, contenedor, nombreArchivo);

                if (File.Exists(directorioArchivo))
                {
                    File.Delete(directorioArchivo);
                }
            }

            return Task.FromResult(0);
        }

        public async Task<string> EditarArchivo(byte[] contenido, string extension, string contenedor, string ruta, string contentType)
        {
            await BorrarArchivo(ruta, contenedor);
            return await GuardarArchivo(contenido, extension, contenedor, contentType);
        }

        public async Task<string> GuardarArchivo(byte[] contenido, string extension, string contenedor, string contentType)
        {
            string nombreArchivo = $"{Guid.NewGuid()}{extension}";
            string folder = Path.Combine(env.WebRootPath, contenedor);

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            string ruta = Path.Combine(folder, nombreArchivo);
            await File.WriteAllBytesAsync(ruta, contenido);

            string urlActual = $"{httpContextAccesor.HttpContext.Request.Scheme}://{httpContextAccesor.HttpContext.Request.Host}";
            string urlBd = Path.Combine(urlActual, contenedor, nombreArchivo).Replace("\\", "/");

            return urlBd;
        }
    }
}
