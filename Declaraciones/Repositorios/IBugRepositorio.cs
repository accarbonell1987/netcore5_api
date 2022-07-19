using Entidades.Modelos;
using Entidades.Utilidades.Paginado.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Declaraciones.Repositorios {
    /// <summary>
    /// Interfaz para la implementación dentro del Repositorio de Bug
    /// </summary>
    public interface IBugRepositorio {
        /// <summary>
        /// Método de implementación que permite obtener de la base de datos una lista con todos los Bugs
        /// </summary>
        /// <param name="pagina">Página Actual</param>
        /// <param name="tamanoPagina">Elementos por Página</param>
        /// <returns>Lista de Bugs</returns>
        IEnumerable<Bug> ObtenerTodos(int? pagina = null, int? tamanoPagina = null);

        /// <summary>
        /// Método de implementación asíncrono que permite obtener de la base de datos el listado de Bugs
        /// </summary>
        /// <param name="pagina">Página Actual</param>
        /// <param name="tamanoPagina">Elementos por Página</param>
        /// <returns>Lista de Bugs</returns>
        Task<IEnumerable<Bug>> ObtenerTodosAsinc(int? pagina = null, int? tamanoPagina = null);

        /// <summary>
        /// Método de implementación que permite obtener de la base de datos un objeto de Paginación con la lista de los Bugs
        /// </summary>
        /// <param name="pagina">Página Actual</param>
        /// <param name="tamanoPagina">Elementos por Página</param>
        /// <returns>Objeto de Paginación con la Lista de Bugs</returns>
        IResultadoPaginado<Bug> ObtenerTodosPaginado(int? pagina = null, int? tamanoPagina = null);

        /// <summary>
        /// Método de implementación que permite obtener de la base de datos un Bug por medio de su Id
        /// </summary>
        /// <param name="idBug">idBug</param>
        /// <returns>Objeto Bug</returns>
        Bug ObtenerBugPorId(int idBug);

        /// <summary>
        /// Método de implementación asíncrono que permite obtener de la base de datos un Bug por medio de su Id
        /// </summary>
        /// <param name="idBug">idBug</param>
        /// <returns></returns>
        Task<Bug> ObtenerBugPorIdAsinc(int idBug);

        /// <summary>
        /// Método de implementación asíncrono que permite obtener de la base de datos un Bug por medio del
        /// id de proyecto y usuario
        /// </summary>
        /// <param name="idUsuario">idUsuario</param>
        /// <param name="idProyecto">idProyecto</param>
        /// <returns>Bug</returns>
        Task<Bug> ObtenerBugPorUsuarioYProyetoAsinc(int idUsuario, int idProyecto);

        /// <summary>
        /// Método de implementación asíncrono que permite obtener de la base de datos los Bugs por medio del
        /// id de proyecto
        /// </summary>
        /// <param name="idProyecto">idProyecto</param>
        /// <returns>Lista de Bugs</returns>
        Task<IEnumerable<Bug>> ObtenerBugsPorProyetoAsinc(int idProyecto);

        /// <summary>
        /// Método de implementación asíncrono que permite obtener de la base de datos los Bugs por medio del
        /// id de usuario
        /// </summary>
        /// <param name="idUsuario">idUsuario</param>
        /// <returns>Lista de Bugs</returns>
        Task<IEnumerable<Bug>> ObtenerBugsPorUsuarioAsinc(int idUsuario);

        /// <summary>
        /// Método de implementación que permite crear un registro de un Bug en la base de datos
        /// </summary>
        /// <param name="bug">Objeto Bug</param>
        void CrearBug(Bug bug);

        /// <summary>
        /// Método de implementación que permite hacer una actualización en la base de datos de un Bug
        /// </summary>
        /// <param name="bugBD">Objeto Bug con datos provenientes de la base de datos</param>
        /// <param name="bug">Objeto Bug con los datos a actualizar</param>
        void ActualizarBug(Bug bugBD, Bug bug);

        /// <summary>
        /// Método de implementación que permite eliminar un registro de la base de datos de un Bug
        /// </summary>
        /// <param name="bug">Objeto Bug</param>
        void EliminarBug(Bug bug);
    }
}