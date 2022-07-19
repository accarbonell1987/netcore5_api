using System.ComponentModel.DataAnnotations;

namespace AccesoDatos.Registros {
    public class AdicionarBugInput {
        [Required]
        public int user { get; set; }
        [Required]
        public int project { get; set; }
        [Required]
        public string description { get; set; }
    }
}
