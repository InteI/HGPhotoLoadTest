using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PhotoLoader.HGApi.Models {

    [JsonObject(MemberSerialization.OptIn)]
    class User {
        public enum UserGender {
            FEMALE = 0,
            MALE = 1
        }

        [JsonObject(MemberSerialization.OptIn)]
        public class UserToken {
            [JsonProperty("access_token")]
            public string AccessToken { get; set; }

            [JsonProperty("refresh_token")]
            public string RefreshToken { get; set; }

            [JsonProperty("expire")]
            public int ExpiresIn { get; set; }
        }


        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("gender")]
        public UserGender Gender { get; set; }

        //[JsonProperty("birthday")]
        //public string Birthday { get; set; }

        //[JsonProperty("last_active")]
        //public string LastActive { get; set; }

        [JsonProperty("online")]
        public int Online { get; set; }

        //[JsonProperty("register_date")]
        //public string RegisterDate { get; set; }

        //[JsonProperty("login_date")]
        //public string LoginDate { get; set; }

        //relationship_status
        //mood_id
        //group
        //updated
        //lastUpdated
        //status
        //avatarInfo

        [JsonProperty("token")]
        public UserToken Token { get; set; }
    }
}
