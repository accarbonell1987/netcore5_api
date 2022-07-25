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
    public class UserController : ControllerBase {
        private readonly ILogger<UserController> _logger;
        private readonly ContenedorReglasNegocios _reglasNegocios;

        public UserController(ILogger<UserController> logger, ContenedorReglasNegocios reglasNegocios) {
            _logger = logger;
            _reglasNegocios = reglasNegocios;
        }

        [HttpGet("users/all")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ObtenerTodosUsuarios(
            int? pagina,
            int? tamanoPagina) {
            try {
                var users = await _reglasNegocios.UsuarioRN.ObtenerTodosPaginado(pagina, tamanoPagina);

                if (users.EsObjetoNulo())
                    return NoContent();

                return Ok(users);
            } catch (Exception ex) {
                _logger.LogInformation($"ERROR en controlador UserController.ObtenerTodosUsuarios {ex.Message} {ex.InnerException.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Falla del origen de datos.");
            }
        }
    }
}
