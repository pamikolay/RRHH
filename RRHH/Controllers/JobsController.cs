using RRHH.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace RRHH.Controllers
{
    public class JobsController : Controller
    {
        // GET: Jobs
        public ActionResult Busquedas()
        {
            JobManager jManager = new JobManager();
            ViewBag.TablaBusquedas = jManager.ConsultarActivas();
            return View();
        }
        public ActionResult AgregarNuevaBusqueda()
        {
            CompanyManager cManager = new CompanyManager();
            ViewBag.TablaCompany = cManager.ConsultarTodos();
            return View();
        }

        [HttpPost]
        public ActionResult GuardarNuevaBusqueda(string job_name, string job_description, int company_id)
        {
            Job newJob = new Job();
            newJob.JobName = job_name;
            newJob.JobDescription = job_description;
            CompanyManager cManager = new CompanyManager();     //para pasarle un objeto company necesito el CompanyManager
            newJob.Company = cManager.Consultar(company_id);

            JobManager jManager = new JobManager();
            jManager.Insertar(newJob);

            return RedirectToAction("Busquedas", "Jobs");
        }

        [HttpPost]
        public ActionResult EditarBusqueda(int id_job)
        {
            JobStatusManager jSmanager = new JobStatusManager();
            JobManager jManager = new JobManager();
            CompanyManager cManager = new CompanyManager();
            ViewBag.Company = cManager.ConsultarTodos();       //paso la lista de las compañias
            ViewBag.JobAmodificar = jManager.Consultar(id_job);     //paso el job actual
            ViewBag.JobStatus = jSmanager.ConsultarTodos();         //paso la lista de los jobstatus

            return View();
            //return RedirectToAction("EditarBusqueda", "Jobs");
        }

        [HttpPost]
        public ActionResult EditarBusquedaGuardarCambios(int job_id, string job_name, string job_description, int company_id, int job_status_id)
        {
            Job newJob = new Job();
            newJob.JobID = job_id;
            newJob.JobName = job_name;
            newJob.JobDescription = job_description;
            CompanyManager cManager = new CompanyManager();     //para pasarle un objeto company necesito el CompanyManager
            newJob.Company = cManager.Consultar(company_id);
            JobStatusManager jStatusManager = new JobStatusManager();
            newJob.JobStatus = jStatusManager.Consultar(job_status_id);
            JobManager jManager = new JobManager();
            jManager.Actualizar(newJob);

            return RedirectToAction("Busquedas", "Jobs"); ;
        }

        public ActionResult PruebaEmail()
        {
            ViewBag.mensaje = MailCambioEstado("Pepe Argento","pa.mikolay@gmail.com","laralaaaaa","probando probando");
            return View();
        }

        [HttpPost]
        public string MailCambioEstado(string nombre, string mail, string asunto, string cuerpo)
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
                mailAEnviar.To.Add(mail);
                mailAEnviar.Subject = asunto;   //"Avance proceso de selección";
                mailAEnviar.IsBodyHtml = true;
                mailAEnviar.Body = "Hola " + nombre + ", <br />Te comentamos como avanza tu proceso de selección: <br />" + cuerpo + "<br /><br /><b>Saludos!!!<br />World Solution RRHH";

                //mandamos el mail
                clienteSmtp.Send(mailAEnviar);
                //Si todo sale bien configuro una mensaje
                mensaje = "Se ha enviado el cambio de estado al candidato mediante un e-mail.";
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