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
                    aux.nombre = datos.Lector["nombre"] != DBNull.Value ? (string)datos.Lector["nombre"] : "";
                    aux.apellido = datos.Lector["apellido"] != DBNull.Value ? (string)datos.Lector["apellido"] : "";
                    aux.email = datos.Lector["email"] != DBNull.Value ? (string)datos.Lector["email"] : "";
                    aux.password = datos.Lector["pass"] != DBNull.Value ? (string)datos.Lector["pass"] : "";
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
        public bool obtenerUsuarioPorEmail(string email, string password, out Usuario usuario)
        {
            usuario = null;
            AccesoDatos.AccesoDatos datos = new AccesoDatos.AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT Id, nombre, apellido, email, pass, urlImagenPerfil, admin FROM USERS WHERE email = @Email and pass = @password");
                datos.agregarParametro("@Email", email);
                datos.agregarParametro("@password", password);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    usuario = new Usuario();
                    usuario.id = (int)datos.Lector["Id"];
                    usuario.nombre = datos.Lector["nombre"] != DBNull.Value ? (string)datos.Lector["nombre"] : "";
                    usuario.apellido = datos.Lector["apellido"] != DBNull.Value ? (string)datos.Lector["apellido"] : "";
                    usuario.email = datos.Lector["email"] != DBNull.Value ? (string)datos.Lector["email"] : "";
                    usuario.password = datos.Lector["pass"] != DBNull.Value ? (string)datos.Lector["pass"] : "";
                    usuario.urlImagenPerfil = datos.Lector["urlImagenPerfil"] != DBNull.Value ? (string)datos.Lector["urlImagenPerfil"] : null;
                    usuario.esAdmin = datos.Lector["admin"] != DBNull.Value && (bool)datos.Lector["admin"];

                    return true;
                }

                return false;

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
        public bool insertarUsuario(Usuario nuevo)
        {
            AccesoDatos.AccesoDatos datos = new AccesoDatos.AccesoDatos();

            // Función local para mapear string vacío → NULL en DB
            object ValorONull(string valor)
            {
                return string.IsNullOrWhiteSpace(valor)
                    ? (object)DBNull.Value
                    : valor.Trim();
            }

            try
            {
                datos.setearConsulta(
                    "INSERT INTO USERS (nombre, apellido, email, pass, urlImagenPerfil, admin) " +
                    "VALUES (@nombre, @apellido, @email, @pass, @url, @admin)"
                );

                // Strings que pueden venir null o vacíos
                datos.agregarParametro("@nombre", ValorONull(nuevo.nombre));
                datos.agregarParametro("@apellido", ValorONull(nuevo.apellido));
                datos.agregarParametro("@email", ValorONull(nuevo.email));
                datos.agregarParametro("@pass", ValorONull(nuevo.password));

                //  Imagen opcional (puede venir null)
                datos.agregarParametro("@url", ValorONull(nuevo.urlImagenPerfil));
                datos.agregarParametro("@admin", nuevo.esAdmin);

                int filas = datos.ejecutarAccion();

                return filas > 0;
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
        public bool actualizarUsuario (Usuario usuario)
        {
            AccesoDatos.AccesoDatos datos = new AccesoDatos.AccesoDatos();

            // Función local para mapear string -> valor o NULL
            object ValorONull(string valor)
            {
                return string.IsNullOrWhiteSpace(valor)
                    ? (object)DBNull.Value
                    : valor;
            }


            try
            {
                //"urlImagenPerfil = @UrlImagenPerfil " 
                datos.setearConsulta(
                    "UPDATE USERS SET " +
                    "email = @Email, " +
                    "nombre = @Nombre, " +
                    "apellido = @Apellido, " +
                    "pass = @Password, " +
                    "urlImagenPerfil = @UrlImagenPerfil ,"+
                    "admin = @Admin " +

                    "WHERE Id = @Id"
                );

                // Strings: si vienen null o vacíos -> DB NULL
                datos.agregarParametro("@Email", ValorONull(usuario.email));
                datos.agregarParametro("@Nombre", ValorONull(usuario.nombre));
                datos.agregarParametro("@Apellido", ValorONull(usuario.apellido));
                datos.agregarParametro("@Password", ValorONull(usuario.password));
                datos.agregarParametro("@UrlImagenPerfil", ValorONull(usuario.urlImagenPerfil));
                datos.agregarParametro("@Admin", usuario.esAdmin);
                
                datos.agregarParametro("@Id", usuario.id);

                int filas = datos.ejecutarAccion();

                if (filas > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
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
        public int obtenerProximoId()
        {
            AccesoDatos.AccesoDatos datos = new AccesoDatos.AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT ISNULL(MAX(Id), 0) + 1 FROM USERS");
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                    return (int)datos.Lector[0];

                return 1; 
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
        public bool ExisteEmail(string email)
        {
            string emailNormalizado = (email ?? string.Empty).Trim().Replace(" ", "").ToLower();

            AccesoDatos.AccesoDatos datos = new AccesoDatos.AccesoDatos();

            try
            {
                datos.setearConsulta(
                    "SELECT COUNT(*) " +
                    "FROM USERS " +
                    "WHERE LOWER(REPLACE(Email, ' ', '')) = @Email"
                );

                datos.agregarParametro("@Email", emailNormalizado);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    int cantidad = (int)datos.Lector[0];
                    if (cantidad > 0)
                        return true;
                }

                return false;
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
        public bool eliminarUsuario(int id)
        {
            AccesoDatos.AccesoDatos datos = new AccesoDatos.AccesoDatos();
            try
            {
                datos.setearConsulta("DELETE FROM USERS WHERE Id = @Id");
                datos.agregarParametro("@Id", id);
                int filas = datos.ejecutarAccion();


                if (filas > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
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

        public List<Usuario> BuscarUsuariosPorTexto(string texto)
        {
            List<Usuario> lista = new List<Usuario>();
            AccesoDatos.AccesoDatos datos = new AccesoDatos.AccesoDatos();

            try
            {
                // Si no hay texto → devuelvo vacío
                if (string.IsNullOrWhiteSpace(texto))
                    return new List<Usuario>();

                string filtro = "%" + texto.Trim() + "%";

                string query = @"
        SELECT  Id,
                Email,
                Pass,
                Nombre,
                Apellido,
                UrlImagenPerfil,
                Admin
        FROM USERS
        WHERE
               Email           LIKE @filtro
            OR Pass            LIKE @filtro
            OR Nombre          LIKE @filtro
            OR Apellido        LIKE @filtro
            OR UrlImagenPerfil LIKE @filtro
            OR CONVERT(VARCHAR(10), Admin) LIKE @filtro
        ORDER BY Nombre ASC";

                datos.setearConsulta(query);
                datos.agregarParametro("@filtro", filtro);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Usuario aux = new Usuario();

                    aux.id = (int)datos.Lector["Id"];
                    aux.email = (string)datos.Lector["Email"];
                    aux.password = (string)datos.Lector["Pass"];

                    aux.nombre = datos.Lector["Nombre"] != DBNull.Value
                        ? (string)datos.Lector["Nombre"]
                        : "";

                    aux.apellido = datos.Lector["Apellido"] != DBNull.Value
                        ? (string)datos.Lector["Apellido"]
                        : "";

                    aux.urlImagenPerfil = datos.Lector["UrlImagenPerfil"] != DBNull.Value
                        ? (string)datos.Lector["UrlImagenPerfil"]
                        : "";

                    aux.esAdmin = (bool)datos.Lector["Admin"];

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
