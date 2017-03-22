using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RRHH.Models
{
    public class InterviewsManager
    {
        public List<Interviews> ConsultarTodos()
        {
            List<Interviews> interviews = new List<Interviews>();

            string sqlquery = "select * from Interviews";
            DataBase ConexionBD = new DataBase();
            SqlCommand sentencia = ConexionBD.Conectar(sqlquery);

            SqlDataReader reader = sentencia.ExecuteReader();
            while (reader.Read()) //mientras haya un registro para leer
            {
                //creo el artículo, le completo los datos 
                Interviews interview = new Interviews();
                interview.ID = (int)reader["ID"];
                interview.Status = (string)reader["Status"];
                //AGREGO LA company A LA LISTA
                interviews.Add(interview);
            }

            //CERRAR EL READER AL TERMINAR DE LEER LOS REGISTROS
            reader.Close();
            //CERRAR LA CONEXION AL TERMINAR!!!!
            ConexionBD.Desconectar();

            return interviews;
        }

        public Interviews Consultar(int ID)
        {
            string sqlquery = "select * from Interviews WHERE ID=@ID";
            DataBase ConexionBD = new DataBase();
            SqlCommand sentencia = ConexionBD.Conectar(sqlquery);
            sentencia.Parameters.AddWithValue("@ID", ID);

            Interviews interview = new Interviews();
            SqlDataReader reader = sentencia.ExecuteReader();
            while (reader.Read()) //mientras haya un registro para leer
            {
                //creo el artículo, le completo los datos 
                interview.ID = (int)reader["ID"];
                interview.Status = (string)reader["Status"];
            }

            //CERRAR EL READER AL TERMINAR DE LEER LOS REGISTROS
            reader.Close();
            //CERRAR LA CONEXION AL TERMINAR!!!!
            ConexionBD.Desconectar();

            return interview;
        }
    }
}