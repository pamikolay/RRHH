using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RRHH.Models
{
    public class CompanyManager
    {
        public void Insertar(Company newCompany)
        {
            string sqlquery = "insert into Company (CompanyName) VALUES (@CompanyName)";
            DataBase ConexionBD = new DataBase();
            SqlCommand sentencia = ConexionBD.Conectar(sqlquery);
            sentencia.Parameters.AddWithValue("@CompanyName", newCompany.CompanyName);
            //5-Ejecutar!
            sentencia.ExecuteNonQuery();

            //CERRAR LA CONEXION AL TERMINAR!!!!
            ConexionBD.Desconectar();
        }

        public void Eliminar(Company company)
        {
            this.Eliminar(company.CompanyID);
        }

        public void Eliminar(long ID)
        {
            string sqlquery = "delete from Articulos where Id = @Id";
            DataBase ConexionBD = new DataBase();
            SqlCommand sentencia = ConexionBD.Conectar(sqlquery);
            sentencia.Parameters.AddWithValue("@Id", ID);
            //5-Ejecutar!
            sentencia.ExecuteNonQuery();

            //CERRAR LA CONEXION AL TERMINAR!!!!
            ConexionBD.Desconectar();
        }

        /// <summary>
        /// Modificamos una compañia
        /// </summary>
        /// <param name="company"></param>
        public void Actualizar(Company company)
        {
            string sqlquery = "update Company set CompanyName = @CompanyName WHERE CompanyID = @CompanyID";
            DataBase ConexionBD = new DataBase();
            SqlCommand sentencia = ConexionBD.Conectar(sqlquery);

            sentencia.Parameters.AddWithValue("@CompanyName", company.CompanyName);
            sentencia.Parameters.AddWithValue("@CompanyID", company.CompanyID);
            //5-Ejecutar!
            sentencia.ExecuteNonQuery();

            //CERRAR LA CONEXION AL TERMINAR!!!!
            ConexionBD.Desconectar();
        }

        public List<Company> ConsultarTodos()
        {
            List<Company> companys = new List<Company>();

            string sqlquery = "select * from Company";
            DataBase ConexionBD = new DataBase();
            SqlCommand sentencia = ConexionBD.Conectar(sqlquery);

            SqlDataReader reader = sentencia.ExecuteReader();
            while (reader.Read()) //mientras haya un registro para leer
            {
                //creo el artículo, le completo los datos 
                Company company = new Company();
                company.CompanyID = (int)reader["CompanyID"];
                company.CompanyName = (string)reader["CompanyName"];
                //AGREGO LA company A LA LISTA
                companys.Add(company);
            }

            //CERRAR EL READER AL TERMINAR DE LEER LOS REGISTROS
            reader.Close();
            //CERRAR LA CONEXION AL TERMINAR!!!!
            ConexionBD.Desconectar();

            return companys;
        }

        public Company Consultar(int ID)
        {
            string sqlquery = "select * from Company WHERE CompanyID=@CompanyID";
            DataBase ConexionBD = new DataBase();
            SqlCommand sentencia = ConexionBD.Conectar(sqlquery);
            sentencia.Parameters.AddWithValue("@CompanyID", ID);

            Company company = new Company();
            SqlDataReader reader = sentencia.ExecuteReader();
            while (reader.Read()) //mientras haya un registro para leer
            {
                //creo el artículo, le completo los datos 
                company.CompanyID = (int)reader["CompanyID"];
                company.CompanyName = (string)reader["CompanyName"];
            }

            //CERRAR EL READER AL TERMINAR DE LEER LOS REGISTROS
            reader.Close();
            //CERRAR LA CONEXION AL TERMINAR!!!!
            ConexionBD.Desconectar();

            return company;
        }

    }
}