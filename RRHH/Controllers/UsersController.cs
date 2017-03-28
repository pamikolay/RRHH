using RRHH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RRHH.Controllers
{
    public class UsersController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Users user)
        {
            UsersManager uManager = new UsersManager();
            string message = "";
            if (ModelState.IsValid)
            {
                if (uManager.CheckUser(user) > 0)
                {
                    message = "Correcto";
                    user = uManager.Consultar(user.Email);
                    Session["UsuarioLogueado"] = user;
                }
                else
                {
                    message = "Usuario o Contraseña incorrecta";

                }
            }
            else
            {
                message = "Todos los campos son requeridos";
            }

            if (Request.IsAjaxRequest())
            {
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Logout()
        {
            Session["UsuarioLogueado"] = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Registro()
        {
            ProvincesManager pManager = new ProvincesManager();
            ViewBag.Provinces = pManager.ConsultarTodas();
            return View();
        }
        [HttpPost]
        public ActionResult GetCiudades(int id)
        {
            List<Citys> ciudades = new List<Citys>();
            ciudades = new CitysManager().GetCiudadesPorProvincia(id);
            return Json(new SelectList(ciudades, "ID", "Name"));
        }

        [HttpPost]
        public ActionResult GuardarNuevoUsuario(string first_name, string last_name, string password, string email, string phone, string address, int province_id, int city_id, string genre)
        {
            Users newUser = new Users();
            newUser.FirstName = first_name;
            newUser.LastName = last_name;
            newUser.Password = password;
            newUser.Email = email;
            newUser.Phone = phone;
            newUser.Address = address;
            newUser.Genre = genre;
            ProvincesManager pManager = new ProvincesManager();
            newUser.Province = pManager.Consultar(province_id);
            CitysManager cManager = new CitysManager();
            newUser.City = cManager.Consultar(city_id);

            UsersManager uManager = new UsersManager();
            uManager.Insertar(newUser);

            return RedirectToAction("Index", "Home");
        }
        public ActionResult ApplyJob(int id_job)
        {
            if (Session["UsuarioLogueado"]==null)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                Users user = (Users)Session["UsuarioLogueado"];
                Applicants applicant = new Applicants();
                applicant.Postulant = user;
                JobsManager jManeger = new JobsManager();
                applicant.Job= jManeger.Consultar(id_job);
                Session["TrabajoPostulado"] = applicant.Job;
                ApplicantsManager aManager = new ApplicantsManager();
                if ( aManager.CheckApplicant(user,(applicant.Job)) > 0) //compruebo si el ususario ya aplico a esa busqueda
                {
                    return View("/Views/Users/YaAplicaste.cshtml");
                }
                aManager.Insertar(applicant);
                return View();
            }
        }
        public ActionResult MisPostulaciones()
        {
            List<Applicants> applys = new List<Applicants>();
            applys = new ApplicantsManager().ConsultarPorUser((Users)Session["UsuarioLogueado"]);
            ViewBag.UserApplys = applys;
            return View();
        }
    }
}