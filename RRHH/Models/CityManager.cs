using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RRHH.Models
{
    public class CityManager
    {
        public List<City> ConsultarTodas()
        {
            List<City> citys = new List<City>();

            string sqlquery = "select * from City";
            DataBase ConexionBD = new DataBase();
            SqlCommand sentencia = ConexionBD.Conectar(sqlquery);

            SqlDataReader reader = sentencia.ExecuteReader();
            while (reader.Read()) //mientras haya un registro para leer
            {
                //creo la provincia y le completo los datos 
                City city = new City();
                city.CityID = (int)reader["CityID"];
                city.CityName = (string)reader["CityName"];
                //AGREGO LA company A LA LISTA
                citys.Add(city);
            }

            //CERRAR EL READER AL TERMINAR DE LEER LOS REGISTROS
            reader.Close();
            //CERRAR LA CONEXION AL TERMINAR!!!!
            ConexionBD.Desconectar();

            return citys;
        }

        public City Consultar(int ID)
        {
            string sqlquery = "SELECT * from City WHERE CityID=@CityID";
            DataBase ConexionBD = new DataBase();
            SqlCommand sentencia = ConexionBD.Conectar(sqlquery);
            sentencia.Parameters.AddWithValue("@CityID", ID);

            City city = new City();
            SqlDataReader reader = sentencia.ExecuteReader();
            while (reader.Read()) //mientras haya un registro para leer
            {
                //creo la provincia y le completo los datos 
                city.CityID = (int)reader["CityID"];
                city.CityName = (string)reader["CityName"];
            }

            //CERRAR EL READER AL TERMINAR DE LEER LOS REGISTROS
            reader.Close();
            //CERRAR LA CONEXION AL TERMINAR!!!!
            ConexionBD.Desconectar();

            return city;
        }
        public List<City> GetCiudadesPorProvincia(int ID)
        {
            List<City> citys = new List<City>();

            string sqlquery = "select CityID, CityName from City WHERE ProvinceID=@ProvinceID";
            DataBase ConexionBD = new DataBase();
            SqlCommand sentencia = ConexionBD.Conectar(sqlquery);
            sentencia.Parameters.AddWithValue("@ProvinceID", ID);

            SqlDataReader reader = sentencia.ExecuteReader();
            while (reader.Read()) //mientras haya un registro para leer
            {
                //creo la provincia y le completo los datos 
                City city = new City();
                city.CityID = (int)reader["CityID"];
                city.CityName = (string)reader["CityName"];
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