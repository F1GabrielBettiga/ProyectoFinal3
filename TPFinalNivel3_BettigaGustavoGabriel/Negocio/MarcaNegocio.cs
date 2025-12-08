using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class MarcaNegocio
    {
        public List<Marca> listarMarcas()
        {
            List<Marca> lista = new List<Marca>();
            AccesoDatos.AccesoDatos datos = new AccesoDatos.AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT Id, Descripcion FROM MARCAS");

                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Marca aux = new Marca();
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

        public bool actualizarMarca(Marca marca)
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
                    "UPDATE MARCAS SET " +
                    "Descripcion = @Descripcion " +
                    "WHERE Id = @Id"
                );

                // Strings: si vienen null o vacíos -> DB NULL
                datos.agregarParametro("@Descripcion", ValorONull(marca.descripcion));

                datos.agregarParametro("@Id", marca.id);

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

        public bool agregarMarca(Marca marca)
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

                datos.setearConsulta("INSERT INTO MARCAS (Descripcion)" +
                                      "VALUES (@Descripcion);");

                // Strings: si vienen null o vacíos -> DB NULL
                datos.agregarParametro("@Descripcion", ValorONull(marca.descripcion));

                

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

        public bool eliminarMarca(int id)
        {
            AccesoDatos.AccesoDatos datos = new AccesoDatos.AccesoDatos();
            try
            {
                datos.setearConsulta("DELETE FROM MARCAS WHERE Id = @Id");
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

        public List<Marca> listarMarcasPorCategoria(int? idCategoria)
        {
            var lista = new List<Marca>();
            var datos = new AccesoDatos.AccesoDatos();

            try
            {
                if (idCategoria.HasValue && idCategoria.Value > 0)
                {
                    datos.setearConsulta(
                        "SELECT DISTINCT M.Id, M.Descripcion " +
                        "FROM MARCAS M " +
                        "INNER JOIN ARTICULOS A ON A.IdMarca = M.Id " +
                        "WHERE A.IdCategoria = @idCategoria " +
                        "ORDER BY M.Descripcion");

                    datos.agregarParametro("@idCategoria", idCategoria.Value);
                }
                else
                {
                    datos.setearConsulta(
                        "SELECT Id, Descripcion " +
                        "FROM MARCAS " +
                        "ORDER BY Descripcion");
                }

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    var m = new Marca();
                    m.id = (int)datos.Lector["Id"];
                    m.descripcion = (string)datos.Lector["Descripcion"];
                    lista.Add(m);
                }

                return lista;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public bool ExisteMarca(string descripcion)
        {
            AccesoDatos.AccesoDatos datos = new AccesoDatos.AccesoDatos();

            try
            {
                // Normalizar entrada
                string normalizada = descripcion.Trim().Replace(" ", "").ToUpper();

                datos.setearConsulta(
                    "SELECT COUNT(*) " +
                    "FROM MARCAS " +
                    "WHERE REPLACE(UPPER(LTRIM(RTRIM(Descripcion))), ' ', '') = @desc"
                );

                datos.agregarParametro("@desc", normalizada);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    int cantidad = (int)datos.Lector[0];
                    return cantidad > 0;
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
        public List<Marca> BuscarMarcasPorTexto(string texto)
        {
            List<Marca> lista = new List<Marca>();
            AccesoDatos.AccesoDatos datos = new AccesoDatos.AccesoDatos();

            try
            {
                // Sin texto → no busca nada
                if (string.IsNullOrWhiteSpace(texto))
                    return new List<Marca>();

                string filtro = "%" + texto.Trim() + "%";

                string query = @"
        SELECT Id, Descripcion
        FROM MARCAS
        WHERE 
               Descripcion LIKE @filtro
            OR CONVERT(VARCHAR(10), Id) LIKE @filtro
        ORDER BY Descripcion ASC";

                datos.setearConsulta(query);
                datos.agregarParametro("@filtro", filtro);

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Marca aux = new Marca();

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
