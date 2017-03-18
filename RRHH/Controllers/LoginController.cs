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
    }
}