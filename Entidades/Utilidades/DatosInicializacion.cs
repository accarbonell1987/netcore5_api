using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Entidades.Modelos;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Entidades.Utilidades {
  public static class InicializacionBaseDatos {
    private static Project[] ObtenerProyectosEstaticos() {
      ICollection<Project> projects = new List<Project>() {
        new Project() {
          NombreProyecto = "War Thunder",
          DescripcionProyecto = "Juego Free-to-Play MMO, Gaijim",
        },
        new Project() {
          NombreProyecto = "World of Warcraft",
          DescripcionProyecto = "Juego MMO RolePlay, Blizzard",
        },
        new Project() {
          NombreProyecto = "Dota2",
          DescripcionProyecto = "Juego Free-to-Play MMO, Valve",
        },
        new Project() {
          NombreProyecto = "Cuphead",
          DescripcionProyecto = "Juego Multiplayer, Run-and-gun, Microsoft",
        },
        new Project() {
          NombreProyecto = "God of War",
          DescripcionProyecto = "Juego Single player, Sony",
        }
      };
      return projects.ToArray();
    }

    private static User[] ObtenerUsuariosEstaticos() {
      ICollection<User> users = new List<User>() {
        new User() {
          Nombres = "Francis",
          Apellidos = "Copola"
        },
        new User() {
          Nombres = "Michael",
          Apellidos = "Bay"
        },
        new User() {
          Nombres = "George",
          Apellidos = "Lucas"
        },
        new User() {
          Nombres = "Steven",
          Apellidos = "Spilberg"
        },
        new User() {
          Nombres = "Andrew",
          Apellidos = "Stanton"
        },
      };
      return users.ToArray();
    }

    private static void InicializarUsuarios(ContextoBD contextoDB) {
      contextoDB.Users.AddRange(ObtenerUsuariosEstaticos());
      contextoDB.SaveChanges();
      Console.WriteLine("Inicializacion: Datos Usuarios Creados");
    }

    private static void InicializarProyectos(ContextoBD contextoDB) {
      contextoDB.Projects.AddRange(ObtenerProyectosEstaticos());
      contextoDB.SaveChanges();
      Console.WriteLine("Inicializacion: Datos Proyectos Creados");
    }

    public static void Seed(IServiceScope serviceScope) {
      using (var contextoDB = serviceScope.ServiceProvider.GetService<ContextoBD>()) {
        //crear la base de datos
        contextoDB.Database.EnsureCreated();

        //revisar que existan usuarios
        if (!contextoDB.Users.Any()) InicializarUsuarios(contextoDB);
        if (!contextoDB.Projects.Any()) InicializarProyectos(contextoDB);
      }
    }
  }
}
