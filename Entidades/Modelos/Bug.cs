using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades.Modelos {
  [Table("Bug")]
  public class Bug {
    #region Llaves
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdBug { get; set; }
    #endregion

    #region Propiedades
    public string DescripcionBug { get; set; }

    public DateTime CreacionBug { get; set; }
    #endregion

    #region Relaciones
    public Project Proyecto { get; set; }

    public int UserOfBugId { get; set; }
    public User User { get; set; }
    #endregion
  }
}
