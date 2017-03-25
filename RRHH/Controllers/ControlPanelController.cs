using RRHH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RRHH.Controllers
{
    public class ControlPanelController : Controller
    {
        // GET: ControlPanel
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult BusquedasActivas()
        {
            JobsManager jManager = new JobsManager();
            ViewBag.TablaBusquedas = jManager.ConsultarActivas();
            return View();
        }
        public ActionResult BusquedasInactivas()
        {
            JobsManager jManager = new JobsManager();
            ViewBag.TablaBusquedas = jManager.ConsultarInactivas();
            return View();
        }
        public ActionResult BuscarPostulantes()
        {
            JobsManager jManager = new JobsManager();
            ViewBag.BusquedasActivas = jManager.ConsultarActivas();
            return View();
        }
        [HttpPost]
        public ActionResult GetPostulantes(int id)
        {
            List<Users> users = new List<Users>();
            users = new UsersManager().GetUsuariosPorBusqueda(id);
            return Json(new SelectList(users, "ID", "Email"));
        }
        public ActionResult AgregarNuevaBusqueda()
        {
            CompanysManager cManager = new CompanysManager();
            ViewBag.TablaCompany = cManager.ConsultarTodos();
            return View();
        }

        [HttpPost]
        public ActionResult GuardarNuevaBusqueda(string job_name, string job_description, int company_id)
        {
            Jobs newJob = new Jobs();
            newJob.Name = job_name;
            newJob.Description = job_description;
            CompanysManager cManager = new CompanysManager();     //para pasarle un objeto company necesito el CompanyManager
            newJob.Company = cManager.Consultar(company_id);

            JobsManager jManager = new JobsManager();
            jManager.Insertar(newJob);

            return RedirectToAction("BusquedasActivas", "ControlPanel");
        }

        [HttpPost]
        public ActionResult EditarBusqueda(int id_job)
        {
            JobStatusesManager jSmanager = new JobStatusesManager();
            JobsManager jManager = new JobsManager();
            CompanysManager cManager = new CompanysManager();
            ViewBag.Company = cManager.ConsultarTodos();       //paso la lista de las compañias
            ViewBag.JobAmodificar = jManager.Consultar(id_job);     //paso el job actual
            ViewBag.JobStatus = jSmanager.ConsultarTodos();         //paso la lista de los jobstatus

            return View();
            //return RedirectToAction("EditarBusqueda", "Jobs");
        }

        [HttpPost]
        public ActionResult EditarBusquedaGuardarCambios(int job_id, string job_name, string job_description, int company_id, int job_status_id)
        {
            Jobs newJob = new Jobs();
            newJob.ID = job_id;
            newJob.Name = job_name;
            newJob.Description = job_description;
            CompanysManager cManager = new CompanysManager();     //para pasarle un objeto company necesito el CompanyManager
            newJob.Company = cManager.Consultar(company_id);
            JobStatusesManager jStatusManager = new JobStatusesManager();
            newJob.Status = jStatusManager.Consultar(job_status_id);
            JobsManager jManager = new JobsManager();
            jManager.Actualizar(newJob);

            return RedirectToAction("Index", "ControlPanel"); ;
        }

        public ActionResult ConsultarPostulante(int busqueda_id, int user_id)
        {
            Applicants applicant = new ApplicantsManager().ConsultarEstado(busqueda_id, user_id);
            ViewBag.applicant = applicant;

            List<JobApplications> applicationsStatuses = new List<JobApplications>();
            applicationsStatuses = new JobsApplicationsManager().ConsultarTodos();
            ViewBag.JobApplications = applicationsStatuses;

            List<Interviews> interviewsStatuses = new List<Interviews>();
            interviewsStatuses = new InterviewsManager().ConsultarTodos();
            ViewBag.Interviews = interviewsStatuses;

            return View();
        }
        public ActionResult ModificarEstadoPostulante(int jobApp_id, int interview_id, int applicant_id)
        {

            return View();
        }
    }
}