using Entidades.Modelos;
using Entidades.Utilidades.Paginado.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Declaraciones.Repositorios {
  /// <summary>
  /// Interfaz para la implementación dentro del Repositorio de Proyecto
  /// </summary>
  public interface IProyectoRepositorio {
    /// <summary>
    /// Método de implementación que permite obtener de la base de datos una lista con todos los Proyectos
    /// </summary>
    /// <param name="pagina">Página Actual</param>
    /// <param name="tamanoPagina">Elementos por Página</param>
    /// <returns>Lista de Proyectos</returns>
    IEnumerable<Project> ObtenerTodos(int? pagina = null, int? tamanoPagina = null);

    /// <summary>
    /// Método de implementación asíncrono que permite obtener de la base de datos el listado de Proyectos
    /// </summary>
    /// <param name="pagina">Página Actual</param>
    /// <param name="tamanoPagina">Elementos por Página</param>
    /// <returns>Lista de Proyectos</returns>
    Task<IEnumerable<Project>> ObtenerTodosAsinc(int? pagina = null, int? tamanoPagina = null);

    /// <summary>
    /// Método de implementación que permite obtener de la base de datos un objeto de Paginación con la lista de los Proyectos
    /// </summary>
    /// <param name="pagina">Página Actual</param>
    /// <param name="tamanoPagina">Elementos por Página</param>
    /// <returns>Objeto de Paginación con la Lista de Proyectos</returns>
    IResultadoPaginado<Project> ObtenerTodosPaginado(int? pagina = null, int? tamanoPagina = null);

    /// <summary>
    /// Método de implementación que permite obtener de la base de datos un Proyecto por medio de su Id
    /// </summary>
    /// <param name="idProyecto">idProyecto</param>
    /// <returns>Objeto Proyecto</returns>
    Project ObtenerProyectoPorId(int idProyecto);

    /// <summary>
    /// Método de implementación asíncrono que permite obtener de la base de datos un Proyecto por medio de su Id
    /// </summary>
    /// <param name="idProyecto"></param>
    /// <returns></returns>
    Task<Project> ObtenerProyectoPorIdAsinc(int idProyecto);

    /// <summary>
    /// Método de implementación que permite crear un registro de un Proyecto en la base de datos
    /// </summary>
    /// <param name="Proyecto">Objeto Proyecto</param>
    void CrearProyecto(Project Proyecto);

    /// <summary>
    /// Método de implementación que permite hacer una actualización en la base de datos de un Proyecto
    /// </summary>
    /// <param name="Proyecto">Objeto Proyecto con datos provenientes de la base de datos</param>
    void ActualizarProyecto(Project Proyecto);

    /// <summary>
    /// Método de implementación que permite eliminar un registro de la base de datos de un Proyecto
    /// </summary>
    /// <param name="Proyecto">Objeto Proyecto</param>
    void EliminarProyecto(Project Proyecto);
  }
}