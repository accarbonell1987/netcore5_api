using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Registros {
    public class ResponseBug {
        [JsonProperty]
        public int Id { get; set; }
        [JsonProperty]
        public string Description { get; set; }
        [JsonProperty]
        public string Username { get; set; }
        [JsonProperty]
        public string Project { get; set; }
        [JsonProperty]
        public string CreationDate { get; set; }
    }
}