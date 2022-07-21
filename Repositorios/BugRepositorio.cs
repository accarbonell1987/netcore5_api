using Declaraciones.Repositorios;
using Entidades;
using Entidades.Modelo.Extensiones;
using Entidades.Modelos;
using Entidades.Utilidades.Paginado;
using Entidades.Utilidades.Paginado.Interfaces;
using Microsoft.EntityFrameworkCore;
using Repositorio.Base;
using Repositorio.Utilidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositorios {
    public class BugRepositorio : RepositorioBase<Bug>, IBugRepositorio {
        private readonly ContextoBD _contextDb;

        public BugRepositorio(ContextoBD contextoBD) : base(contextoBD) { 
            this._contextDb = contextoBD;
        }

        /// <summary>
        /// Método que permite hacer una actualización en la base de datos de un Bug
        /// </summary>
        /// <param name="bugBD">Objeto Bug con datos provenientes de la base de datos</param>
        /// <param name="bug">Objeto Bug con los datos a actualizar</param>
        public void ActualizarBug(Bug bugBD, Bug bug) {
            bugBD.Mapeo(bug);
            this.Actualizar(bug);
        }

        /// <summary>
        /// Método que permite crear un registro de un Bug en la base de datos
        /// </summary>
        /// <param name="bug">Objeto Bug</param>
        public void CrearBug(Bug bug) {
            this.Crear(bug);
        }

        /// <summary>
        /// Método que permite eliminar un registro de la base de datos de un Bug
        /// </summary>
        /// <param name="bug">Objeto Bug</param>
        public void EliminarBug(Bug bug) {
            this.Eliminar(bug);
        }

        /// <summary>
        /// Método que permite obtener de la base de datos una lista con todas los Bug
        /// </summary>
        /// <param name="pagina">Página Actual</param>
        /// <param name="tamanoPagina">Elementos por Página</param>
        /// <returns>Lista de Bug</returns>
        public IEnumerable<Bug> ObtenerTodos(int? pagina = null, int? tamanoPagina = null) {
            var Bug = this.EncontrarTodos();
            if (pagina.HasValue && tamanoPagina.HasValue) {
            return Bug.ObtenerListaPaginada(pagina.Value, tamanoPagina.Value);
            }

            return Bug.AsEnumerable().ToList();
        }

        /// <summary>
        /// Método asincrono que permite obtener de la base de datos una lista con todas los Bug
        /// </summary>
        /// <param name="pagina">Página Actual</param>
        /// <param name="tamanoPagina">Elementos por Página</param>
        /// <returns>Lista de Bug</returns>
        public async Task<IEnumerable<Bug>> ObtenerTodosAsinc(int? pagina = null, int? tamanoPagina = null) {
            var Bug = this.EncontrarTodos();
            if (pagina.HasValue && tamanoPagina.HasValue) {
            return Bug.ObtenerListaPaginada(pagina.Value, tamanoPagina.Value);
            }

            return await Task.FromResult(Bug.AsEnumerable().ToList());
        }

        /// <summary>
        /// Método que permite obtener de la base de datos un objeto de Paginación con la lista de los Bug
        /// </summary>
        /// <param name="pagina">Página Actual</param>
        /// <param name="tamanoPagina">Elementos por Página</param>
        /// <returns>Objeto de Paginación con la Lista de Bug</returns>
        public IResultadoPaginado<Bug> ObtenerTodosPaginado(int? pagina = null, int? tamanoPagina = null) {
            var Bug = this.EncontrarTodos();
            if (pagina.HasValue && tamanoPagina.HasValue) {
            return Bug.ObtenerPaginado(pagina.Value, tamanoPagina.Value);
            }

            //await Task.FromResult(listaAsignacionConsulta.AsEnumerable().ToList());
            return new ResultadoPaginado<Bug> {
            ContadorFilas = Bug.Count(),
            Resultados = Bug.AsEnumerable().ToList()
            };
        }

        /// <summary>
        /// Método que permite obtener de la base de datos un bug por medio de su Id
        /// </summary>
        /// <param name="idBug">Id de bug</param>
        /// <returns>Objeto bug</returns>
        public Bug ObtenerBugPorId(int idBug) {
            var bugEncontrado = this.EncontrarPorCondicion(bug => bug.Id.Equals(idBug));
            return bugEncontrado.AsEnumerable().DefaultIfEmpty(new Bug()).FirstOrDefault();
        }

        /// <summary>
        /// Método asíncrono que permite obtener de la base de datos un bug por medio de su Id
        /// </summary>
        /// <param name="idBug">Id de bug</param>
        /// <returns>Objeto bug</returns>
        public async Task<Bug> ObtenerBugPorIdAsinc(int idBug) {
            var bugEncontrado = this.EncontrarPorCondicion(bug => bug.Id.Equals(idBug));
            return await Task.FromResult(bugEncontrado.AsEnumerable().DefaultIfEmpty(new Bug()).FirstOrDefault());
        }

        /// <summary>
        /// Método de implementación asíncrono que permite obtener de la base de datos un Bug por medio del
        /// id de proyecto y usuario
        /// </summary>
        /// <param name="idUsuario">idUsuario</param>
        /// <param name="idProyecto">idProyecto</param>
        /// <returns>Bug</returns>
        public async Task<Bug> ObtenerBugPorUsuarioYProyetoAsinc(int idUsuario, int idProyecto) {
            var Bugs = _contextDb.Bug
                .Include(p => p.Proyecto)
                .Include(u => u.Usuario)
                .Where(b => b.ProyectoId == idProyecto && b.UsuarioId == idUsuario);
            return await Task.FromResult(Bugs.AsEnumerable().DefaultIfEmpty(null).FirstOrDefault());
        }

        /// <summary>
        /// Método de implementación asíncrono que permite obtener de la base de datos  Bugs por medio del
        /// id de proyecto y usuario
        /// </summary>
        /// <param name="idUsuario">idUsuario</param>
        /// <param name="idProyecto">idProyecto</param>
        /// <returns>Lista de Bugs</returns>
        public async Task<IEnumerable<Bug>> ObtenerBugsPorUsuarioYProyetoAsinc(int idUsuario, int idProyecto) {
            var Bugs = _contextDb.Bug
                .Include("Proyecto")
                .Include("Usuario")
                .Where(b => b.ProyectoId == idProyecto && b.UsuarioId == idUsuario);
            return await Task.FromResult(Bugs.AsEnumerable());
        }

        /// <summary>
        /// Método de implementación asíncrono que permite obtener de la base de datos los Bugs por medio del
        /// id de proyecto
        /// </summary>
        /// <param name="idProyecto">idProyecto</param>
        /// <returns>Lista de Bugs</returns>
        public async Task<IEnumerable<Bug>> ObtenerBugsPorProyetoAsinc(int idProyecto) {
            var Proyecto = _contextDb.Projects
                .Include("Bugs.Usuario")
                .FirstOrDefault(p => p.Id == idProyecto);

            var Bugs = Proyecto != null ? Proyecto?.Bugs : new List<Bug>();

            return await Task.FromResult(Bugs.AsEnumerable());
        }

        /// <summary>
        /// Método de implementación asíncrono que permite obtener de la base de datos los Bugs por medio del
        /// id de usuario
        /// </summary>
        /// <param name="idUsuario">idUsuario</param>
        /// <returns>Lista de Bugs</returns>
        public async Task<IEnumerable<Bug>> ObtenerBugsPorUsuarioAsinc(int idUsuario) {
            var Bugs = _contextDb.Bug
                .Include("Usuario")
                .Include("Proyecto")
                .Where(p => p.UsuarioId == idUsuario);

            return await Task.FromResult(Bugs.AsEnumerable());
        }
    }
}