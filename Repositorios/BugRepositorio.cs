﻿using Declaraciones.Repositorios;
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
    public class BugRepositorio : RepositorioBase<Bug>, IBugRepositorio {

        public BugRepositorio(ContextoBD contextoBD) : base(contextoBD) { }

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
            var bugEncontrado = this.EncontrarPorCondicion(bug => bug.IdBug.Equals(idBug));
            return bugEncontrado.AsEnumerable().DefaultIfEmpty(new Bug()).FirstOrDefault();
        }

        /// <summary>
        /// Método asíncrono que permite obtener de la base de datos un bug por medio de su Id
        /// </summary>
        /// <param name="idBug">Id de bug</param>
        /// <returns>Objeto bug</returns>
        public async Task<Bug> ObtenerBugPorIdAsinc(int idBug) {
            var bugEncontrado = this.EncontrarPorCondicion(bug => bug.IdBug.Equals(idBug));
            return await Task.FromResult(bugEncontrado.AsEnumerable().DefaultIfEmpty(new Bug()).FirstOrDefault());
        }
    }
}