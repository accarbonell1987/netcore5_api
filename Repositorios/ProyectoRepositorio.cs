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
    public class ProyectoRepositorio : RepositorioBase<Project>, IProyectoRepositorio {
        private readonly ContextoBD _contextDb;
        
        public ProyectoRepositorio(ContextoBD contextoBD) : base(contextoBD) { 
            _contextDb = contextoBD;
        }

        /// <summary>
        /// Método que permite hacer una actualización en la base de datos de un Proyecto
        /// </summary>
        /// <param name="Proyecto">Objeto Proyecto con los datos a actualizar</param>
        public void ActualizarProyecto(Project Proyecto) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método que permite crear un registro de un Proyecto en la base de datos
        /// </summary>
        /// <param name="Proyecto">Objeto Proyecto</param>
        public void CrearProyecto(Project Proyecto) {
            this.Crear(Proyecto);
        }

        /// <summary>
        /// Método que permite eliminar un registro de la base de datos de un Proyecto
        /// </summary>
        /// <param name="Proyecto">Objeto Proyecto</param>
        public void EliminarProyecto(Project Proyecto) {
            this.Eliminar(Proyecto);
        }

        /// <summary>
        /// Método que permite obtener de la base de datos una lista con todas los Proyecto
        /// </summary>
        /// <param name="pagina">Página Actual</param>
        /// <param name="tamanoPagina">Elementos por Página</param>
        /// <returns>Lista de Proyecto</returns>
        public IEnumerable<Project> ObtenerTodos(int? pagina = null, int? tamanoPagina = null) {
            var Proyecto = this.EncontrarTodos();
            if (pagina.HasValue && tamanoPagina.HasValue) {
            return Proyecto.ObtenerListaPaginada(pagina.Value, tamanoPagina.Value);
            }

            return Proyecto.AsEnumerable().ToList();
        }

        /// <summary>
        /// Método asincrono que permite obtener de la base de datos una lista con todas los Proyecto
        /// </summary>
        /// <param name="pagina">Página Actual</param>
        /// <param name="tamanoPagina">Elementos por Página</param>
        /// <returns>Lista de Proyecto</returns>
        public async Task<IEnumerable<Project>> ObtenerTodosAsinc(int? pagina = null, int? tamanoPagina = null) {
            var Proyecto = this.EncontrarTodos();
            if (pagina.HasValue && tamanoPagina.HasValue) {
            return Proyecto.ObtenerListaPaginada(pagina.Value, tamanoPagina.Value);
            }

            return await Proyecto.ToListAsync();
        }

        /// <summary>
        /// Método que permite obtener de la base de datos un Proyecto por medio de su Id
        /// </summary>
        /// <param name="idProyecto">Id de Proyecto</param>
        /// <returns>Objeto Proyecto</returns>
        public Project ObtenerProyectoPorId(int idProyecto) {
            var ProyectoEncontrado = this.EncontrarPorCondicion(Proyecto => Proyecto.Id.Equals(idProyecto));
            return ProyectoEncontrado.AsEnumerable().DefaultIfEmpty(new Project()).FirstOrDefault();
        }

        /// <summary>
        /// Método asíncrono que permite obtener de la base de datos un Proyecto por medio de su Id
        /// </summary>
        /// <param name="idProyecto">Id de Proyecto</param>
        /// <returns>Objeto Proyecto</returns>
        public async Task<Project> ObtenerProyectoPorIdAsinc(int idProyecto) {
            var ProyectoEncontrado = _contextDb.Projects
                .Include("Bugs.Usuario")
                .FirstOrDefaultAsync(p => p.Id == idProyecto);
            return await ProyectoEncontrado;
        }

        /// <summary>
        /// Método que permite obtener de la base de datos un objeto de Paginación con la lista de los Bug
        /// </summary>
        /// <param name="pagina">Página Actual</param>
        /// <param name="tamanoPagina">Elementos por Página</param>
        /// <returns>Objeto de Paginación con la Lista de Bug</returns>
        public async Task<IResultadoPaginado<Project>> ObtenerTodosPaginado(int? pagina = null, int? tamanoPagina = null) {
            var Projects = _contextDb.Projects
                .Include("Bugs.Usuario");
            if (pagina.HasValue && tamanoPagina.HasValue) {
                return await Projects.ObtenerPaginadoAsinc(pagina.Value, tamanoPagina.Value);
            }

            return new ResultadoPaginado<Project> {
                ContadorFilas = await Projects.CountAsync(),
                Resultados = await Projects.ToListAsync()
            };
        }

    }
}