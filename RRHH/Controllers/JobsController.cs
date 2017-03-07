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
            conexion.Open();
            SqlCommand comando = new SqlCommand("select * from Job", conexion);


            //PRUEBA
            string querySql = "select * from dbo.Job";
            SqlDataAdapter adapter = new SqlDataAdapter(querySql, conexion);

            DataSet ds = new DataSet();
            adapter.Fill(ds, "Job");

            DataTable dt = new DataTable("Job");
            adapter.Fill(dt);

            /// VER TODAS LAS FILAS DE LA TABLA Job
            /// JobID
            //dt.Rows[0][0]
            //JobName
            //ds.Tables[0].Rows[0][1]
            //JobDate
            //dt.Rows[0][2]
            //JobDescription
            //dt.Rows[0][3]
            //CompanyID
            //dt.Rows[0][4]
            //JobStatusID
            //dt.Rows[0][5]

            if (dt.Rows.Count>0)
            {
                DataRow row = dt.Rows[0];
                string nombreTrabajo = Convert.ToString(row["JobName"]);
                string descripcionTrabajo = Convert.ToString(row["JobDescription"]);
                string fechaTrabajo = Convert.ToString(row["JobDate"]);

            }
            

            //FIN PRUEBA

            //SqlDataReader registro = comando.ExecuteReader();
            //if (registro.Read())
            //{
            //    ViewBag.n = registro["JobName"];
            //    ViewBag.c = registro["JobDate"];
            //    ViewBag.e = registro["JobDescription"];
            //}
            //else
            //    ViewBag.msj = "No existe un usuario con dicho nombre";
            conexion.Close();
            List<Job> busquedas = (List<Job>)Session["Busquedas"];
            ViewBag.Busquedas = busquedas;
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