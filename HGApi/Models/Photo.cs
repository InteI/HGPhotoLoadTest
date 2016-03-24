using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HGApi.Models {
    [JsonObject(MemberSerialization.OptIn)]
    public class Photo {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("new_photo_id")]
        public int NewPhotoId { get; set; }

        [JsonProperty("author_id")]
        public int AuthorId { get; set; }

        [JsonProperty("file_name")]
        public string OriginalName { get; set; }

        [JsonProperty("fs_name")]
        public string FileName { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }
    }
}
