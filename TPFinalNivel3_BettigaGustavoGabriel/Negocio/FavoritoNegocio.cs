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
                // Consulta: favoritos del usuario + datos completos del artículo
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
            INNER JOIN MARCAS     M ON M.Id = A.IdMarca
            INNER JOIN CATEGORIAS C ON C.Id = A.IdCategoria
            WHERE F.IdUser = @idUsuario
            ORDER BY A.Nombre";

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
                    art.codigo = (string)datos.Lector["Codigo"];
                    art.nombre = (string)datos.Lector["Nombre"];
                    art.descripcion = (string)datos.Lector["Descripcion"];
                    art.imagenUrl = (string)datos.Lector["ImagenUrl"];
                    art.precio = (decimal)datos.Lector["Precio"];

                    // ---- Marca ----
                    art.marca = new Marca();
                    art.marca.id = (int)datos.Lector["IdMarca"];
                    art.marca.descripcion = (string)datos.Lector["MarcaDescripcion"];

                    // ---- Categoría ----
                    art.categoria = new Categoria();
                    art.categoria.id = (int)datos.Lector["IdCategoria"];
                    art.categoria.descripcion = (string)datos.Lector["CategoriaDescripcion"];

                    
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
