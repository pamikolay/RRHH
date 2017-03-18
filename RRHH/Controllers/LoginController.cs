using RRHH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RRHH.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Registro()
        {
            ProvinceManager pManager = new ProvinceManager();
            ViewBag.Provinces = pManager.ConsultarTodas();
            return View();
        }
        [HttpPost]
        public ActionResult GetCiudades(int id)
        {
            List<City> ciudades = new List<City>();
            ciudades = new CityManager().GetCiudadesPorProvincia(id);
            return Json(new SelectList(ciudades, "CityID", "CityName"));
        }
        [HttpPost]
        public ActionResult GuardarNuevoUsuario(string first_name, string last_name, string password, string email, string phone, string address, int province_id, int city_id, string genre)
        {
            UserTable newUser = new UserTable();
            newUser.UserTableFirstName = first_name;
            newUser.UserTableLastName = last_name;
            newUser.UserTablePassword = password;
            newUser.UserTableEmail = email;
            newUser.UserTablePhone = phone;
            newUser.UserTableAddress = address;
            newUser.UserTableGenre = genre;
            ProvinceManager pManager = new ProvinceManager();
            newUser.Province = pManager.Consultar(province_id);
            CityManager cManager = new CityManager();
            newUser.City = cManager.Consultar(city_id);

            UserTableManager uManager = new UserTableManager();
            uManager.Insertar(newUser);

            return RedirectToAction("Index", "Home");
        }
    }
}