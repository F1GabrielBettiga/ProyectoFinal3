using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class FavoritoNegocio
    {
        public List<Favorito> TraerFavoritosPorUsuario(int idUsuario)
        {
            List<Favorito> lista = new List<Favorito>();
            AccesoDatos.AccesoDatos datos = new AccesoDatos.AccesoDatos();

            try
            {
                
                string query = @"
                SELECT 
                 F.Id              AS IdFavorito,
                 F.IdUser          AS IdUser,
                 A.Id              AS IdArticulo,
                 A.Codigo,
                 A.Nombre,
                 A.Descripcion,
                 A.ImagenUrl,
                 A.Precio,
                 A.IdMarca,
                 M.Descripcion     AS MarcaDescripcion,
                 A.IdCategoria,
                 C.Descripcion     AS CategoriaDescripcion
                 FROM FAVORITOS F
                 INNER JOIN ARTICULOS  A ON A.Id = F.IdArticulo
                 LEFT JOIN MARCAS     M ON M.Id = A.IdMarca
                 LEFT JOIN CATEGORIAS C ON C.Id = A.IdCategoria
                 WHERE F.IdUser = @idUsuario
                 ORDER BY F.Id DESC";

                datos.setearConsulta(query);
                datos.agregarParametro("@idUsuario", idUsuario);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    // ====== Favorito ======
                    Favorito fav = new Favorito();
                    fav.id = (int)datos.Lector["IdFavorito"];

                    // ====== Usuario ======
                    fav.usuario = new Usuario();
                    fav.usuario.id = (int)datos.Lector["IdUser"];

                    // ====== Artículo ======
                    Articulo art = new Articulo();
                    art.id = (int)datos.Lector["IdArticulo"];

                    art.codigo = datos.Lector["Codigo"] == DBNull.Value ? "" : (string)datos.Lector["Codigo"];
                    art.nombre = datos.Lector["Nombre"] == DBNull.Value ? "" : (string)datos.Lector["Nombre"];
                    art.descripcion = datos.Lector["Descripcion"] == DBNull.Value ? "" : (string)datos.Lector["Descripcion"];
                    art.imagenUrl = datos.Lector["ImagenUrl"] == DBNull.Value ? "" : (string)datos.Lector["ImagenUrl"];
                    art.precio = datos.Lector["Precio"] == DBNull.Value ? 0m : (decimal)datos.Lector["Precio"];

                    // ---- Marca ----
                    
                    if (datos.Lector["IdMarca"] == DBNull.Value)
                    {
                        art.marca = null; 
                    }
                    else
                    {
                        art.marca = new Marca();
                        art.marca.id = (int)datos.Lector["IdMarca"];
                        art.marca.descripcion = datos.Lector["MarcaDescripcion"] == DBNull.Value ? "" : (string)datos.Lector["MarcaDescripcion"];
                    }

                    // ---- Categoría ----
                    if (datos.Lector["IdCategoria"] == DBNull.Value)
                    {
                        art.categoria = null; 
                    }
                    else
                    {
                        art.categoria = new Categoria();
                        art.categoria.id = (int)datos.Lector["IdCategoria"];
                        art.categoria.descripcion = datos.Lector["CategoriaDescripcion"] == DBNull.Value ? "" : (string)datos.Lector["CategoriaDescripcion"];
                    }

                    fav.articulo = art;
                    lista.Add(fav);
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

        public void InsertarFavorito(int idUsuario, int idArticulo)
        {
            AccesoDatos.AccesoDatos datos = new AccesoDatos.AccesoDatos();

            try
            {
                datos.setearConsulta(
                    "INSERT INTO FAVORITOS (IdUser, IdArticulo) " +
                    "VALUES (@idUsuario, @idArticulo)"
                );

                datos.agregarParametro("@idUsuario", idUsuario);
                datos.agregarParametro("@idArticulo", idArticulo);

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
        public void EliminarFavorito(int idUsuario, int idArticulo)
        {
            AccesoDatos.AccesoDatos datos = new AccesoDatos.AccesoDatos();

            try
            {
                datos.setearConsulta(
                    "DELETE FROM FAVORITOS WHERE IdUser = @idUser AND IdArticulo = @idArticulo"
                );

                datos.agregarParametro("@idUser", idUsuario);
                datos.agregarParametro("@idArticulo", idArticulo);

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
        public bool EliminarFavoritoPorArticulo(int idArticulo)
        {
            AccesoDatos.AccesoDatos datos = new AccesoDatos.AccesoDatos();

            try
            {
                datos.setearConsulta("DELETE FROM FAVORITOS WHERE IdArticulo = @idArticulo");
                datos.agregarParametro("@idArticulo", idArticulo);

                int filasAfectadas = datos.ejecutarAccion();

                // Si borró al menos un registro → TRUE
                return filasAfectadas > 0;
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

        public bool EsFavorito(int idUsuario, int idArticulo)
        {
            AccesoDatos.AccesoDatos datos = new AccesoDatos.AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT COUNT(*) FROM FAVORITOS WHERE IdUser = @idUser AND IdArticulo = @idArticulo");
                datos.agregarParametro("@idUser", idUsuario);
                datos.agregarParametro("@idArticulo", idArticulo);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    int cantidad = (int)datos.Lector[0];
                    return cantidad > 0;   // Si hay 1 o más registros, es favorito
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


    }
}
