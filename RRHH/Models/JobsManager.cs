using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RRHH.Models
{
    public class JobsManager
    {
        public void Insertar(Jobs newJob)
        {
            string sqlquery="insert into Jobs (Name,Date,Description,Company,Status) VALUES (@Name, getdate(), @Description, @Company, 2)";
            DataBase ConexionBD = new DataBase();
            SqlCommand sentencia = ConexionBD.Conectar(sqlquery);
            sentencia.Parameters.AddWithValue("@Name", newJob.Name);
            sentencia.Parameters.AddWithValue("@Description", newJob.Description);
            sentencia.Parameters.AddWithValue("@Company", newJob.Company.ID);
            //5-Ejecutar!
            sentencia.ExecuteNonQuery();

            //CERRAR LA CONEXION AL TERMINAR!!!!
            ConexionBD.Desconectar();
        }

        public void InactivarJob(Jobs job)
        {
            this.InactivarJob(job.ID);
        }

        public void InactivarJob(long ID)
        {
            string sqlquery = "UPDATE Jobs set Status = 1 where ID = @ID";
            DataBase ConexionBD = new DataBase();
            SqlCommand sentencia = ConexionBD.Conectar(sqlquery);
            sentencia.Parameters.AddWithValue("@ID", ID);
            //5-Ejecutar!
            sentencia.ExecuteNonQuery();

            //CERRAR LA CONEXION AL TERMINAR!!!!
            ConexionBD.Desconectar();
        }

        /// <summary>
        /// Modificamos todo un trabajo
        /// </summary>
        /// <param name="job"></param>
        public void Actualizar(Jobs job)
        {
            string sqlquery = "UPDATE Jobs set Name = @Name, Description = @Description, Company = @Company, Status = @Status where ID = @ID";
            //LA FECHA NO LA QUIERO MODIFICAR
            DataBase ConexionBD = new DataBase();
            SqlCommand sentencia = ConexionBD.Conectar(sqlquery);
            //4-escribrimos la sentencia
            sentencia.Parameters.AddWithValue("@Name", job.Name);
            sentencia.Parameters.AddWithValue("@Description", job.Description);
            sentencia.Parameters.AddWithValue("@Company", job.Company.ID);
            sentencia.Parameters.AddWithValue("@Status", job.Status.ID);
            sentencia.Parameters.AddWithValue("@ID", job.ID);
            //5-Ejecutar!
            sentencia.ExecuteNonQuery();

            //CERRAR LA CONEXION AL TERMINAR!!!!
            ConexionBD.Desconectar();
        }

        public List<Jobs> ConsultarTodos()
        {
            List<Jobs> jobs = new List<Jobs>();

            string sqlquery = "SELECT * FROM Jobs";
            DataBase ConexionBD = new DataBase();
            SqlCommand sentencia = ConexionBD.Conectar(sqlquery);

            SqlDataReader reader = sentencia.ExecuteReader();
            while (reader.Read()) //mientras haya un registro para leer
            {
                //creo el artículo, le completo los datos 
                Jobs job = new Jobs();
                job.ID = (int)reader["ID"];
                job.Date = (DateTime)reader["Date"];
                job.Name = (string)reader["Name"];
                job.Description = (string)reader["Description"];
                job.Company.ID=(int)(reader["Company"]);
                job.Status.ID = (int)(reader["Status"]);
                //AGREGO EL job A LA LISTA
                jobs.Add(job);
            }

            //CERRAR EL READER AL TERMINAR DE LEER LOS REGISTROS
            reader.Close();
            //CERRAR LA CONEXION AL TERMINAR!!!!
            ConexionBD.Desconectar();

            return jobs;
        }

        public List<Jobs> ConsultarActivas()
        {
            List<Jobs> jobs = new List<Jobs>();

            //las busquedas son ordenadas de por fecha de la mas nueva a la mas antigua
            string sqlquery =   "SELECT        Jobs.ID, Jobs.Name, Jobs.Date, Jobs.Description, Jobs.Company, Jobs.Status, " + 
                                "Companys.Name AS CompanyName " + 
                                "FROM   Companys " + 
                                "INNER JOIN Jobs ON Companys.ID = Jobs.Company " + 
                                "WHERE(Jobs.Status = 2)  ORDER BY Jobs.Date DESC";
            DataBase ConexionBD = new DataBase();
            SqlCommand sentencia = ConexionBD.Conectar(sqlquery);

            SqlDataReader reader = sentencia.ExecuteReader();
            while (reader.Read()) //mientras haya un registro para leer
            {
                //creo el artículo, le completo los datos 
                Jobs job = new Jobs();
                job.ID = (int)reader["ID"];
                job.Date = (DateTime)reader["Date"];
                job.Name = (string)reader["Name"];
                job.Description = (string)reader["Description"];
                job.Company = new Companys();
                job.Company.Name = (string)reader["CompanyName"];
                //agrego el job a la lista
                jobs.Add(job);
            }

            //CERRAR EL READER AL TERMINAR DE LEER LOS REGISTROS
            reader.Close();
            //CERRAR LA CONEXION AL TERMINAR!!!!
            ConexionBD.Desconectar();

            return jobs;
        }

        [Obsolete]
        public List<Jobs> ConsultarActivasViejo()
        {
            List<Jobs> jobs = new List<Jobs>();
            
            //las busquedas son ordenadas de por fecha de la mas nueva a la mas antigua
            string sqlquery = "SELECT * FROM Jobs WHERE Status = 2 ORDER BY Date DESC";
            DataBase ConexionBD = new DataBase();
            SqlCommand sentencia = ConexionBD.Conectar(sqlquery);

            CompanysManager cManager = new CompanysManager();     //para pasarle un objeto company necesito el CompanyManager
            
            SqlDataReader reader = sentencia.ExecuteReader();
            while (reader.Read()) //mientras haya un registro para leer
            {
                //creo el artículo, le completo los datos 
                Jobs job = new Jobs();
                job.ID = (int)reader["ID"];
                job.Date = (DateTime)reader["Date"];        
                job.Name = (string)reader["Name"];
                job.Description = (string)reader["Description"];
                job.Company = cManager.Consultar((int)reader["Company"]);
                //agrego el job a la lista
                jobs.Add(job);
            }

            //CERRAR EL READER AL TERMINAR DE LEER LOS REGISTROS
            reader.Close();
            //CERRAR LA CONEXION AL TERMINAR!!!!
            ConexionBD.Desconectar();

            return jobs;
        }

        public List<Jobs> ConsultarInactivas()
        {
            List<Jobs> jobs = new List<Jobs>();

            string sqlquery =   "SELECT        Jobs.ID, Jobs.Name, Jobs.Date, Jobs.Description, Jobs.Company, Jobs.Status, " + 
                                "Companys.Name AS CompanyName " + 
                                "FROM   Companys " + 
                                "INNER JOIN Jobs ON Companys.ID = Jobs.Company " + 
                                "WHERE(Jobs.Status = 1)  ORDER BY Jobs.Date DESC";
            DataBase ConexionBD = new DataBase();
            SqlCommand sentencia = ConexionBD.Conectar(sqlquery);

            CompanysManager cManager = new CompanysManager();     //para pasarle un objeto company necesito el CompanyManager

            SqlDataReader reader = sentencia.ExecuteReader();
            while (reader.Read()) //mientras haya un registro para leer
            {
                //creo el artículo, le completo los datos 
                Jobs job = new Jobs();
                job.ID = (int)reader["ID"];
                job.Date = (DateTime)reader["Date"];
                job.Name = (string)reader["Name"];
                job.Description = (string)reader["Description"];
                job.Company = new Companys();
                job.Company.Name = (string)reader["CompanyName"];
                //agrego el job a la lista
                jobs.Add(job);
            }

            //CERRAR EL READER AL TERMINAR DE LEER LOS REGISTROS
            reader.Close();
            //CERRAR LA CONEXION AL TERMINAR!!!!
            ConexionBD.Desconectar();

            return jobs;
        }

        public Jobs Consultar(int ID)
        {
            string sqlquery =   "SELECT        Jobs.*, Companys.ID AS CompanyID, Companys.Name AS CompanyName, " + 
                                "JobStatuses.ID AS JobStatusID, JobStatuses.Details AS JobStatusDetails " + 
                                "FROM            dbo.Companys INNER JOIN " + 
                                "Jobs ON Companys.ID = Jobs.Company INNER JOIN " + 
                                "JobStatuses ON Jobs.Status = JobStatuses.ID " + 
                                "WHERE Jobs.ID = @ID";
            DataBase ConexionBD = new DataBase();
            SqlCommand sentencia = ConexionBD.Conectar(sqlquery);
            sentencia.Parameters.AddWithValue("@ID", ID);

            Jobs job = new Jobs();
            SqlDataReader reader = sentencia.ExecuteReader();
            while (reader.Read()) //mientras haya un registro para leer
            {
                //creo el artículo, le completo los datos 
                job.ID = (int)reader["ID"];
                job.Date = (DateTime)reader["Date"];    
                job.Name = (string)reader["Name"];
                job.Description = (string)reader["Description"];
                job.Company = new Companys();
                job.Company.ID = (int)reader["CompanyID"];
                job.Company.Name = (string)reader["CompanyName"];
                job.Status = new JobStatuses();
                job.Status.ID= (int)reader["JobStatusID"];
                job.Status.Details = (string)reader["JobStatusDetails"];

            }

            //CERRAR EL READER AL TERMINAR DE LEER LOS REGISTROS
            reader.Close();
            //CERRAR LA CONEXION AL TERMINAR!!!!
            ConexionBD.Desconectar();

            return job;
        }

        /// <summary>
        /// Trae los puestos publicados por la compañia pasada por parámetro
        /// </summary>
        /// <param name="copmany"></param>
        /// <returns></returns>
        public List<Jobs> Consultar(Companys company)
        {
            return new List<Jobs>();
        }
    }
}