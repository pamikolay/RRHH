using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RRHH.Models
{
    public class ProvinceManager
    {
        public List<Province> ConsultarTodas()
        {
            List<Province> provinces = new List<Province>();

            string sqlquery = "select * from Province";
            DataBase ConexionBD = new DataBase();
            SqlCommand sentencia = ConexionBD.Conectar(sqlquery);

            SqlDataReader reader = sentencia.ExecuteReader();
            while (reader.Read()) //mientras haya un registro para leer
            {
                //creo la provincia y le completo los datos 
                Province province = new Province();
                province.ProvinceID = (int)reader["ProvinceID"];
                province.ProvinceName = (string)reader["ProvinceName"];
                //AGREGO LA company A LA LISTA
                provinces.Add(province);
            }

            //CERRAR EL READER AL TERMINAR DE LEER LOS REGISTROS
            reader.Close();
            //CERRAR LA CONEXION AL TERMINAR!!!!
            ConexionBD.Desconectar();

            return provinces;
        }

        public Province Consultar(int ID)
        {
            string sqlquery = "select * from Province WHERE ProvinceID=@ProvinceID";
            DataBase ConexionBD = new DataBase();
            SqlCommand sentencia = ConexionBD.Conectar(sqlquery);
            sentencia.Parameters.AddWithValue("@ProvinceID", ID);

            Province province = new Province();
            SqlDataReader reader = sentencia.ExecuteReader();
            while (reader.Read()) //mientras haya un registro para leer
            {
                //creo la provincia y le completo los datos 
                province.ProvinceID = (int)reader["ProvinceID"];
                province.ProvinceName = (string)reader["ProvinceName"];
            }

            //CERRAR EL READER AL TERMINAR DE LEER LOS REGISTROS
            reader.Close();
            //CERRAR LA CONEXION AL TERMINAR!!!!
            ConexionBD.Desconectar();

            return province;
        }
    }
}