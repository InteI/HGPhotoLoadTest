using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HGApi.Models {

    [JsonObject(MemberSerialization.OptIn)]
    public class Error {
        [JsonProperty("error")]
        public string Message { get; set; }
    }
}
