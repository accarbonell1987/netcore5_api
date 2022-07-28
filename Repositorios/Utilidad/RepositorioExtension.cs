using Entidades.Utilidades.Paginado;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio.Utilidad
{
    // Using from: https://www.codingame.com/playgrounds/5363/paging-with-entity-framework-core
    // And https://gunnarpeipman.com/net/ef-core-paging/

    /// <summary>
    /// Clase de extension para el uso dentro de las clases del repositorio
    /// </summary>
    public static class RepositorioExtension
    {
        /// <summary>
        /// Funcion que permite obtener un objeto de Páginación para el repositorio que lo necesite
        /// </summary>
        /// <typeparam name="T">Clase de Entidad de Repositorio</typeparam>
        /// <param name="consulta">Query, definida como Queryable</param>
        /// <param name="pagina">Página actual</param>
        /// <param name="tamanoPagina">Elementos por Página</param>
        /// <returns>Objeto de Paginación con lista de objetos</returns>
        public static ResultadoPaginado<T> ObtenerPaginado<T>(this IQueryable<T> consulta,
                                                              int pagina,
                                                              int tamanoPagina) where T : class
        {
            var result = new ResultadoPaginado<T>
            {
                PaginaActual = pagina,
                TamanoPagina = tamanoPagina,
                ContadorFilas = consulta.Count()
            };


            var contadorPaginas = (double)result.ContadorFilas / tamanoPagina;
            result.TamanoPagina = (int)Math.Ceiling(contadorPaginas);

            var salto = (pagina - 1) * tamanoPagina;
            result.Resultados = consulta.Skip(salto).Take(tamanoPagina).AsEnumerable().ToList();

            return result;
        }

        /// <summary>
        /// Funcion que permite obtener una lista de objetos páginada para el repositorio que lo necesite
        /// </summary>
        /// <typeparam name="T">Clase de Entidad de Repositorio</typeparam>
        /// <param name="consulta">Query, definida como Queryable</param>
        /// <param name="pagina">Página actual</param>
        /// <param name="tamanoPagina">Elementos por Página</param>
        /// <returns>Lista de objetos páginada</returns>
        public static IEnumerable<T> ObtenerListaPaginada<T>(this IQueryable<T> consulta,
                                                             int pagina,
                                                             int tamanoPagina) where T : class
        {
            var salto = (pagina - 1) * tamanoPagina;
            var resultados = consulta.Skip(salto).Take(tamanoPagina).AsEnumerable().ToList();

            return resultados;
        }

        /// <summary>
        /// Funcion asíncrona que permite obtener un objeto de Páginación para el repositorio que lo necesite
        /// </summary>
        /// <typeparam name="T">Clase de Entidad de Repositorio</typeparam>
        /// <param name="consulta">Query, definida como Queryable</param>
        /// <param name="pagina">Página actual</param>
        /// <param name="tamanoPagina">Elementos por Página</param>
        /// <returns>Objeto de Paginación con lista de objetos</returns>
        public static async Task<ResultadoPaginado<T>> ObtenerPaginadoAsinc<T>(this IQueryable<T> consulta,
                                                                               int pagina,
                                                                               int tamanoPagina) where T : class
        {
            var resultado = new ResultadoPaginado<T>
            {
                PaginaActual = pagina,
                TamanoPagina = tamanoPagina,
                ContadorFilas = await consulta.CountAsync()
            };


            var contadorPaginas = (double)resultado.ContadorFilas / tamanoPagina;
            resultado.ContadorPagina = (int)Math.Ceiling(contadorPaginas);

            var salto = (pagina - 1) * tamanoPagina;
            var resultados = consulta.Skip(salto).Take(tamanoPagina).AsEnumerable();
            resultado.Resultados = await resultados.AsQueryable().ToListAsync();

            return resultado;
        }

        /// <summary>
        /// Funcion Asíncrona que permite obtener una lista de objetos páginada para el repositorio que lo necesite
        /// </summary>
        /// <typeparam name="T">Clase de Entidad de Repositorio</typeparam>
        /// <param name="consulta">Query, definida como Queryable</param>
        /// <param name="pagina">Página actual</param>
        /// <param name="tamanoPagina">Elementos por Página</param>
        /// <returns>Lista de objetos páginada</returns>
        public static async Task<IEnumerable<T>> ObtenerListaPaginadaAsinc<T>(this IQueryable<T> consulta,
                                                                              int pagina,
                                                                              int tamanoPagina) where T : class
        {
            var salto = (pagina - 1) * tamanoPagina;
            var resultadosEnumerados = consulta.Skip(salto).Take(tamanoPagina).AsEnumerable();
            var resultados = await resultadosEnumerados.AsQueryable().ToListAsync();

            return resultados;
        }
    }
}
