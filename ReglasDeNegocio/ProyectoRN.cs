using Entidades.Modelos;
using Entidades.Utilidades.Paginado.Interfaces;
using Microsoft.Extensions.Logging;
using ReglasNegocio.Contenedor;
using Repositorio.Contenedores.Interfaces;
using System;
using System.Threading.Tasks;

namespace ReglasDeNegocio {
    /// <summary>
    /// Clase de Reglas de Negocios para los Proyecto
    /// </summary>
    public class ProyectoRN {
        private readonly IContenedorRepositorio _contenedorRepositorio;
        private readonly ILogger _log;

        public ProyectoRN(IContenedorRepositorio contenedorRepositorio, ILogger<ContenedorReglasNegocios> log) {
            _contenedorRepositorio = contenedorRepositorio;
            _log = log;
        }

        /// <summary>
        /// Función que permite obtener un proyecto de acuerdo al ID enviado
        /// </summary>
        /// <param name="idProyecto">Id del proyecto buscado</param>
        /// <returns>Project</returns>
        public async Task<Project> ObtenerProyectoPorIdAsinc(int idProyecto) {
            try {
                var proyecto = await _contenedorRepositorio.Proyecto.ObtenerProyectoPorIdAsinc(idProyecto);
                return proyecto;
            } catch (Exception ex) {
                _log.LogInformation($"Error en la Regla de Negocios: ProyectoRN.ObtenerProyectoPorIdAsinc: {ex.Message} {ex.InnerException}");
                throw new Exception(ex.Message);
            }
        }

        public async Task<IResultadoPaginado<Project>> ObtenerTodosPaginado(int? pagina = null, int? tamanoPagina = null) {
            try {
                return await _contenedorRepositorio.Proyecto.ObtenerTodosPaginado(pagina, tamanoPagina);
            } catch (Exception ex) {
                _log.LogInformation($"Error en la Regla de Negocios: ProyectoRN.ObtenerTodosPaginado: {ex.Message} {ex.InnerException.Message}");
                throw new Exception(ex.Message);
            }
        }
    }
}
