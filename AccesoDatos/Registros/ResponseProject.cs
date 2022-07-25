using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Registros {
    public class ResponseProject {
        [JsonProperty]
        public int Id { get; set; }
        [JsonProperty]
        public string NombreProyecto { get; set; }
        [JsonProperty]
        public string DescripcionProyecto { get; set; }
    }
}