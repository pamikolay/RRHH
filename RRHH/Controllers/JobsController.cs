using RRHH.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RRHH.Controllers
{
    public class JobsController : Controller
    {
        // GET: Jobs
        public ActionResult Busquedas()
        {
            JobsManager jManager = new JobsManager();
            ViewBag.TablaBusquedas = jManager.ConsultarActivas();
            return View();
        }        
    }
}