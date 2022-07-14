using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

using Entidades.Utilidades;

namespace BugsAPI.Ayudas {
  public static class Inicializacion {
    public static void InicializarBaseDatos(IApplicationBuilder applicationBuilder) {
      using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope()) {
        InicializacionBaseDatos.Seed(serviceScope);
      }
    }
  }
}
