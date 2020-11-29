using Biblioteca_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Biblioteca_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComentarioController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ComentarioController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost]
        public ActionResult Post([FromBody] Comentario comentario)
        {
            try
            {
                string email = _configuration["Comnetarios:email"];
                string password = _configuration["Comnetarios:password"];
                MailMessage mailMessage = new MailMessage(email, email, comentario.Asunto, comentario.Mensaje);
                mailMessage.IsBodyHtml = true;

                SmtpClient smtpClient = new SmtpClient(_configuration["SmtpClient:smtp"]);
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Port = Convert.ToInt32(_configuration["SmtpClient:puerto"]);
                smtpClient.Credentials = new System.Net.NetworkCredential(email, password);
                smtpClient.Send(mailMessage);
                smtpClient.Dispose();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
                throw;
            }
            
        }
    }
}
