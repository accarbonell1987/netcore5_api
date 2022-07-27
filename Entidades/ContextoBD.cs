using System;
using Microsoft.EntityFrameworkCore;
using Entidades.Modelos;
using Entidades.Utilidades;

namespace Entidades {
    /// <summary>
    /// Clase de Contexto de la Base de datos y la integración del Módelo con Entity Framework Core
    /// </summary>
    public class ContextoBD: DbContext {
        #region Constructores
        public ContextoBD() {
        }

        public ContextoBD(DbContextOptions<ContextoBD> opciones) : base(opciones) {
        }
        #endregion

        #region Declaraciones DBSet
        public DbSet<Bug> Bug { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        #endregion

        #region Overrides
        /// <summary>
        /// Método de sobrecargar que permite la utilización de FluentAPI
        /// </summary>
        /// <param name="builder">variable de construccion del EF</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Project>()
                .HasMany(p => p.Bugs)
                .WithOne(b => b.Proyecto);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Bugs)
                .WithOne(b => b.Usuario);

            //base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.EnableSensitiveDataLogging();
        }
        #endregion
  }
}
