using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades.Modelos {
    [Table("Bug")]
    public class Bug {
        #region Llaves
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        #endregion

        #region Propiedades
        public string DescripcionBug { get; set; }

        public DateTime CreacionBug { get; set; }
        #endregion

        #region Relaciones
        public int ProyectoId { get; set; }
        public Project Proyecto { get; set; }

        public int UsuarioId { get; set; }
        public User Usuario { get; set; }
        #endregion
    }
}
