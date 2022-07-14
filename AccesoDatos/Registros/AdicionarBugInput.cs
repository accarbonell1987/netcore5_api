using System.ComponentModel.DataAnnotations;

namespace AccesoDatos.Registros {
    public class AdicionarBugInput {
        [Required]
        public int IdUser { get; set; }
        [Required]
        public int IdProject { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
