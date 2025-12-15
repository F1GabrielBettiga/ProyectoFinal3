using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class AccesoDatos
    {
        private SqlDataReader lector;
        public SqlConnection conexion { get; }
        private SqlCommand comando;

        public SqlDataReader Lector
        {
            get { return lector; }
        }
        public AccesoDatos()
        {
            conexion = new SqlConnection("workstation id=CATALOGO_BETTIGA_WEB_DB.mssql.somee.com;packet size=4096;user id=GabrielBettiga_SQLLogin_1;pwd=ni15tbt2sy;data source=CATALOGO_BETTIGA_WEB_DB.mssql.somee.com;persist security info=False;initial catalog=CATALOGO_BETTIGA_WEB_DB;TrustServerCertificate=True");
            comando = new SqlCommand();
            comando.Connection = conexion;
        }

        public void setearConsulta(string consulta)
        {
            comando.Parameters.Clear();
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;


        }

        public void agregarParametro(string nombre, object valor)
        {
            comando.Parameters.AddWithValue(nombre, valor);
        }

        

        public void ejecutarLectura()
        {
            
            try
            {
                conexion.Open();
                lector = comando.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ejecutarAccion()
        {
            try
            {
                
                conexion.Open();
               return comando.ExecuteNonQuery(); // Retorna el número de filas afectadas
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void cerrarConexion()
        {
            try
            {
                // Cerrar el lector si está abierto
                if (lector != null && !lector.IsClosed)
                    lector.Close();

                // Cerrar la conexión si existe y está abierta
                if (conexion != null && conexion.State == System.Data.ConnectionState.Open)
                    conexion.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}
