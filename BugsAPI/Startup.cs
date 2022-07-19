using BugsAPI.Ayudas;
using BugsAPI.Extensiones;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BugsAPI {
    public class Startup {
        #region Atributos
        public IConfiguration Configuration { get; }
        #endregion

        #region Constructor
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }
        #endregion

        #region Servicios
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.ConfigurarCors();
            services.ConfigurarContextoSQLServer(Configuration);
            services.ConfiguraContenedoresRepositorios();
            services.ConfigurarCapaReglaNegocios();
            services.AddControllers();
            services.ConfigurarSwaggerGen();
            services.ConfiguarControladoresConJson();
        }
        #endregion

        #region Configuracion
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BugsAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseMiddlewares();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });

            //Inicializar datos en base de datos
            Inicializacion.InicializarBaseDatos(app);
        }
        #endregion
    }
}
