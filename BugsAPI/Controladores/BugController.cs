using AccesoDatos.Registros;
using Entidades;
using Entidades.Ayudas;
using Entidades.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReglasNegocio.Contenedor;
using Repositorio.Contenedores.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugsAPI.Controladores {
    [Route("api/")]
    [ApiController]
    public class BugController : ControllerBase {
        private readonly ILogger<BugController> _logger;
        private readonly ContenedorReglasNegocios _reglasNegocios;

        public BugController(ILogger<BugController> logger, ContenedorReglasNegocios reglasNegocios) {
            _logger = logger;
            _reglasNegocios = reglasNegocios;
        }

        [HttpPost("bug")]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AdicionarBug([FromBody] AdicionarBugInput data) {
            try {
                if (!ModelState.IsValid) {
                    return StatusCode(StatusCodes.Status400BadRequest, "Error en la entrada de datos.");
                }

                var proyecto = await _reglasNegocios.ProyectoRN.ObtenerProyectoPorIdAsinc(data.project);
                if (proyecto.EsObjetoNulo()) return NoContent();

                var usuario = await _reglasNegocios.UsuarioRN.ObtenerUsuarioPorIdAsinc(data.user);
                if (usuario.EsObjetoNulo()) return NoContent();

                var bug = await _reglasNegocios.BugRN.AdicionarBug(proyecto, usuario, data.description);
                return Ok(bug);
            } catch (Exception ex) {
                _logger.LogError($"ERROR en controlador BugController.AdicionarBug {ex.Message} {ex.InnerException.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Falla del origen de datos.");
            }
        }

        //•	at least one parameter is required;
        //•	only the GET method is allowed, otherwise, a 405 status code is returned;
        //•	if no bugs were found for filter conditions, a 404 status code is returned;
        //•	Otherwise, you should return a 200 status code and response in the following JSON format:

        [HttpGet("bugs")]
        [ProducesResponseType(typeof(ResponseBug), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ObtenerBugsPorProyeto(
            [FromQuery] int? project_id,
            [FromQuery] int? user_id,
            [FromQuery] string start_date,
            [FromQuery] string end_date) {
            try {
                //validar encabezados
                bool parametrosCorrectos = ParametrosHeaderCorrectos(project_id, user_id, start_date, end_date);
                if (!parametrosCorrectos) return StatusCode(StatusCodes.Status404NotFound, "Falla en los encabezados.");
                
                IEnumerable<Bug> bugs = new List<Bug>();

                //busqueda cuando existe la doble condicion de usuario y proyecto
                if (project_id.HasValue && user_id.HasValue) 
                    bugs = await _reglasNegocios.BugRN.ObtenerBugsPorProyectoYUsuario((int)project_id, (int)user_id);

                //cuando solo existe proyecto
                if (!bugs.Any() && project_id.HasValue) 
                    bugs = await _reglasNegocios.BugRN.ObtenerBugsPorProyecto((int)project_id);

                //cuando solo existe usuario
                if (!bugs.Any() && user_id.HasValue)
                    bugs = await _reglasNegocios.BugRN.ObtenerBugsPorUsuario((int)user_id);

                //si existen rango de fechas
                if (bugs.Any()) {
                    //si viene con rango de fecha
                    var validoStartDate = DateTime.TryParse(start_date, out DateTime convertidoStartDate);
                    var validoEndDate = DateTime.TryParse(end_date, out DateTime convertidoEndDate);

                    if (validoStartDate && validoEndDate && convertidoEndDate > convertidoStartDate) {
                        bugs = bugs.Where(p => p.CreacionBug >= convertidoStartDate && p.CreacionBug <= convertidoEndDate);
                    }
                } else 
                    return StatusCode(StatusCodes.Status404NotFound, "No existen datos para mostrar");

                //mapear la respuesta al formato requerido
                IEnumerable<ResponseBug> responseBugs = bugs.Select(
                    p => new ResponseBug {
                        Id = p.Id,
                        Description = p.DescripcionBug,
                        Project = p.Proyecto.DescripcionProyecto,
                        Username = p.Usuario.NombreYApellidos,
                        CreationDate = user_id.HasValue ? p.CreacionBug.ToString("dd/MM/yyyy HH:mm:ss") : null,
                    });

                return Ok(responseBugs);
            } catch (Exception ex) {
                _logger.LogInformation($"ERROR en controlador BugController.ObtenerBugs {ex.Message} {ex.InnerException.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Falla del origen de datos.");
            }
        }

        [HttpGet("bugs/detail")]
        [ProducesResponseType(typeof(Bug), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ObtenerBugsPorProyetoDetallado(
            [FromQuery] int? project_id,
            [FromQuery] int? user_id,
            [FromQuery] string start_date,
            [FromQuery] string end_date) {
            try {
                //validar encabezados
                bool parametrosCorrectos = ParametrosHeaderCorrectos(project_id, user_id, start_date, end_date);
                if (!parametrosCorrectos) return StatusCode(StatusCodes.Status404NotFound, "Falla en los encabezados.");

                IEnumerable<Bug> bugs = new List<Bug>();

                //busqueda cuando existe la doble condicion de usuario y proyecto
                if (project_id.HasValue && user_id.HasValue)
                    bugs = await _reglasNegocios.BugRN.ObtenerBugsPorProyectoYUsuario((int)project_id, (int)user_id);

                //cuando solo existe proyecto
                if (!bugs.Any() && project_id.HasValue)
                    bugs = await _reglasNegocios.BugRN.ObtenerBugsPorProyecto((int)project_id);

                //cuando solo existe usuario
                if (!bugs.Any() && user_id.HasValue)
                    bugs = await _reglasNegocios.BugRN.ObtenerBugsPorUsuario((int)user_id);

                //si existen rango de fechas
                if (bugs.Any()) {
                    //si viene con rango de fecha
                    var validoStartDate = DateTime.TryParse(start_date, out DateTime convertidoStartDate);
                    var validoEndDate = DateTime.TryParse(end_date, out DateTime convertidoEndDate);

                    if (validoStartDate && validoEndDate && convertidoEndDate > convertidoStartDate) {
                        bugs = bugs.Where(p => p.CreacionBug >= convertidoStartDate && p.CreacionBug <= convertidoEndDate);
                    }
                } else
                    return StatusCode(StatusCodes.Status404NotFound, "No existen datos para mostrar");

                return Ok(bugs);
            } catch (Exception ex) {
                _logger.LogInformation($"ERROR en controlador BugController.ObtenerBugs {ex.Message} {ex.InnerException.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Falla del origen de datos.");
            }
        }

        private bool ParametrosHeaderCorrectos(int? project_id, int? user_id, string start_date, string end_date) {
            try {
                var validoStartDate = DateTime.TryParse(start_date, out DateTime convertidoStartDate);
                var validoEndDate = DateTime.TryParse(end_date, out DateTime convertidoEndDate);

                bool correct = project_id.HasValue || user_id.HasValue;

                if (validoStartDate && validoEndDate)
                    correct = convertidoEndDate > convertidoStartDate;

                return correct;
            } catch (Exception ex) {
                _logger.LogInformation($"Error en controlador BugController.ParametrosHeaderCorrectos: {ex.Message} {ex.InnerException.Message}");
                throw new Exception(ex.Message);
            }
        }
    }
}
