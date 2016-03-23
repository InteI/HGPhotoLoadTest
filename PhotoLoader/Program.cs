using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft;
using Newtonsoft.Json.Serialization;

namespace PhotoLoader
{
    [JsonObject(MemberSerialization.OptIn)]
    struct AuthResponse {
        [JsonProperty("token")]
        public Token Token { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    struct Token {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
    }


    class Program {
        private string BaseUrl { get; set; } = "http://giraffe.code-geek.ru/v1_4/api/";
        private string PhotoUrl { get; set; } = "photo/";
        private string LoginUrl { get; set; } = "login/";

        private AuthResponse Token;

        private IRestClient Client { get; set; }

        static void Main(string[] args) {
            new Program().Start();
        }

        private void Start() {
            Client = new RestClient(BaseUrl);

            IRestRequest request = new RestRequest(LoginUrl, Method.POST);
            request.AddParameter("auth_email", "2@mail.ru");
            request.AddParameter("auth_password", "test");

            IRestResponse response = Client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK) {
                Token = JsonConvert.DeserializeObject<AuthResponse>(response.Content);

                Console.WriteLine(Token.Token.AccessToken);
            }


        }
    }
}
