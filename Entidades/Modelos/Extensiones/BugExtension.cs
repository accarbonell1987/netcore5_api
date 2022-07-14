using Entidades.Modelos;

namespace Entidades.Modelo.Extensiones
{
    /// <summary>
    /// Clase de Extensión para la Clase de Entidad Bug
    /// </summary>
    public static class BugExtension
    {
        /// <summary>
        /// Método que mapea un objeto de entidad Usuario dentro de otro objeto Bug
        /// </summary>
        /// <param name="bugBD">Objeto Bug a mapear con los datos del otro objeto</param>
        /// <param name="bug">Objeto Bug con datos a mapear</param>`
        public static void Mapeo(this Bug bugBD, Bug bug)
        {
            bugBD.DescripcionBug = bug.DescripcionBug;
        }
    }
}
