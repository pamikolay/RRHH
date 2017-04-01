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

        public ActionResult MiPerfil()
        {
            ViewBag.Provinces = new ProvincesManager().ConsultarTodas();

            Users user = new UsersManager().Consultar(((Users)Session["UsuarioLogueado"]).ID);
            ViewBag.User = user;

            ViewBag.Citys = new CitysManager().GetCiudadesPorProvincia(user.Province.ID);

            return View();
        }

        public ActionResult ModificarDatos(string first_name, string last_name, string password, string email, string phone, string address, int province_id, int city_id, string genre)
        {
            Users user = new Users();
            user.ID = ((Users)Session["UsuarioLogueado"]).ID;
            user.FirstName = first_name;
            user.LastName = last_name;
            user.Password = password;
            user.Email = email;
            user.Phone = phone;
            user.Address = address;
            user.Genre = genre;
            user.Province = new Provinces();
            user.Province.ID = province_id;
            user.City = new Citys();
            user.City.ID = city_id;

            UsersManager uManager = new UsersManager();
            uManager.Actualizar(user);

            return View();
        }

        public ActionResult BusquedasActivas()
        {
            JobsManager jManager = new JobsManager();
            ViewBag.BusquedasActivas = jManager.ConsultarActivas();
            return View();
        }
        public ActionResult BusquedasInactivas()
        {
            JobsManager jManager = new JobsManager();
            ViewBag.BusquedasInactivas = jManager.ConsultarInactivas();
            return View();
        }
        public ActionResult VerBusqueda(int ID)
        {
            JobsManager jManager = new JobsManager();
            ViewBag.Busqueda = jManager.Consultar(ID);
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
            newJob.Company = new Companys();
            newJob.Company.ID = company_id;

            JobsManager jManager = new JobsManager();
            jManager.Insertar(newJob);

            return RedirectToAction("BusquedasActivas", "ControlPanel");
        }

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
            newJob.Company = new Companys();
            newJob.Company.ID = company_id;
            newJob.Status = new JobStatuses();
            newJob.Status.ID = job_status_id;
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
        public ActionResult ModificarEstadoPostulante(int applicant_id, int jobApp_id, int interview_id)
        {
            ApplicantsManager applicantMod = new ApplicantsManager();
            applicantMod.ActualizarEstado(applicant_id, jobApp_id, interview_id);

            Applicants apply = applicantMod.Consultar(applicant_id);
            string respuestaEmail = new EmailsManager().EmailCambioEstado(apply);
            ViewBag.respuestaEmail = respuestaEmail;
            ViewBag.datosApply = apply;

            return View();
        }
    }
}