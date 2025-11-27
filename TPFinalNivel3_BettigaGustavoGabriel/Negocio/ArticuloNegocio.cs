using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> listarArticulos()
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos.AccesoDatos datos = new AccesoDatos.AccesoDatos();

            try
            {
                datos.setearConsulta(
                    "SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, " +
                    "       A.IdMarca AS MarcaId, M.Descripcion AS MarcaDescripcion, " +
                    "       A.IdCategoria AS CategoriaId, C.Descripcion AS CategoriaDescripcion, " +
                    "       A.ImagenUrl, A.Precio " +
                    "FROM ARTICULOS A " +
                    "INNER JOIN CATEGORIAS C ON A.IdCategoria = C.Id " +
                    "INNER JOIN MARCAS M     ON A.IdMarca = M.Id"
                );

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();

                    // Id (int, no debería ser null)
                    aux.id = datos.Lector["Id"] is DBNull ? 0 : (int)datos.Lector["Id"];

                    // Código
                    aux.codigo = datos.Lector["Codigo"] is DBNull? null: datos.Lector["Codigo"].ToString();

                    // Nombre
                    aux.nombre = datos.Lector["Nombre"] is DBNull? null: datos.Lector["Nombre"].ToString();

                    // Descripción
                    aux.descripcion = datos.Lector["Descripcion"] is DBNull? null: datos.Lector["Descripcion"].ToString();

                    // ImagenUrl (puede venir null → lo dejamos en null)
                    aux.imagenUrl = datos.Lector["ImagenUrl"] is DBNull? null: datos.Lector["ImagenUrl"].ToString();

                    // Precio
                    aux.precio = datos.Lector["Precio"] is DBNull? 0m: (decimal)datos.Lector["Precio"];

                    // Marca
                    aux.marca = new Marca();
                    aux.marca.id = datos.Lector["MarcaId"] is DBNull ? 0: (int)datos.Lector["MarcaId"];

                    aux.marca.descripcion = datos.Lector["MarcaDescripcion"] is DBNull? null : datos.Lector["MarcaDescripcion"].ToString();

                    // Categoria
                    aux.categoria = new Categoria();
                    aux.categoria.id = datos.Lector["CategoriaId"] is DBNull ? 0: (int)datos.Lector["CategoriaId"];

                    aux.categoria.descripcion = datos.Lector["CategoriaDescripcion"] is DBNull? null: datos.Lector["CategoriaDescripcion"].ToString();

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

        public bool actualizarArticulo (Articulo articulo)
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
                    "UPDATE ARTICULOS SET " +
                    "Codigo = @Codigo, " +
                    "Nombre = @Nombre, " +
                    "Descripcion = @Descripcion, " +
                    "IdMarca = @IdMarca, " +
                    "IdCategoria = @IdCategoria, " +
                    "ImagenUrl = @ImagenUrl, " +
                    "Precio = @Precio " +
                    "WHERE Id = @Id"
                );

                // Strings: si vienen null o vacíos -> DB NULL
                datos.agregarParametro("@Codigo", ValorONull(articulo.codigo));
                datos.agregarParametro("@Nombre", ValorONull(articulo.nombre));
                datos.agregarParametro("@Descripcion", ValorONull(articulo.descripcion));
                datos.agregarParametro("@ImagenUrl", ValorONull(articulo.imagenUrl));

                
                datos.agregarParametro("@IdMarca",
                    articulo.marca != null ? articulo.marca.id : (object)DBNull.Value);

                datos.agregarParametro("@IdCategoria",
                    articulo.categoria != null ? articulo.categoria.id : (object)DBNull.Value);

                datos.agregarParametro("@Precio", articulo.precio);

                datos.agregarParametro("@Id", articulo.id);

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


        public bool agregarArticulo(Articulo articulo)
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

                datos.setearConsulta( "INSERT INTO ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, ImagenUrl, Precio)" +
                                      "VALUES (@codigo, @nombre, @descripcion, @idMarca, @idCategoria, @imagenUrl, @precio);");

                // Strings: si vienen null o vacíos -> DB NULL
                datos.agregarParametro("@Codigo", ValorONull(articulo.codigo));
                datos.agregarParametro("@Nombre", ValorONull(articulo.nombre));
                datos.agregarParametro("@Descripcion", ValorONull(articulo.descripcion));
                datos.agregarParametro("@ImagenUrl", ValorONull(articulo.imagenUrl));


                datos.agregarParametro("@IdMarca",
                    articulo.marca != null ? articulo.marca.id : (object)DBNull.Value);

                datos.agregarParametro("@IdCategoria",
                    articulo.categoria != null ? articulo.categoria.id : (object)DBNull.Value);

                datos.agregarParametro("@Precio", articulo.precio);

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
                datos.setearConsulta("SELECT ISNULL(MAX(Id), 0) + 1 FROM ARTICULOS");
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    return (int)datos.Lector[0];
                }
                else
                {

                    return 1; // si no hay registros todavía
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



    }
}
