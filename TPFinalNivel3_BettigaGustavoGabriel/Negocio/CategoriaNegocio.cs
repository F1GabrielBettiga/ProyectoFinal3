using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class CategoriaNegocio
    {

        public List<Categoria> listarCategorias()
        {
            List<Categoria> lista = new List<Categoria>();
            AccesoDatos.AccesoDatos datos = new AccesoDatos.AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT Id, Descripcion FROM CATEGORIAS");

                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Categoria aux = new Categoria();
                    aux.id = (int)datos.Lector["Id"];
                    aux.descripcion = datos.Lector["Descripcion"] != DBNull.Value ? (string)datos.Lector["Descripcion"] : "-";

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

        public bool actualizarCategoria(Categoria cat)
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

                datos.setearConsulta(
                    "UPDATE CATEGORIAS SET " +
                    "Descripcion = @Descripcion " +
                    "WHERE Id = @Id"
                );

                // Strings: si vienen null o vacíos -> DB NULL
                datos.agregarParametro("@Descripcion", ValorONull(cat.descripcion));

                datos.agregarParametro("@Id", cat.id);

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

        public bool agregarCategoria(Categoria cat)
        {
            AccesoDatos.AccesoDatos datos = new AccesoDatos.AccesoDatos();

            // Función local para mapear string -> valor o NULL
            object ValorONull(string valor)
            {
                return string.IsNullOrWhiteSpace(valor)? (object)DBNull.Value: valor;
            }


            try
            {

                datos.setearConsulta("INSERT INTO CATEGORIAS (Descripcion)" +
                                      "VALUES (@Descripcion);");

                // Strings: si vienen null o vacíos -> DB NULL
                datos.agregarParametro("@Descripcion", ValorONull(cat.descripcion));

               

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

        public bool eliminarCategoria(int id)
        {
            AccesoDatos.AccesoDatos datos = new AccesoDatos.AccesoDatos();
            try
            {
                datos.setearConsulta("DELETE FROM CATEGORIAS WHERE Id = @Id");
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
        public bool ExisteDescripcion(string descripcion)
        {
            AccesoDatos.AccesoDatos datos = new AccesoDatos.AccesoDatos();

            try
            {
                // Normalizar entrada
                string normalizada = descripcion.Trim().Replace(" ", "").ToUpper();

                datos.setearConsulta(
                    "SELECT COUNT(*) " +
                    "FROM CATEGORIAS " +
                    "WHERE REPLACE(UPPER(LTRIM(RTRIM(Descripcion))), ' ', '') = @desc"
                );

                datos.agregarParametro("@desc", normalizada);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    int cantidad = (int)datos.Lector[0];
                    if (cantidad > 0)
                    {
                        return true;
                    }
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
        public List<Categoria> BuscarCategoriasPorTexto(string texto)
        {
            List<Categoria> lista = new List<Categoria>();
            AccesoDatos.AccesoDatos datos = new AccesoDatos.AccesoDatos();

            try
            {
                // Si no hay texto → devuelvo vacío
                if (string.IsNullOrWhiteSpace(texto))
                    return new List<Categoria>();

                string filtro = "%" + texto.Trim() + "%";

                string query = @"
        SELECT Id, Descripcion
        FROM CATEGORIAS
        WHERE 
               Descripcion LIKE @filtro
            OR CONVERT(VARCHAR(10), Id) LIKE @filtro
        ORDER BY Descripcion ASC";

                datos.setearConsulta(query);
                datos.agregarParametro("@filtro", filtro);

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Categoria aux = new Categoria();

                    aux.id = (int)datos.Lector["Id"];
                    aux.descripcion = datos.Lector["Descripcion"] != DBNull.Value
                        ? (string)datos.Lector["Descripcion"]
                        : "";

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
