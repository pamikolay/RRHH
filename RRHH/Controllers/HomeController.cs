using RRHH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace RRHH.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if(Session["UsuarioLogueado"] != null && ((Users)Session["UsuarioLogueado"]).UserType.ID==1)
            {
                return RedirectToAction("Index", "ControlPanel");
            }
            return View();
        }

        public ActionResult Contacto()
        {
            return View();
        }

        public ActionResult Contactarse(string name, string email, string subject, string message)
        {
            string envioContacto = EmailContacto(name,email,subject,message);
            ViewBag.Mensaje = envioContacto;

            return View();
        }

        [HttpPost]
        public string EmailContacto(string nombre, string mail, string asunto, string cuerpo)
        {
            string mensaje = "";
            try
            {
                //Definimos la conexión al servidor SMTP que vamos a usar
                //para mandar el mail. Hay que buscarla en nuestro proveedor
                SmtpClient clienteSmtp = new SmtpClient("smtp.gmail.com", 587);
                clienteSmtp.Credentials = new NetworkCredential("rrhh.proyecto.comit@gmail.com", "proyectoCom17");
                clienteSmtp.EnableSsl = true;

                //Generamos el objeto MAIL a enviar
                MailMessage mailAEnviar = new MailMessage();
                mailAEnviar.From = new MailAddress("rrhh.proyecto.comit@gmail.com", "RRHH Proyecto ComIT-2017");
                mailAEnviar.To.Add("rrhh.proyecto.comit@gmail.com");
                mailAEnviar.Subject = "Contacto - World Solutios RRHH";
                mailAEnviar.IsBodyHtml = true;
                mailAEnviar.Body = "Nombre: " + nombre + "<br />Mail: " + mail + "<br /><strong>Asunto: " + asunto + "</strong><br />Mensaje: " + cuerpo;

                //mandamos el mail
                clienteSmtp.Send(mailAEnviar);
                //Si todo sale bien configuro una mensaje
                mensaje = "Tu consulta ha sido enviada y sera respondida a la brevedad.";
            }
            catch (Exception ex)
            {
                mensaje = "Lo sentimos, ha ocurrido un error al enviar el mail. Intenta nuevamente en unos minutos";
                string detalleError = ex.Message;   //guardo el error para ver que paso pero no lo muestro al usuario
                //mensaje = "ERROR: " + ex.Message;
            }

            return mensaje;
        }
    }
}