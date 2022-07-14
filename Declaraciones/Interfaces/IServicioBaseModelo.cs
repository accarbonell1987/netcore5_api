using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Declaraciones.Interfaces
{
    /// <summary>
    /// Interface de Base para ser implementada con los métodos de Servicios en el consumo de una API externa
    /// </summary>
    /// <typeparam name="T">Clase Parametro</typeparam>
    public interface IServicioBaseModelo<T>
    {
        /// <summary>
        /// Método asíncrono de implementación Base para la obtención de Lista de Objetos por medio del servicio
        /// </summary>
        /// <param name="endPoint">EndPoint que se inserta a la URL Base</param>
        /// <param name="parametrosSolicitud">Parametros de consulta junto con el Endpoint</param>
        /// <returns>Lista de Objetos Serializados</returns>
        Task<IList<T>> ObtenerLista(string endPoint, params string[] parametrosSolicitud);

        /// <summary>
        /// Método asíncrono de implementación Base para la obtención de Lista de Objetos por medio del servicio
        /// </summary>
        /// <param name="endPoint">EndPoint que se inserta a la URL Base</param>
        /// <param name="parametrosSolicitud">Parametros de consulta junto con el Endpoint</param>
        /// <returns>Lista de Objetos Serializados</returns>
        Task<IList<T>> ObtenerLista(string endPoint, Dictionary<string, string> parametrosSolicitud = null);

        /// <summary>
        /// Método asíncrono de implementación Base para la obtención de un Objeto por medio del servicio
        /// </summary>
        /// <param name="endPoint">EndPoint que se inserta a la URL Base</param>
        /// <param name="parametrosSolicitud">Parametros de consulta junto con el Endpoint</param>
        /// <returns>Objeto Serializado</returns>
        Task<T> Obtener(string endPoint, params string[] parametrosSolicitud);

        /// <summary>
        /// Método asíncrono de implementación Base para la obtención de un Objeto por medio del servicio
        /// </summary>
        /// <param name="endPoint">EndPoint que se inserta a la URL Base</param>
        /// <param name="parametrosSolicitud">Parametros de consulta junto con el Endpoint</param>
        /// <returns>Objeto Serializado</returns>
        Task<object> ObtenerComoObjeto(string endPoint, Dictionary<string, string> parametrosSolicitud = null);

        /// <summary>
        /// Método asíncrono de implementación Base para la obtención de un Objeto por medio del servicio
        /// </summary>
        /// <param name="endPoint">EndPoint que se inserta a la URL Base</param>
        /// <param name="parametrosSolicitud">Parametros de consulta junto con el Endpoint</param>
        /// <returns>Objeto Serializado</returns>
        Task<T> Obtener(string endPoint, Dictionary<string, string> parametrosSolicitud = null);

        /// <summary>
        /// Método asíncrono de implementación Base para Crear un Objeto por medio del servicio
        /// </summary>
        /// <param name="endPoint">EndPoint que se inserta a la URL Base</param>
        /// <param name="objeto">Objeto que se inserta en el body</param>
        /// <param name="parametrosSolicitud">Parametros de consulta junto con el Endpoint</param>
        /// <returns>Objeto de Respuesta Serializado</returns>
        Task<object> Crear(string endPoint, T objeto, params string[] parametrosSolicitud);

        /// <summary>
        /// Método asíncrono de implementación Base para Crear un Objeto por medio del servicio
        /// </summary>
        /// <param name="endPoint">EndPoint que se inserta a la URL Base</param>
        /// <param name="objeto">Objeto que se inserta en el body</param>
        /// <param name="parametrosSolicitud">Parametros de consulta junto con el Endpoint</param>
        /// <returns>Objeto de Respuesta Serializado</returns>
        Task<object> Crear<V>(string endPoint, V objeto, params string[] parametrosSolicitud);

        /// <summary>
        /// Método asíncrono de implementación Base para Actualizar un Objeto por medio del servicio
        /// </summary>
        /// <param name="endPoint">EndPoint que se inserta a la URL Base</param>
        /// <param name="objeto">Objeto que se inserta en el body</param>
        /// <param name="parametrosSolicitud">Parametros de consulta junto con el Endpoint</param>
        /// <returns>Objeto de Respuesta Serializado</returns>
        Task<object> Actualizar(string endPoint, T objeto, params string[] parametrosSolicitud);

        /// <summary>
        /// Método asíncrono de implementación Base para la obtención de un Objeto por medio del servicio
        /// </summary>
        /// <param name="endPoint">EndPoint que se inserta a la URL Base</param>
        /// <param name="parametrosSolicitud">Parametros de consulta junto con el Endpoint</param>
        /// <returns>Objeto de Respuesta Serializado</returns>
        Task<object> Eliminar(string endPoint, params string[] parametrosSolicitud);
    }
}
