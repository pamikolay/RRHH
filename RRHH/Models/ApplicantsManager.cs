﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RRHH.Models
{
    public class ApplicantsManager
    {
        public void Insertar(Applicants newApplicant)
        {
            string sqlquery = "INSERT INTO Applicants (Date,Postulant,Job,ApplicationStatus,InterviewStatus) VALUES (getdate(),@Postulant,@Job,1,1)";
            DataBase ConexionBD = new DataBase();
            SqlCommand sentencia = ConexionBD.Conectar(sqlquery);
            sentencia.Parameters.AddWithValue("@Postulant", newApplicant.Postulant.ID);
            sentencia.Parameters.AddWithValue("@Job", newApplicant.Job.ID);
            //5-Ejecutar!
            sentencia.ExecuteNonQuery();

            //CERRAR LA CONEXION AL TERMINAR!!!!
            ConexionBD.Desconectar();
        }
        public int CheckApplicant(Users user, Jobs job)
        {
            string sqlquery = "SELECT COUNT(*) FROM Applicants WHERE Postulant=@Postulant AND Job=@Job";
            DataBase ConexionBD = new DataBase();
            SqlCommand sentencia = ConexionBD.Conectar(sqlquery);
            sentencia.Parameters.AddWithValue("@Postulant", user.ID);
            sentencia.Parameters.AddWithValue("@Job", job.ID);

            Int32 a = (Int32)(sentencia.ExecuteScalar());

            //CERRAR LA CONEXION AL TERMINAR!!!!
            ConexionBD.Desconectar();
            return a;
        }
        public List<Applicants> ConsultarPorUser(Users user)
        {
            List<Applicants> applys = new List<Applicants>();

            string sqlquery = "SELECT * FROM Applicants WHERE Postulant=@Postulant";
            DataBase ConexionBD = new DataBase();
            SqlCommand sentencia = ConexionBD.Conectar(sqlquery);
            sentencia.Parameters.AddWithValue("@Postulant", user.ID);
            
            SqlDataReader reader= sentencia.ExecuteReader();
            while (reader.Read()) //mientras haya un registro para leer
            {
                //creo la busqueda y le completo los datos 
                Applicants apply = new Applicants();
                apply.ID = (int)reader["ID"];
                apply.Date = (DateTime)reader["Date"];
                apply.Job = new JobsManager().Consultar((int)reader["Job"]);
                apply.Postulant = new UsersManager().Consultar((int)reader["Postulant"]);
                apply.ApplicationStatus = new JobsApplicationsManager().Consultar((int)reader["ApplicationStatus"]);
                apply.InterviewStatus = new InterviewsManager().Consultar((int)reader["InterviewStatus"]);
                applys.Add(apply);
            }

            return applys;
        }
    }
}