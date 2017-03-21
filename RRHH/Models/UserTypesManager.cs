using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RRHH.Models
{
    public class UserTypesManager
    {
        public UserTypes Consultar(int ID)
        {
            string sqlquery = "select * from UserTypes WHERE ID=@ID";
            DataBase ConexionBD = new DataBase();
            SqlCommand sentencia = ConexionBD.Conectar(sqlquery);
            sentencia.Parameters.AddWithValue("@ID", ID);

            UserTypes userType = new UserTypes();
            SqlDataReader reader = sentencia.ExecuteReader();
            if (reader.Read()) //mientras haya un registro para leer
            {
                //creo el artículo, le completo los datos 
                userType.ID = (int)reader["ID"];
                userType.Name = (string)reader["Name"];
            }
            else
            {
                userType = null;
            }

            //CERRAR EL READER AL TERMINAR DE LEER LOS REGISTROS
            reader.Close();
            //CERRAR LA CONEXION AL TERMINAR!!!!
            ConexionBD.Desconectar();

            return userType;
        }
    }
}