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
    public class ProjectController : ControllerBase {
        private readonly ILogger<ProjectController> _logger;
        private readonly ContenedorReglasNegocios _reglasNegocios;

        public ProjectController(ILogger<ProjectController> logger, ContenedorReglasNegocios reglasNegocios) {
            _logger = logger;
            _reglasNegocios = reglasNegocios;
        }

        [HttpGet("projects/all")]
        [ProducesResponseType(typeof(Project), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ObtenerTodosProyectos(
            int? pagina,
            int? tamanoPagina) {
            try {
                var projects = await _reglasNegocios.ProyectoRN.ObtenerTodosPaginado(pagina, tamanoPagina);

                if (projects.EsObjetoNulo())
                    return NoContent();

                return Ok(projects);
            } catch (Exception ex) {
                _logger.LogInformation($"ERROR en controlador ProjectController.ObtenerTodosProyectos {ex.Message} {ex.InnerException.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Falla del origen de datos.");
            }
        }
    }
}
