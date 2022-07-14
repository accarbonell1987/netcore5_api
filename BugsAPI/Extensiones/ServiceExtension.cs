﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

using Entidades;

namespace BugsAPI.Extensiones {
  public static class ServiceExtension {
    /// <summary>
    /// Configura los Cors de Services.
    /// </summary>
    /// <param name="services">Services</param>
    public static void ConfigurarCors(this IServiceCollection services) {
      services.AddCors(options =>
      {
        options.AddPolicy("CorsPolicy",
            builder => builder
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithOrigins("https://localhost:3000", "http://localhost:3000"));
      });
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
  }
}