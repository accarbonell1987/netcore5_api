using BugsAPI.Ayudas;
using Microsoft.AspNetCore.Builder;

namespace BugsAPI.Extensiones
{
    /// <summary>
    /// Extension para App
    /// </summary>
    public static class AppExtension
    {
        /// <summary>
        /// Función que permite configurar los middlewares a usar con la App
        /// </summary>
        /// <param name="app"></param>
        public static void UseMiddlewares(this IApplicationBuilder app)
        {
            app.UseMiddleware<ManipuladorErroresMiddleware>();
        }
    }
}
