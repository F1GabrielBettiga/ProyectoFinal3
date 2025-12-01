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
                datos.setearConsulta("SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, " +
                                     "       A.IdMarca AS MarcaId, M.Descripcion AS MarcaDescripcion, " +
                                     "       A.IdCategoria AS CategoriaId, C.Descripcion AS CategoriaDescripcion, " +
                                     "       A.ImagenUrl, A.Precio " +
                                     "FROM ARTICULOS A " +
                                     "INNER JOIN CATEGORIAS C ON A.IdCategoria = C.Id " +
                                     "INNER JOIN MARCAS M     ON A.IdMarca = M.Id " +
                                     "ORDER BY A.Nombre ASC"
                );

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();

                    // Id (int, no debería ser null)
                    aux.id = datos.Lector["Id"] is DBNull ? 0 : (int)datos.Lector["Id"];

                    // Código
                    aux.codigo = datos.Lector["Codigo"] is DBNull ? null : datos.Lector["Codigo"].ToString();

                    // Nombre
                    aux.nombre = datos.Lector["Nombre"] is DBNull ? null : datos.Lector["Nombre"].ToString();

                    // Descripción
                    aux.descripcion = datos.Lector["Descripcion"] is DBNull ? null : datos.Lector["Descripcion"].ToString();

                    // ImagenUrl (puede venir null → lo dejamos en null)
                    aux.imagenUrl = datos.Lector["ImagenUrl"] is DBNull ? null : datos.Lector["ImagenUrl"].ToString();

                    // Precio
                    aux.precio = datos.Lector["Precio"] is DBNull ? 0m : (decimal)datos.Lector["Precio"];

                    // Marca
                    aux.marca = new Marca();
                    aux.marca.id = datos.Lector["MarcaId"] is DBNull ? 0 : (int)datos.Lector["MarcaId"];

                    aux.marca.descripcion = datos.Lector["MarcaDescripcion"] is DBNull ? null : datos.Lector["MarcaDescripcion"].ToString();

                    // Categoria
                    aux.categoria = new Categoria();
                    aux.categoria.id = datos.Lector["CategoriaId"] is DBNull ? 0 : (int)datos.Lector["CategoriaId"];

                    aux.categoria.descripcion = datos.Lector["CategoriaDescripcion"] is DBNull ? null : datos.Lector["CategoriaDescripcion"].ToString();

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
        public List<Articulo> FiltrarArticulos(int? idCategoria, int? idMarca, decimal? precioMin, decimal? precioMax)
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos.AccesoDatos datos = new AccesoDatos.AccesoDatos();

            try
            {
                // ===========================
                // 1) Base de la consulta
                // ===========================
                string query = @"
            SELECT A.Id, A.Nombre, A.Descripcion, A.ImagenUrl,
                   A.Precio, A.IdCategoria, A.IdMarca,
                   C.Descripcion AS CategoriaDescripcion,
                   M.Descripcion AS MarcaDescripcion
            FROM ARTICULOS A
            INNER JOIN CATEGORIAS C ON C.Id = A.IdCategoria
            INNER JOIN MARCAS M ON M.Id = A.IdMarca
            WHERE 1=1";

                // ===========================
                // 2) Armo el WHERE dinámico
                //    (solo texto, sin parámetros todavía)
                // ===========================
                if ((idCategoria.HasValue) && (idCategoria.Value > 0))
                {
                    query += " AND A.IdCategoria = @idCategoria";
                }

                if ((idMarca.HasValue) && (idMarca.Value > 0))
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

                // ===========================
                // 3) Ahora recién seteo la consulta
                // ===========================
                datos.setearConsulta(query);

                // ===========================
                // 4) Y recién acá agrego los parámetros
                // ===========================
                if ((idCategoria.HasValue) && (idCategoria.Value > 0))
                {
                    datos.agregarParametro("@idCategoria", idCategoria.Value);
                }

                if ((idMarca.HasValue) && (idMarca.Value > 0))
                {
                    datos.agregarParametro("@idMarca", idMarca.Value);
                }

                if (precioMin.HasValue)
                {
                    datos.agregarParametro("@precioMin", precioMin.Value);
                }

                if (precioMax.HasValue)
                {
                    datos.agregarParametro("@precioMax", precioMax.Value);
                }

                // ===========================
                // 5) Ejecuto y mapeo
                // ===========================
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();

                    aux.id = (int)datos.Lector["Id"];
                    aux.nombre = (string)datos.Lector["Nombre"];
                    aux.descripcion = (string)datos.Lector["Descripcion"];
                    aux.imagenUrl = (string)datos.Lector["ImagenUrl"];
                    aux.precio = (decimal)datos.Lector["Precio"];

                    // ---- Categoria ----
                    aux.categoria = new Categoria();
                    aux.categoria.id = (int)datos.Lector["IdCategoria"];
                    aux.categoria.descripcion = (string)datos.Lector["CategoriaDescripcion"];

                    // ---- Marca ----
                    aux.marca = new Marca();
                    aux.marca.id = (int)datos.Lector["IdMarca"];
                    aux.marca.descripcion = (string)datos.Lector["MarcaDescripcion"];

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
                // Si viene vacío, podés devolver lista vacía o la lista completa;
                // por ahora devuelvo vacío para que no rompa.
                if (string.IsNullOrWhiteSpace(texto))
                {
                    return new List<Articulo>();
                }

                string filtro = "%" + texto.Trim() + "%";

                string query = @"
            SELECT  A.Id,
                    A.Codigo,
                    A.Nombre,
                    A.Descripcion,
                    A.ImagenUrl,
                    A.Precio,
                    A.IdMarca      AS MarcaId,
                    M.Descripcion  AS MarcaDescripcion,
                    A.IdCategoria  AS CategoriaId,
                    C.Descripcion  AS CategoriaDescripcion
            FROM ARTICULOS A
            INNER JOIN CATEGORIAS C ON A.IdCategoria = C.Id
            INNER JOIN MARCAS     M ON A.IdMarca     = M.Id
            WHERE
                   (A.Codigo      LIKE @filtro)
                OR (A.Nombre      LIKE @filtro)
                OR (A.Descripcion LIKE @filtro)
                OR (M.Descripcion LIKE @filtro)
                OR (C.Descripcion LIKE @filtro)
                OR (CONVERT(varchar(50), A.Precio) LIKE @filtro)
            ORDER BY A.Nombre ASC";

                // 1) Seteamos el texto de la consulta
                datos.setearConsulta(query);

                // 2) Agregamos el parámetro @filtro DESPUÉS de setearConsulta
                datos.agregarParametro("@filtro", filtro);

                // 3) Ejecutamos
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();

                    aux.id = (int)datos.Lector["Id"];
                    aux.codigo = (string)datos.Lector["Codigo"];
                    aux.nombre = (string)datos.Lector["Nombre"];
                    aux.descripcion = (string)datos.Lector["Descripcion"];
                    aux.imagenUrl = (string)datos.Lector["ImagenUrl"];
                    aux.precio = (decimal)datos.Lector["Precio"];

                    // --- Marca ---
                    aux.marca = new Marca();
                    aux.marca.id = (int)datos.Lector["MarcaId"];
                    aux.marca.descripcion = (string)datos.Lector["MarcaDescripcion"];

                    // --- Categoría ---
                    aux.categoria = new Categoria();
                    aux.categoria.id = (int)datos.Lector["CategoriaId"];
                    aux.categoria.descripcion = (string)datos.Lector["CategoriaDescripcion"];

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
