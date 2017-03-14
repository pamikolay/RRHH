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
            ViewBag.TablaBusquedas = retornarBusquedasActivas();
            return View();
        }
        public ActionResult AgregarNuevaBusqueda()
        {
            ViewBag.TablaCompany = retornarCompanys();
            return View();
        }

        [HttpPost]
        public ActionResult GuardarNuevaBusqueda(string job_name, string job_description, string empresaPuesto)
        {
            string s = System.Configuration.ConfigurationManager.ConnectionStrings["cadenaconexion1"].ConnectionString;
            SqlConnection conexion = new SqlConnection(s);
            conexion.Open();
            string queryInsertSql = "insert into Job (JobName,JobDate,JobDescription,CompanyID,JobStatusID) values('" +
              job_name + "','" + DateTime.Now.ToString("yyyy-MM-dd h:m:s") + "','" + job_description + "','" + int.Parse(empresaPuesto) + "','" + 2 + "')";
            SqlCommand comando = new SqlCommand(queryInsertSql,conexion);
            comando.ExecuteNonQuery();

            return RedirectToAction("Busquedas", "Jobs");
        }

        public ActionResult EditarBusqueda(int id_job, string job_name, string job_description)
        {
            string s = System.Configuration.ConfigurationManager.ConnectionStrings["cadenaconexion1"].ConnectionString;
            SqlConnection conexion = new SqlConnection(s);
            conexion.Open();
            SqlCommand comando = new SqlCommand("update Job set " +
                "JobName='" + job_name + "',JobDescription='" + job_description + "' where JobID='" + id_job + "'", conexion);
            comando.ExecuteNonQuery();
            conexion.Close();

            return RedirectToAction("Busquedas", "Jobs");
        }

        public List<Job> retornarBusquedasActivas()
        {
            string s = System.Configuration.ConfigurationManager.ConnectionStrings["cadenaconexion1"].ConnectionString;
            SqlConnection conexion = new SqlConnection(s);
            string querySql = "select * from Job";
            SqlDataAdapter adapter = new SqlDataAdapter(querySql, conexion);

            DataTable dt = new DataTable();
            adapter.Fill(dt);

            List<Job> listaTablaBusquedas = new List<Job>();
            foreach (DataRow row in dt.Rows)
            {
                if (Convert.ToString(row["JobStatusID"]) == "2")
                {
                    Job busquedasSql = new Job();
                    busquedasSql.JobID = Convert.ToInt32(row["JobID"]);
                    busquedasSql.JobName = Convert.ToString(row["JobName"]);
                    busquedasSql.JobDate = Convert.ToDateTime(row["JobDate"]);
                    busquedasSql.JobDescription = Convert.ToString(row["JobDescription"]);
                    listaTablaBusquedas.Add(busquedasSql);
                }
            }

            conexion.Close();

            return listaTablaBusquedas;
        }

        public List<Company> retornarCompanys()
        {
            string s = System.Configuration.ConfigurationManager.ConnectionStrings["cadenaconexion1"].ConnectionString;
            SqlConnection conexion = new SqlConnection(s);
            string queryCompany = "select [CompanyID],[CompanyName] from [Company]";
            SqlDataAdapter adapter = new SqlDataAdapter(queryCompany, conexion);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            List<Company> listaTablaCompany = new List<Company>();
            foreach (DataRow row in dt.Rows)
            {
                Company companySql = new Company();
                companySql.CompanyID = Convert.ToInt32(row["CompanyID"]);
                companySql.CompanyName = Convert.ToString(row["CompanyName"]);
                listaTablaCompany.Add(companySql);
            }
            conexion.Close();
            return listaTablaCompany;
        }
    }
}