using Entidades.Modelos;
using Entidades.Utilidades.Paginado.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Declaraciones.Repositorios {
  /// <summary>
  /// Interfaz para la implementación dentro del Repositorio de Usuario
  /// </summary>
  public interface IUsuarioRepositorio {
    /// <summary>
    /// Método de implementación que permite obtener de la base de datos una lista con todos los Usuarios
    /// </summary>
    /// <param name="pagina">Página Actual</param>
    /// <param name="tamanoPagina">Elementos por Página</param>
    /// <returns>Lista de Usuarios</returns>
    IEnumerable<User> ObtenerTodos(int? pagina = null, int? tamanoPagina = null);

    /// <summary>
    /// Método de implementación asíncrono que permite obtener de la base de datos el listado de Usuarios
    /// </summary>
    /// <param name="pagina">Página Actual</param>
    /// <param name="tamanoPagina">Elementos por Página</param>
    /// <returns>Lista de Usuarios</returns>
    Task<IEnumerable<User>> ObtenerTodosAsinc(int? pagina = null, int? tamanoPagina = null);

    /// <summary>
    /// Método de implementación que permite obtener de la base de datos un Usuario por medio de su Id
    /// </summary>
    /// <param name="idUsuario">idUsuario</param>
    /// <returns>Objeto Usuario</returns>
    User ObtenerUsuarioPorId(int idUsuario);

    /// <summary>
    /// Método de implementación asíncrono que permite obtener de la base de datos un Usuario por medio de su Id
    /// </summary>
    /// <param name="idUsuario"></param>
    /// <returns></returns>
    Task<User> ObtenerUsuarioPorIdAsinc(int idUsuario);

    /// <summary>
    /// Método de implementación que permite crear un registro de un Usuario en la base de datos
    /// </summary>
    /// <param name="Usuario">Objeto Usuario</param>
    void CrearUsuario(User usuario);

    /// <summary>
    /// Método de implementación que permite hacer una actualización en la base de datos de un Usuario
    /// </summary>
    /// <param name="usuarioBD">Objeto Usuario con datos provenientes de la base de datos</param>
    /// <param name="usuario">Objeto Usuario con los datos a actualizar</param>
    void ActualizarUsuario(User usuarioBD, User usuario);

    /// <summary>
    /// Método de implementación que permite eliminar un registro de la base de datos de un Usuario
    /// </summary>
    /// <param name="Usuario">Objeto Usuario</param>
    void EliminarUsuario(User usuario);

        /// <summary>
        /// Método de implementación que permite obtener de la base de datos un objeto de Paginación con la lista de los Usuarios
        /// </summary>
        /// <param name="pagina">Página Actual</param>
        /// <param name="tamanoPagina">Elementos por Página</param>
        /// <returns>Objeto de Paginación con la Lista de Usuarios</returns>
        Task<IResultadoPaginado<User>> ObtenerTodosPaginado(int? pagina = null, int? tamanoPagina = null);
    }
}