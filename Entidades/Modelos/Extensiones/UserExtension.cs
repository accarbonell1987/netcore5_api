using Entidades.Modelos;

namespace Entidades.Modelo.Extensiones
{
    /// <summary>
    /// Clase de Extensión para la Clase de Entidad Usuario
    /// </summary>
    public static class UserExtension
    {
        /// <summary>
        /// Método que mapea un objeto de entidad Usuario dentro de otro objeto Usuario
        /// </summary>
        /// <param name="usuarioBD">Objeto Usuario a mapear con los datos del otro objeto</param>
        /// <param name="usuario">Objeto Usuario con datos a mapear</param>
        public static void Mapeo(this User usuarioBD, User usuario)
        {
            usuarioBD.Nombres = usuario.Nombres;
            usuarioBD.Apellidos = usuario.Apellidos;
        }
    }
}
