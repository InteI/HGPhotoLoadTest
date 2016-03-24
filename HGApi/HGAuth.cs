using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;
using PhotoLoader.HGApi.Models;
using Newtonsoft.Json;

namespace HGApi {
    class HGAuth : IAuthenticator {
        public User User { get; private set; }
        public string Email { get; set; } = "2@mail.ru";
        public string Password { get; set; } = "test";

        public void Authenticate(IRestClient client, IRestRequest request) {
            if (User == null /*|| User.Token.ExpiresIn <= System.DateTime.Now.Ticks*/) {
                Login(client);
            }

            request.AddParameter("access_token", User.Token.AccessToken);
        }

        public void Login(IRestClient client) {
            IRestRequest request = new RestRequest("login/", Method.POST);
            request.AddParameter("auth_email", Email);
            request.AddParameter("auth_password", Password);

            client.Authenticator = null;
            IRestResponse response = client.Execute(request);
            client.Authenticator = this;

            if (response.StatusCode == System.Net.HttpStatusCode.OK) {
                User = JsonConvert.DeserializeObject<User>(response.Content);
            } else {
                if (response.ErrorException != null) {
                    throw response.ErrorException;
                } else {
                    throw new System.Net.WebException(response.Content);
                }
            }
        }

        public void ReLogin(IRestClient client) {
            IRestRequest request = new RestRequest("relogin/", Method.POST);
            request.AddParameter("refresh_token", User.Token.RefreshToken);

            client.Authenticator = null;
            IRestResponse response = client.Execute(request);
            client.Authenticator = this;

            if (response.StatusCode == System.Net.HttpStatusCode.OK) {
                User = JsonConvert.DeserializeObject<User>(response.Content);
            } else {
                if (response.ErrorException != null) {
                    throw response.ErrorException;
                } else {
                    throw new System.Net.WebException(response.Content);
                }
            }
        }
    }
}
