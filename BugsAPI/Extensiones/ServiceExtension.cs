using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

using Entidades;
using Repositorio.Contenedores.Interfaces;
using Repositorio.Contenedores;
using ReglasNegocio.Contenedor.Interfaces;
using ReglasNegocio.Contenedor;
using ReglasDeNegocio;

namespace BugsAPI.Extensiones {
    public static class ServiceExtension {
        /// <summary>
        /// Configura los Cors de Services.
        /// </summary>
        /// <param name="services">Services</param>
        public static void ConfigurarCors(this IServiceCollection services) {
            services.AddCors(options => {
                options.AddPolicy("CorsPolicy", policy => {
                    policy
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowAnyOrigin();
                        //.WithOrigins("https://localhost:3000", "http://localhost:3000");
                    });
                }
            );
        }

        /// <summary>
        /// Configura el contexto de la conexion para las Base de datos SQL, conjunto con el string de conexión.
        /// </summary>
        /// <param name="services">Services</param>
        /// <param name="config">Configuration</param>
        public static void ConfigurarContextoSQLServer(this IServiceCollection services, IConfiguration config) {
          // Add framework services.
          services.AddDbContext<ContextoBD>(options =>
              options.UseSqlServer(config.GetConnectionString("SQLLocalDBConnection")));
        }

        /// <summary>
        /// Register the Swagger generator, defining one or more Swagger documents
        /// </summary>
        /// <param name="services">Services</param>
        public static void ConfigurarSwaggerGen(this IServiceCollection services) {
          services.AddSwaggerGen(c => {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "BugsAPI", Version = "v1" });
          });
        }

        /// <summary>
        /// Configura los Contenedores de los Repositorios.
        /// </summary>
        /// <param name="services">Services</param>
        public static void ConfiguraContenedoresRepositorios(this IServiceCollection services) {
            services.AddScoped<IContenedorRepositorio, ContenedorRepositorio>();
        }

        /// <summary>
        /// Configura toda la capa de Reglas de Negocios
        /// </summary>
        /// <param name="services">Services.</param>
        public static void ConfigurarCapaReglaNegocios(this IServiceCollection services) {
            services.AddScoped<IContenedorReglasNegocios, ContenedorReglasNegocios>();
            services.AddScoped<ContenedorReglasNegocios>();
            services.AddScoped<BugRN>();
            services.AddScoped<UsuarioRN>();
            services.AddScoped<ProyectoRN>();
        }

        /// <summary>
        /// Configura Los controladores, para que puedan ser usados por NewtonsoftJson
        /// </summary>
        /// <param name="services">Services.</param>
        public static void ConfiguarControladoresConJson(this IServiceCollection services) {
            services.AddControllers()
                    .AddNewtonsoftJson(x => {
                        x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    });
        }
    }
}