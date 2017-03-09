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
        public ActionResult JobsIndex()
        {
            List<Job> busquedas = (List<Job>)Session["Busquedas"];
            ViewBag.Busquedas = busquedas;
            return View();
        }
        public ActionResult JobsIndexPrueba()
        {
            string s = System.Configuration.ConfigurationManager.ConnectionStrings["cadenaconexion1"].ConnectionString;
            SqlConnection conexion = new SqlConnection(s);
            string querySql = "select * from Job";
            SqlDataAdapter adapter = new SqlDataAdapter(querySql, conexion);

            DataTable dt = new DataTable();
            adapter.Fill(dt);

            ViewBag.TablaBusquedas = dt.Rows;

            List<Job> listaTablaBusquedas = new List<Job>();
            foreach (DataRow row in dt.Rows)
            {
                Job busquedasSql = new Job();
                busquedasSql.JobName = Convert.ToString(row["JobName"]);
                //busquedasSql.JobDate = Convert.ToDateTime(row["JobDate"]);
                busquedasSql.JobDescription = Convert.ToString(row["JobDescription"]);
                listaTablaBusquedas.Add(busquedasSql);
            }
            
            conexion.Close();
            ViewBag.TablaBusquedas = listaTablaBusquedas;
            return View();
        }
        [HttpPost]
        public ActionResult GuardarBusqueda(string job_name, string job_description)
        {
            Job nuevaBusqueda = new Job();
            nuevaBusqueda.JobName = job_name;
            nuevaBusqueda.JobDate = DateTime.Now;
            nuevaBusqueda.JobDescription = job_description;
            
            List<Job> listaBusquedas = (List<Job>)Session["Busquedas"];
            if (listaBusquedas == null) //verifico si existe la lista de articulos
            {
                //si no existe la creo
                listaBusquedas = new List<Job>();
            }
            listaBusquedas.Add(nuevaBusqueda);
            Session["Busquedas"] = listaBusquedas;

            return RedirectToAction("JobsIndex", "Jobs");
        }
    }
}