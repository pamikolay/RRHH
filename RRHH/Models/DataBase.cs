using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RRHH.Models
{
    public class DataBase
    {
        private SqlConnection conexion = new SqlConnection("Server=CPX-LF4YH71NNSV\\SQLEXPRESS;Database=RRHH;Trusted_Connection=True;");
        private SqlCommand sentencia;

        public SqlCommand Conectar(string sqlquery)
        {
            conexion.Open();
            sentencia = conexion.CreateCommand();
            sentencia.CommandText = sqlquery;
            return sentencia;
        }

        public void Desconectar()
        {
            conexion.Close();
        }
    }
}