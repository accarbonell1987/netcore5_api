using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades.Modelos {
  
    [Table("Proyecto")]
    public class Project {
        #region Propiedades
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdProyecto { get; set; }

        public string NombreProyecto { get; set; }

        public string DescripcionProyecto { get; set; }
        #endregion

        #region Relaciones
        public virtual ICollection<Bug> Bugs { get; set; }
        #endregion

        #region Constructor
        public Project() {
            Bugs = new List<Bug>();
        }
        #endregion
    }
}
