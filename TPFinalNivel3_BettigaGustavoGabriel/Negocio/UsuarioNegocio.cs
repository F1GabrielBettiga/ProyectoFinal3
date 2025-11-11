using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class UsuarioNegocio
    {
        public List<Usuario> listarUsuarios()
        {
            List<Usuario> lista = new List<Usuario>();
            AccesoDatos.AccesoDatos datos = new AccesoDatos.AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT Id, nombre, apellido, email, pass, urlImagenPerfil, admin FROM USERS");

                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Usuario aux = new Usuario();
                    aux.id = (int)datos.Lector["Id"];
                    aux.nombre = datos.Lector["nombre"] != DBNull.Value ? (string)datos.Lector["nombre"] : "-";
                    aux.apellido = datos.Lector["apellido"] != DBNull.Value ? (string)datos.Lector["apellido"] : "-";
                    aux.email = datos.Lector["email"] != DBNull.Value ? (string)datos.Lector["email"] : "-";
                    aux.password = datos.Lector["pass"] != DBNull.Value ? (string)datos.Lector["pass"] : "-";
                    aux.urlImagenPerfil = datos.Lector["urlImagenPerfil"] != DBNull.Value ? (string)datos.Lector["urlImagenPerfil"] : null;
                    aux.esAdmin = datos.Lector["admin"] != DBNull.Value && (bool)datos.Lector["admin"];

                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }





    }
}
