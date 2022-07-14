using AccesoDatos.Registros;
using Entidades.Ayudas;
using Entidades.Modelos;
using Microsoft.Extensions.Logging;
using ReglasNegocio.Contenedor;
using Repositorio.Contenedores.Interfaces;
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
            var bug = proyecto.Bugs.FirstOrDefault(p => p.UserOfBugId == usuario.IdUsuario);

            //si existe lo actualizo sino lo creo
            if (!bug.EsObjetoNulo()) {
                bug.DescripcionBug = descripcion;
                bug.CreacionBug = System.DateTime.Now;
            } else {
                proyecto.Bugs.Add(new Bug() {
                    CreacionBug = System.DateTime.Now,
                    DescripcionBug = descripcion,
                    Proyecto = proyecto,
                    Usuario = usuario
                });
            }

            await _contenedorRepositorio.GuardarAsinc();
        }
    }
}
