using AccesoDatos.Registros;
using Entidades.Ayudas;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReglasNegocio.Contenedor;
using Repositorio.Contenedores.Interfaces;
using System;
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
                
                var proyecto = await _reglasNegocios.ProyectoRN.ObtenerProyectoPorIdAsinc(data.IdProject);
                if (proyecto.EsObjetoNulo()) return NoContent();

                var usuario = await _reglasNegocios.UsuarioRN.ObtenerUsuarioPorIdAsinc(data.IdUser);
                if (usuario.EsObjetoNulo()) return NoContent();

                await _reglasNegocios.BugRN.AdicionarBug(proyecto, usuario, data.Description);

                return Ok();
            } catch (Exception ex) {
                _logger.LogError($"ERROR en controlador BugController.AdicionarBug {ex.Message} {ex.InnerException.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Falla del origen de datos.");
            }
        }
    }
}
