using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RRHH.Models
{
    public class ProvincesManager
    {
        public List<Provinces> ConsultarTodas()
        {
            List<Provinces> provinces = new List<Provinces>();

            string sqlquery = "select * from Provinces";
            DataBase ConexionBD = new DataBase();
            SqlCommand sentencia = ConexionBD.Conectar(sqlquery);

            SqlDataReader reader = sentencia.ExecuteReader();
            while (reader.Read()) //mientras haya un registro para leer
            {
                //creo la provincia y le completo los datos 
                Provinces province = new Provinces();
                province.ID = (int)reader["ID"];
                province.Name = (string)reader["Name"];
                //AGREGO LA company A LA LISTA
                provinces.Add(province);
            }

            //CERRAR EL READER AL TERMINAR DE LEER LOS REGISTROS
            reader.Close();
            //CERRAR LA CONEXION AL TERMINAR!!!!
            ConexionBD.Desconectar();

            return provinces;
        }

        public Provinces Consultar(int ID)
        {
            string sqlquery = "select * from Provinces WHERE ID=@ID";
            DataBase ConexionBD = new DataBase();
            SqlCommand sentencia = ConexionBD.Conectar(sqlquery);
            sentencia.Parameters.AddWithValue("@ID", ID);

            Provinces province = new Provinces();
            SqlDataReader reader = sentencia.ExecuteReader();
            while (reader.Read()) //mientras haya un registro para leer
            {
                //creo la provincia y le completo los datos 
                province.ID = (int)reader["ID"];
                province.Name = (string)reader["Name"];
            }

            //CERRAR EL READER AL TERMINAR DE LEER LOS REGISTROS
            reader.Close();
            //CERRAR LA CONEXION AL TERMINAR!!!!
            ConexionBD.Desconectar();

            return province;
        }
    }
}