using AccesoDatos.Registros;
using Entidades.Ayudas;
using Entidades.Modelos;
using Microsoft.Extensions.Logging;
using ReglasNegocio.Contenedor;
using Repositorio.Contenedores.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReglasDeNegocio {
    /// <summary>
    /// Clase de Reglas de Negocios para los Bugs
    /// </summary>
    public class BugRN {
        private readonly IContenedorRepositorio _contenedorRepositorio;
        private readonly ILogger _log;

        public BugRN(IContenedorRepositorio contenedorRepositorio, ILogger<ContenedorReglasNegocios> log) {
            _contenedorRepositorio = contenedorRepositorio;
            _log = log;
        }

        public async Task AdicionarBug(Project proyecto, User usuario, string descripcion) {
            //buscar un bug que este asignado al usuario dentro del proyecto
            var bug = await _contenedorRepositorio.Bug.ObtenerBugPorUsuarioYProyetoAsinc(usuario.IdUsuario, proyecto.IdProyecto);

            //si existe lo actualizo sino lo creo
            if (!bug.EsObjetoNulo()) {
                bug.DescripcionBug = descripcion;
                bug.CreacionBug = System.DateTime.Now;
            } else {
                _contenedorRepositorio.Bug.CrearBug(new Bug() {
                    CreacionBug = System.DateTime.Now,
                    DescripcionBug = descripcion,
                    UsuarioId = usuario.IdUsuario,
                    ProyectoId = proyecto.IdProyecto,
                });
            }

            await _contenedorRepositorio.GuardarAsinc();
        }

        

        public async Task<IEnumerable<Bug>> ObtenerBugsPorProyecto(int project_id) {
            try {
                var proyecto = await _contenedorRepositorio.Proyecto.ObtenerProyectoPorIdAsinc(project_id);

                if (proyecto == null) return new List<Bug>();

                return proyecto.Bugs;
            } catch (Exception ex) {
                _log.LogInformation($"Error en la Regla de Negocios: BugRN.ObtenerBugsPorProyecto: {ex.Message} {ex.InnerException.Message}");
                throw new Exception(ex.Message);
            }
        }

        public async Task<Bug> ObtenerBugPorUsuario(int? user_id) {
            try {
                if (user_id == null) return null; 
                
                var usuario = await _contenedorRepositorio.Usuario.ObtenerUsuarioPorIdAsinc((int)user_id);
                return usuario?.UserBug;
            } catch (Exception ex) {
                _log.LogInformation($"Error en la Regla de Negocios: BugRN.ObtenerBugsPorUsuario: {ex.Message} {ex.InnerException.Message}");
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Bug>> ObtenerBugsPorProyectoYUsuario(int project_id, int user_id) {
            try {
                var proyecto = await _contenedorRepositorio.Proyecto.ObtenerProyectoPorIdAsinc(project_id);

                if (proyecto == null) return new List<Bug>();

                return proyecto.Bugs.Where(p => p.UsuarioId == user_id).ToList();
            } catch (Exception ex) {
                _log.LogInformation($"Error en la Regla de Negocios: BugRN.ObtenerBugsPorProyectoYUsuario: {ex.Message} {ex.InnerException.Message}");
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Bug>> ObtenerBugsPorProyectoUsuarioFecha(int project_id, int user_id, DateTime startDate, DateTime endDate) {
            try {
                var proyecto = await _contenedorRepositorio.Proyecto.ObtenerProyectoPorIdAsinc(project_id);

                if (proyecto == null) return new List<Bug>();

                return proyecto.Bugs.Where(p => p.UsuarioId == user_id && p.CreacionBug >= startDate && p.CreacionBug <= endDate).ToList();
            } catch (Exception ex) {
                _log.LogInformation($"Error en la Regla de Negocios: BugRN.ObtenerBugsPorProyectoUsuarioFecha: {ex.Message} {ex.InnerException.Message}");
                throw new Exception(ex.Message);
            }
        }
    }
}
