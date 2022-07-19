using Declaraciones.Repositorios;
using Entidades;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Repositorio.Contenedores.Interfaces;
using Repositorios;
using System.Threading.Tasks;

namespace Repositorio.Contenedores
{
    /// <summary>
    /// Clase Factoria de los repositorios
    /// </summary>
    public class ContenedorRepositorio : IContenedorRepositorio
    {
        private readonly ContextoBD contextoBD;
        private readonly ILogger _log;

        public ContenedorRepositorio(ILogger<ContenedorRepositorio> log,
                                    ContextoBD contextoBD)
        {
            this.contextoBD = contextoBD;
            _log = log;
        }

        // Agregar repositorios de entidades de tipo privado
        private IUsuarioRepositorio usuario;
        private IBugRepositorio bug;
        private IProyectoRepositorio proyecto;

        #region ImplementacionesInterface
        public IUsuarioRepositorio Usuario
        {
            get
            {
                if (this.usuario == null)
                {
                    this.usuario = new UsuarioRepositorio(this.contextoBD);
                }
                return this.usuario;
            }
        }

        public IBugRepositorio Bug {
            get {
                if (this.bug == null) {
                    this.bug = new BugRepositorio(this.contextoBD);
                }
                return this.bug;
            }
        }

        public IProyectoRepositorio Proyecto {
            get {
                if (this.proyecto == null) {
                    this.proyecto = new ProyectoRepositorio(this.contextoBD);
                }
                return this.proyecto;
            }
        }

        /// <summary>
        /// Método para guardar cambios en los repositorios.
        /// </summary>
        public void Guardar()
            {
                this.contextoBD.SaveChanges();
            }

        /// <summary>
        /// Método para guardar cambios en los repositorios.
        /// </summary>
        public async Task GuardarAsinc()
        {
            await this.contextoBD.SaveChangesAsync();
        }

        public IDbContextTransaction IniciarTransaccion()
        {
            return this.contextoBD.Database.BeginTransaction();
        }

        public async Task<IDbContextTransaction> IniciarTransaccionAsinc()
        {
            return await this.contextoBD.Database.BeginTransactionAsync();
        }
        #endregion ImplementacionesInterface
    }
}
