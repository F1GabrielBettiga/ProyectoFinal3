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
                    "LEFT JOIN CATEGORIAS C ON A.IdCategoria = C.Id " +  
                    "LEFT JOIN MARCAS M     ON A.IdMarca = M.Id " +      
                    "ORDER BY A.Nombre ASC"
                );

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();

                    aux.id = datos.Lector["Id"] is DBNull ? 0 : (int)datos.Lector["Id"];
                    aux.codigo = datos.Lector["Codigo"] is DBNull ? null : datos.Lector["Codigo"].ToString();
                    aux.nombre = datos.Lector["Nombre"] is DBNull ? null : datos.Lector["Nombre"].ToString();
                    aux.descripcion = datos.Lector["Descripcion"] is DBNull ? null : datos.Lector["Descripcion"].ToString();
                    aux.imagenUrl = datos.Lector["ImagenUrl"] is DBNull ? null : datos.Lector["ImagenUrl"].ToString();
                    aux.precio = datos.Lector["Precio"] is DBNull ? 0m : (decimal)datos.Lector["Precio"];

                    // Marca
                    aux.marca = new Marca();
                    aux.marca.id = datos.Lector["MarcaId"] is DBNull ? 0 : (int)datos.Lector["MarcaId"];
                    aux.marca.descripcion = datos.Lector["MarcaDescripcion"] is DBNull
                                            ? null
                                            : datos.Lector["MarcaDescripcion"].ToString();

                    // Categoría
                    aux.categoria = new Categoria();
                    aux.categoria.id = datos.Lector["CategoriaId"] is DBNull ? 0 : (int)datos.Lector["CategoriaId"];
                    aux.categoria.descripcion = datos.Lector["CategoriaDescripcion"] is DBNull
                                                ? null
                                                : datos.Lector["CategoriaDescripcion"].ToString();

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
        public bool actualizarArticulo(Articulo articulo)
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

                // ----- Strings: si vienen null o vacíos -> DB NULL -----
                datos.agregarParametro("@Codigo", ValorONull(articulo.codigo));
                datos.agregarParametro("@Nombre", ValorONull(articulo.nombre));
                datos.agregarParametro("@Descripcion", ValorONull(articulo.descripcion));
                datos.agregarParametro("@ImagenUrl", ValorONull(articulo.imagenUrl));

                // ----- Marca -----
                // Si la marca es null o su id es 0 ("Sin Marca") -> NULL en BD
                datos.agregarParametro("@IdMarca",
                    (articulo.marca == null || articulo.marca.id == 0)
                        ? (object)DBNull.Value
                        : articulo.marca.id);

                // ----- Categoría -----
                // Si la categoría es null o su id es 0 ("Sin Categoría") -> NULL en BD
                datos.agregarParametro("@IdCategoria",
                    (articulo.categoria == null || articulo.categoria.id == 0)
                        ? (object)DBNull.Value
                        : articulo.categoria.id);

                // ----- Precio e Id -----
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
        public void ActualizarMarcaEnArticulos(int idMarca)
        {
            AccesoDatos.AccesoDatos datos = new AccesoDatos.AccesoDatos();

            try
            {
                datos.setearConsulta(
                    "UPDATE ARTICULOS " +
                    "SET IdMarca = NULL " +
                    "WHERE IdMarca = @idMarca"
                );

                datos.agregarParametro("@idMarca", idMarca);

                datos.ejecutarAccion();
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
        public void ActualizarCategoriaEnArticulos(int idCategoria)
        {
            AccesoDatos.AccesoDatos datos = new AccesoDatos.AccesoDatos();

            try
            {
                datos.setearConsulta(
                    "UPDATE ARTICULOS " +
                    "SET IdCategoria = NULL " +
                    "WHERE IdCategoria = @IdCategoria"
                );

                datos.agregarParametro("@IdCategoria", idCategoria);

                datos.ejecutarAccion();
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

                datos.setearConsulta("INSERT INTO ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, ImagenUrl, Precio)" +
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
        public bool eliminarArticulo(int id)
        {
            AccesoDatos.AccesoDatos datos = new AccesoDatos.AccesoDatos();
            try
            {
                datos.setearConsulta("DELETE FROM ARTICULOS WHERE Id = @Id");
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
        public bool ExisteArticulo(string codigo)
        {
            AccesoDatos.AccesoDatos datos = new AccesoDatos.AccesoDatos();

            try
            {
                
                string codigoNormalizado = (codigo ?? string.Empty).Trim().Replace(" ", "").ToUpper();

                datos.setearConsulta(
                    "SELECT COUNT(*) FROM ARTICULOS " +
                    "WHERE REPLACE(UPPER(Codigo), ' ', '') = @Codigo"
                );

                datos.agregarParametro("@Codigo", codigoNormalizado);
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
        public List<Articulo> FiltrarArticulos(int? idCategoria, int? idMarca, decimal? precioMin, decimal? precioMax)
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos.AccesoDatos datos = new AccesoDatos.AccesoDatos();

            try
            {
                // 1) Base de la consulta: LEFT JOIN para traer también los que NO tienen marca/categoría
                string query = @"
        SELECT  A.Id,
                A.Codigo,
                A.Nombre,
                A.Descripcion,
                A.ImagenUrl,
                A.Precio,
                A.IdCategoria,
                A.IdMarca,
                C.Descripcion AS CategoriaDescripcion,
                M.Descripcion AS MarcaDescripcion
        FROM ARTICULOS A
        LEFT JOIN CATEGORIAS C ON A.IdCategoria = C.Id
        LEFT JOIN MARCAS     M ON A.IdMarca     = M.Id
        WHERE 1 = 1";

                // 2) WHERE dinámico
                if (idCategoria.HasValue && idCategoria.Value > 0)
                {
                    query += " AND A.IdCategoria = @idCategoria";
                }

                if (idMarca.HasValue && idMarca.Value > 0)
                {
                    query += " AND A.IdMarca = @idMarca";
                }

                if (precioMin.HasValue)
                {
                    query += " AND A.Precio >= @precioMin";
                }

                if (precioMax.HasValue)
                {
                    query += " AND A.Precio <= @precioMax";
                }

                query += " ORDER BY A.Nombre ASC";

                // 3) Seteo consulta
                datos.setearConsulta(query);

                // 4) Parámetros
                if (idCategoria.HasValue && idCategoria.Value > 0)
                    datos.agregarParametro("@idCategoria", idCategoria.Value);

                if (idMarca.HasValue && idMarca.Value > 0)
                    datos.agregarParametro("@idMarca", idMarca.Value);

                if (precioMin.HasValue)
                    datos.agregarParametro("@precioMin", precioMin.Value);

                if (precioMax.HasValue)
                    datos.agregarParametro("@precioMax", precioMax.Value);

                // 5) Ejecutar y mapear
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();

                    aux.id = (int)datos.Lector["Id"];
                    aux.codigo = datos.Lector["Codigo"] is DBNull ? null : datos.Lector["Codigo"].ToString();
                    aux.nombre = datos.Lector["Nombre"] is DBNull ? null : datos.Lector["Nombre"].ToString();
                    aux.descripcion = datos.Lector["Descripcion"] is DBNull ? null : datos.Lector["Descripcion"].ToString();
                    aux.imagenUrl = datos.Lector["ImagenUrl"] is DBNull ? null : datos.Lector["ImagenUrl"].ToString();
                    aux.precio = datos.Lector["Precio"] is DBNull ? 0m : (decimal)datos.Lector["Precio"];

                    // ---- Categoría (puede ser null) ----
                    aux.categoria = new Categoria();
                    aux.categoria.id = datos.Lector["IdCategoria"] is DBNull ? 0 : (int)datos.Lector["IdCategoria"];
                    aux.categoria.descripcion = datos.Lector["CategoriaDescripcion"] is DBNull
                                                ? null
                                                : datos.Lector["CategoriaDescripcion"].ToString();

                    // ---- Marca (puede ser null) ----
                    aux.marca = new Marca();
                    aux.marca.id = datos.Lector["IdMarca"] is DBNull ? 0 : (int)datos.Lector["IdMarca"];
                    aux.marca.descripcion = datos.Lector["MarcaDescripcion"] is DBNull
                                            ? null
                                            : datos.Lector["MarcaDescripcion"].ToString();

                    lista.Add(aux);
                }

                return lista;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        // Búsqueda global por texto en varios campos
        public List<Articulo> BuscarArticulosPorTexto(string texto)
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos.AccesoDatos datos = new AccesoDatos.AccesoDatos();

            try
            {
                // Si viene vacío, devuelvo lista vacía (como ya tenías)
                if (string.IsNullOrWhiteSpace(texto))
                    return new List<Articulo>();

                string filtro = "%" + texto.Trim() + "%";

                string query = @"
        SELECT  A.Id,
                A.Codigo,
                A.Nombre,
                A.Descripcion,
                A.ImagenUrl,
                A.Precio,
                A.IdMarca         AS MarcaId,
                M.Descripcion     AS MarcaDescripcion,
                A.IdCategoria     AS CategoriaId,
                C.Descripcion     AS CategoriaDescripcion
        FROM ARTICULOS A
        LEFT JOIN CATEGORIAS C ON A.IdCategoria = C.Id   -- <- LEFT JOIN
        LEFT JOIN MARCAS     M ON A.IdMarca     = M.Id   -- <- LEFT JOIN
        WHERE
               (A.Codigo      LIKE @filtro)
            OR (A.Nombre      LIKE @filtro)
            OR (A.Descripcion LIKE @filtro)
            OR (M.Descripcion LIKE @filtro)
            OR (C.Descripcion LIKE @filtro)
            OR (CONVERT(varchar(50), A.Precio) LIKE @filtro)
        ORDER BY A.Nombre ASC";

                datos.setearConsulta(query);
                datos.agregarParametro("@filtro", filtro);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();

                    aux.id = (int)datos.Lector["Id"];
                    aux.codigo = datos.Lector["Codigo"] is DBNull ? null : datos.Lector["Codigo"].ToString();
                    aux.nombre = datos.Lector["Nombre"] is DBNull ? null : datos.Lector["Nombre"].ToString();
                    aux.descripcion = datos.Lector["Descripcion"] is DBNull ? null : datos.Lector["Descripcion"].ToString();
                    aux.imagenUrl = datos.Lector["ImagenUrl"] is DBNull ? null : datos.Lector["ImagenUrl"].ToString();
                    aux.precio = datos.Lector["Precio"] is DBNull ? 0m : (decimal)datos.Lector["Precio"];

                    // --- Marca (puede ser NULL) ---
                    aux.marca = new Marca();
                    if (datos.Lector["MarcaId"] is DBNull)
                    {
                        aux.marca.id = 0;
                        aux.marca.descripcion = null;
                    }
                    else
                    {
                        aux.marca.id = (int)datos.Lector["MarcaId"];
                        aux.marca.descripcion = datos.Lector["MarcaDescripcion"] is DBNull
                            ? null
                            : datos.Lector["MarcaDescripcion"].ToString();
                    }

                    // --- Categoría (puede ser NULL) ---
                    aux.categoria = new Categoria();
                    if (datos.Lector["CategoriaId"] is DBNull)
                    {
                        aux.categoria.id = 0;
                        aux.categoria.descripcion = null;
                    }
                    else
                    {
                        aux.categoria.id = (int)datos.Lector["CategoriaId"];
                        aux.categoria.descripcion = datos.Lector["CategoriaDescripcion"] is DBNull
                            ? null
                            : datos.Lector["CategoriaDescripcion"].ToString();
                    }

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
