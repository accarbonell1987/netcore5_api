using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Registros {
    public class ResponseUser {
        [JsonProperty]
        public int Id { get; set; }
        [JsonProperty]
        public string Nombres { get; set; }
        [JsonProperty]
        public string Apellidos { get; set; }
        [JsonProperty]
        public string NombreYApellidos { get => $"{Nombres} {Apellidos}"; }
    }
}