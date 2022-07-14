using Microsoft.Extensions.Logging;
using ReglasDeNegocio;
using ReglasNegocio.Contenedor.Interfaces;
using Repositorio.Contenedores.Interfaces;

namespace ReglasNegocio.Contenedor
{
    /// <summary>
    /// Clase Factoría para las clases de  las Reglas de Negocios
    /// </summary>
    public class ContenedorReglasNegocios : IContenedorReglasNegocios
    {
        private readonly IContenedorRepositorio _contenedorRepositorio;
        private readonly ILogger<ContenedorReglasNegocios> _logger;

        public ContenedorReglasNegocios(
            IContenedorRepositorio contenedorRepositorio, ILogger<ContenedorReglasNegocios> logger) {
            _contenedorRepositorio = contenedorRepositorio;
            _logger = logger;
        }

        private BugRN _bugRN;
        private ProyectoRN _proyectoRN;
        private UsuarioRN _usuarioRN;

        #region Instancias del Factory
        public BugRN BugRN
        {
            get
            {
                if (_bugRN == null)
                {
                    _bugRN = new BugRN(_contenedorRepositorio, _logger);
                }
                return _bugRN;
            }
        }

        public ProyectoRN ProyectoRN {
            get {
                if (_proyectoRN == null) {
                    _proyectoRN = new ProyectoRN(_contenedorRepositorio, _logger);
                }
                return _proyectoRN;
            }
        }

        public UsuarioRN UsuarioRN {
            get {
                if (_usuarioRN == null) {
                    _usuarioRN = new UsuarioRN(_contenedorRepositorio, _logger);
                }
                return _usuarioRN;
            }
        }
        #endregion Instancias del Factory

        public IContenedorRepositorio ContenedorRepositorio
        {
            get => _contenedorRepositorio;
        }
    }
}
