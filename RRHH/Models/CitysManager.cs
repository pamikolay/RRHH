using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RRHH.Models
{
    public class CitysManager
    {
        public List<Citys> ConsultarTodas()
        {
            List<Citys> citys = new List<Citys>();

            string sqlquery = "select * from Citys";
            DataBase ConexionBD = new DataBase();
            SqlCommand sentencia = ConexionBD.Conectar(sqlquery);

            SqlDataReader reader = sentencia.ExecuteReader();
            while (reader.Read()) //mientras haya un registro para leer
            {
                //creo la provincia y le completo los datos 
                Citys city = new Citys();
                city.ID = (int)reader["ID"];
                city.Name = (string)reader["Name"];
                //AGREGO LA company A LA LISTA
                citys.Add(city);
            }

            //CERRAR EL READER AL TERMINAR DE LEER LOS REGISTROS
            reader.Close();
            //CERRAR LA CONEXION AL TERMINAR!!!!
            ConexionBD.Desconectar();

            return citys;
        }

        public Citys Consultar(int ID)
        {
            string sqlquery = "SELECT * from Citys WHERE ID=@ID";
            DataBase ConexionBD = new DataBase();
            SqlCommand sentencia = ConexionBD.Conectar(sqlquery);
            sentencia.Parameters.AddWithValue("@ID", ID);

            Citys city = new Citys();
            SqlDataReader reader = sentencia.ExecuteReader();
            while (reader.Read()) //mientras haya un registro para leer
            {
                //creo la provincia y le completo los datos 
                city.ID = (int)reader["ID"];
                city.Name = (string)reader["Name"];
            }

            //CERRAR EL READER AL TERMINAR DE LEER LOS REGISTROS
            reader.Close();
            //CERRAR LA CONEXION AL TERMINAR!!!!
            ConexionBD.Desconectar();

            return city;
        }
        public List<Citys> GetCiudadesPorProvincia(int ID)
        {
            List<Citys> citys = new List<Citys>();

            string sqlquery = "select ID, Name from Citys WHERE Province=@Province";
            DataBase ConexionBD = new DataBase();
            SqlCommand sentencia = ConexionBD.Conectar(sqlquery);
            sentencia.Parameters.AddWithValue("@Province", ID);

            SqlDataReader reader = sentencia.ExecuteReader();
            while (reader.Read()) //mientras haya un registro para leer
            {
                //creo la provincia y le completo los datos 
                Citys city = new Citys();
                city.ID = (int)reader["ID"];
                city.Name = (string)reader["Name"];
                //AGREGO LA company A LA LISTA
                citys.Add(city);
            }

            //CERRAR EL READER AL TERMINAR DE LEER LOS REGISTROS
            reader.Close();
            //CERRAR LA CONEXION AL TERMINAR!!!!
            ConexionBD.Desconectar();

            return citys;
        }
    }
}