using RRHH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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
        public ActionResult ValidarEmail(string email)
        {
            int a = new UsersManager().ValidarPorEmail(email);
            return Json(a);
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
            newUser.Province = new Provinces();
            newUser.Province.ID = province_id;
            newUser.City = new Citys();
            newUser.City.ID = city_id;

            UsersManager uManager = new UsersManager();
            uManager.Insertar(newUser);

            EmailRegistro(newUser);

            return RedirectToAction("Login", "Users");
        }

        public ActionResult CargarCv(HttpPostedFileBase cv_file)
        {
            if (cv_file != null)
            {
                cv_file.SaveAs(Server.MapPath("~/Content/Cvs/" + ((Users)Session["UsuarioLogueado"]).ID + "-" + ((Users)Session["UsuarioLogueado"]).Email + ".pdf"));
                UsersManager uManager = new UsersManager();
                uManager.SubirCvOk(((Users)Session["UsuarioLogueado"]));
                return View("/Views/Users/CargaCvOk.cshtml");
            }
            return View("/Views/Users/CargarCv.cshtml");
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

        public ActionResult ApplyJob(int id_job)
        {
            if (Session["UsuarioLogueado"]==null)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                Users user = new UsersManager().UpdateStatusCv((Users)Session["UsuarioLogueado"]);
                if (user.CvStatus == "Cargado-OK")
                {
                    Applicants applicant = new Applicants();
                    applicant.Postulant = user;
                    applicant.Job = new Jobs();
                    applicant.Job.ID = id_job;
                    Session["TrabajoPostulado"] = applicant.Job;
                    ApplicantsManager aManager = new ApplicantsManager();
                        if ( aManager.CheckApplicant(user,(applicant.Job)) > 0) //compruebo si el ususario ya aplico a esa busqueda
                        {
                            return View("/Views/Users/YaAplicaste.cshtml");
                        }
                    aManager.Insertar(applicant);
                    return View();
                }
                else {
                    return View("/Views/Users/FaltaCv.cshtml");
                }
            }
        }
        public ActionResult MisPostulaciones()
        {
            List<Applicants> applys = new List<Applicants>();
            applys = new ApplicantsManager().ConsultarPorUser((Users)Session["UsuarioLogueado"]);
            ViewBag.UserApplys = applys;
            return View();
        }

        public void EmailRegistro(Users user)
        {
            string mensaje = "";
            try
            {
                //Definimos la conexión al servidor SMTP que vamos a usar
                //para mandar el mail. Hay que buscarla en nuestro proveedor
                SmtpClient clienteSmtp = new SmtpClient("smtp.gmail.com", 587);
                clienteSmtp.Credentials = new System.Net.NetworkCredential("rrhh.proyecto.comit@gmail.com", "proyectoCom17");
                clienteSmtp.EnableSsl = true;

                //Generamos el objeto MAIL a enviar
                MailMessage mailAEnviar = new MailMessage();
                mailAEnviar.From = new MailAddress("rrhh.proyecto.comit@gmail.com", "RRHH Proyecto ComIT-2017");
                mailAEnviar.To.Add(user.Email);
                mailAEnviar.Subject = "Registro - World Solutios RRHH";
                mailAEnviar.IsBodyHtml = true;
                mailAEnviar.Body = "Hola " + user.FirstName + ", gracias por registrarte.<br /><br /><strong>Te damos la bienvenida a World Solutions RRHH</strong>";

                //mandamos el mail
                clienteSmtp.Send(mailAEnviar);
                //Si todo sale bien configuro una mensaje
                mensaje = "Se ha enviado un mail confirmando tu registro en World Solution RRHH";
            }
            catch (Exception ex)
            {
                mensaje = "Lo sentimos, ha ocurrido un error al enviar el mail. Intenta nuevamente en unos minutos";
                string detalleError = ex.Message;   //guardo el error para ver que paso pero no lo muestro al usuario
                //mensaje = "ERROR: " + ex.Message;
            }
            mensaje = mensaje+1;//si quiero retornar el mensaje borrar esta linea y poner --> return mensaje;
        }
    }
}
