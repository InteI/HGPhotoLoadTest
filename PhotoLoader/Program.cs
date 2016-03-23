using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;
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

        /*private void Start() {
            Client = new RestClient(BaseUrl);

            IRestRequest request = new RestRequest(LoginUrl, Method.POST);
            request.AddParameter("auth_email", "2@mail.ru");
            request.AddParameter("auth_password", "test");

            IRestResponse response = Client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK) {
                Token = JsonConvert.DeserializeObject<AuthResponse>(response.Content);

                Console.WriteLine(Token.Token.AccessToken);

                request = new RestRequest(PhotoUrl, Method.POST);
                request.AddParameter("access_token", Token.Token.AccessToken);
                request.AddFile("photo", "image-67f4f42ddb226dd6a23b487cf22a6362c0652128922f2de051567a497fdbf536-V.jpeg");

                response = Client.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK) {
                    Console.WriteLine("PhotoPostSuccess!");
                } else {
                    Console.WriteLine("PhotoPostfailed: {0}", response.Content);
                }
            } else {
                Console.WriteLine("AuthFailed! {0}", response.Content);
            }

            Console.ReadKey(true);
        }*/

        private void Start() {
            Client = new RestClient(BaseUrl);
            Client.Authenticator = new HGApi.HGAuth();

            IRestRequest test = new RestRequest("comments/");
            Client.Execute(test);

            Console.WriteLine(((HGApi.HGAuth)Client.Authenticator).User.Token.AccessToken);

            Console.ReadKey(true);
        }
    }
}
