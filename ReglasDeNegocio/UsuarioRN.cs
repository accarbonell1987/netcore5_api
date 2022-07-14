using Entidades.Modelos;
using Microsoft.Extensions.Logging;
using ReglasNegocio.Contenedor;
using Repositorio.Contenedores.Interfaces;
using System;
using System.Threading.Tasks;

namespace ReglasDeNegocio {
    /// <summary>
    /// Clase de Reglas de Negocios para los Usuario
    /// </summary>
    public class UsuarioRN{
        private readonly IContenedorRepositorio _contenedorRepositorio;
        private readonly ILogger _log;

        public UsuarioRN(IContenedorRepositorio contenedorRepositorio, ILogger<ContenedorReglasNegocios> log) {
            _contenedorRepositorio = contenedorRepositorio;
            _log = log;
        }

        /// <summary>
        /// Función que permite obtener un usuario de acuerdo al ID enviado
        /// </summary>
        /// <param name="idUsuario">Id del usuario buscado</param>
        /// <returns>User</returns>
        public async Task<User> ObtenerUsuarioPorIdAsinc(int idUsuario) {
            try {
                var usuario = await _contenedorRepositorio.Usuario.ObtenerUsuarioPorIdAsinc(idUsuario);
                return usuario;
            } catch (Exception ex) {
                _log.LogInformation($"Error en la Regla de Negocios: UsuarioRN.ObtenerUsuarioPorIdAsinc: {ex.Message} {ex.InnerException}");
                throw new Exception(ex.Message);
            }
        }
    }
}
