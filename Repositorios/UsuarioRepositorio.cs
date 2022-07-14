using Declaraciones.Repositorios;
using Entidades;
using Entidades.Modelo.Extensiones;
using Entidades.Modelos;
using Entidades.Utilidades.Paginado;
using Entidades.Utilidades.Paginado.Interfaces;
using Repositorio.Base;
using Repositorio.Utilidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositorios {
  public class UsuarioRepositorio : RepositorioBase<User>, IUsuarioRepositorio {

    public UsuarioRepositorio(ContextoBD contextoBD) : base(contextoBD) { }

    /// <summary>
    /// Método que permite hacer una actualización en la base de datos de un Usuario
    /// </summary>
    /// <param name="usuarioBD">Objeto Usuario con datos provenientes de la base de datos</param>
    /// <param name="usuario">Objeto Usuario con los datos a actualizar</param>
    public void ActualizarUsuario(User usuarioBD, User usuario) {
      usuarioBD.Mapeo(usuario);
      this.Actualizar(usuario);
    }

    /// <summary>
    /// Método que permite crear un registro de un Usuario en la base de datos
    /// </summary>
    /// <param name="usuario">Objeto Usuario</param>
    public void CrearUsuario(User usuario) {
      this.Crear(usuario);
    }

    /// <summary>
    /// Método que permite eliminar un registro de la base de datos de un Usuario
    /// </summary>
    /// <param name="usuario">Objeto Usuario</param>
    public void EliminarUsuario(User usuario) {
      this.Eliminar(usuario);
    }

    /// <summary>
    /// Método que permite obtener de la base de datos una lista con todas los Usuarios
    /// </summary>
    /// <param name="pagina">Página Actual</param>
    /// <param name="tamanoPagina">Elementos por Página</param>
    /// <returns>Lista de Usuarios</returns>
    public IEnumerable<User> ObtenerTodos(int? pagina = null, int? tamanoPagina = null) {
      var usuarios = this.EncontrarTodos();
      if (pagina.HasValue && tamanoPagina.HasValue) {
        return usuarios.ObtenerListaPaginada(pagina.Value, tamanoPagina.Value);
      }

      return usuarios.AsEnumerable().ToList();
    }

    /// <summary>
    /// Método asincrono que permite obtener de la base de datos una lista con todas los Usuarios
    /// </summary>
    /// <param name="pagina">Página Actual</param>
    /// <param name="tamanoPagina">Elementos por Página</param>
    /// <returns>Lista de Usuarios</returns>
    public async Task<IEnumerable<User>> ObtenerTodosAsinc(int? pagina = null, int? tamanoPagina = null) {
      var usuarios = this.EncontrarTodos();
      if (pagina.HasValue && tamanoPagina.HasValue) {
        return usuarios.ObtenerListaPaginada(pagina.Value, tamanoPagina.Value);
      }

      return await Task.FromResult(usuarios.AsEnumerable().ToList());
    }

    /// <summary>
    /// Método que permite obtener de la base de datos un objeto de Paginación con la lista de los Usuarios
    /// </summary>
    /// <param name="pagina">Página Actual</param>
    /// <param name="tamanoPagina">Elementos por Página</param>
    /// <returns>Objeto de Paginación con la Lista de Usuarios</returns>
    public IResultadoPaginado<User> ObtenerTodosPaginado(int? pagina = null, int? tamanoPagina = null) {
      var usuarios = this.EncontrarTodos();
      if (pagina.HasValue && tamanoPagina.HasValue) {
        return usuarios.ObtenerPaginado(pagina.Value, tamanoPagina.Value);
      }

      //await Task.FromResult(listaAsignacionConsulta.AsEnumerable().ToList());
      return new ResultadoPaginado<User> {
        ContadorFilas = usuarios.Count(),
        Resultados = usuarios.AsEnumerable().ToList()
      };
    }

    /// <summary>
    /// Método que permite obtener de la base de datos un Usuario por medio de su Id
    /// </summary>
    /// <param name="idUsuario">Id de Usuario</param>
    /// <returns>Objeto Usuario</returns>
    public User ObtenerUsuarioPorId(int idUsuario) {
      var usuarioEncontrado = this.EncontrarPorCondicion(usuario => usuario.IdUsuario.Equals(idUsuario));
      return usuarioEncontrado.AsEnumerable().DefaultIfEmpty(new User()).FirstOrDefault();
    }

    /// <summary>
    /// Método asíncrono que permite obtener de la base de datos un Usuario por medio de su Id
    /// </summary>
    /// <param name="idUsuario">Id de Usuario</param>
    /// <returns>Objeto Usuario</returns>
    public async Task<User> ObtenerUsuarioPorIdAsinc(int idUsuario) {
      var usuarioEncontrado = this.EncontrarPorCondicion(usuario => usuario.IdUsuario.Equals(idUsuario));
      return await Task.FromResult(usuarioEncontrado.AsEnumerable().DefaultIfEmpty(new User()).FirstOrDefault());
    }
  }
}