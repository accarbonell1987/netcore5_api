﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades.Modelos {
    [Table("Usuario")]
    public class User {
        #region Llaves
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        #endregion

        #region Propiedades
        public string Nombres { get; set; }

        public string Apellidos { get; set; }
        #endregion

        #region Relaciones
        public IList<Bug> Bugs { get; set; }
        #endregion

        #region Metodos
        public string NombreYApellidos { get => $"{Nombres} {Apellidos}"; }
        #endregion
    }
}
