using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades.Ayudas {
    /// <summary>
    /// Extension de utilidad para clases pertenecientes al modelo
    /// </summary>
    public static class EntidadExtension {
        /// <summary>
        /// Función que detecta si el objeto es nulo
        /// </summary>
        /// <param name="entidad">Objeto de Entidad de Modelo</param>
        /// <returns>Booleano si el objeto es nulo</returns>
        public static bool EsObjetoNulo<T>(this T entidad) where T : class {
            return entidad == null;
        }

        /// <summary>
        /// Función que detecta si el objeto viene sin contenido o su id es vacío
        /// </summary>
        /// <param name="entidad">Objeto de Entidad de Modelo</param>
        /// <returns>Booleano si el objeto es vacio</returns>
        /*public static bool EsObjetoVacio<T>(this T entidad) where T : class
        {
            return entidad.Id.Equals(Guid.Empty);
        }*/


        /// <summary>
        /// Función que detecta si la lista de objetos es nula
        /// </summary>
        /// <param name="listaEntidades">Lista de Objetos de Entidad de Modelo</param>
        /// <returns>Booleano si la lista de objetos es nulo</returns>
        public static bool EsListaObjetoNula<T>(this IEnumerable<T> listaEntidades) where T : class {
            return listaEntidades == null;
        }

        /// <summary>
        /// Función que detecta si la lista objetos es vacia
        /// </summary>
        /// <param name="listaEntidades">Lista de Objeto de Entidad de Modelo</param>
        /// <returns>Booleano si la lista de objetos es vacia</returns>
        public static bool EsListaObjetoVacia<T>(this IEnumerable<T> listaEntidades) where T : class {
            return !listaEntidades.Any();
        }

        /// <summary>
        /// Función que detecta si la lista de estructura es nula
        /// </summary>
        /// <param name="listaEntidades">Lista de estructura</param>
        /// <returns>Booleano si la lista de estructura es nula</returns>
        public static bool EsListaNula<T>(this IEnumerable<T> listaEntidades) where T : struct {
            return listaEntidades == null;
        }

        /// <summary>
        /// Función que detecta si la lista de estructura es nula
        /// </summary>
        /// <param name="listaEntidades">Lista de estructura</param>
        /// <returns>Booleano si la lista de estructura es nula</returns>
        public static bool EsListaVacia<T>(this IEnumerable<T> listaEntidades) where T : struct {
            return !listaEntidades.Any();
        }

    }
}
