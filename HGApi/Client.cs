using System;
using System.Net;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using HGApi.Models;

namespace HGApi {
    public class Client {
        private string BaseUrl { get; set; } = "http://giraffe.code-geek.ru/v1_4/api/";
        private IRestClient RestClient { get; set; }

        #region Route
        private string Photo { get; set; } = "photo/";
        private string Login { get; set; } = "login/";
        #endregion

        public Client(string baseUrl = null) {
            if (baseUrl != null) {
                BaseUrl = baseUrl;
            }

            RestClient = new RestClient(BaseUrl);
            RestClient.Authenticator = new HGAuth();
            ((HGAuth)RestClient.Authenticator).Login(RestClient);
        }

        public T Execute<T>(IRestRequest request) where T : new() {
            IRestResponse response = RestClient.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK) {
                return JsonConvert.DeserializeObject<T>(response.Content);
            } else {
                if (response.ErrorException != null) {
                    throw response.ErrorException;
                } else {
                    if (response.StatusCode == HttpStatusCode.Unauthorized) {
                        ((HGAuth)RestClient.Authenticator).ReLogin(RestClient);
                    }

                    throw new WebException(response.Content);
                }
            }
        }

        public async Task<T> ExecuteAsync<T>(IRestRequest request) where T : new() {
            IRestResponse response = await RestClient.ExecuteTaskAsync(request);

            if (response.StatusCode == HttpStatusCode.OK) {
                return JsonConvert.DeserializeObject<T>(response.Content);
            } else {
                if (response.ErrorException != null) {
                    throw response.ErrorException;
                } else {
                    if (response.StatusCode == HttpStatusCode.Unauthorized) {
                        ((HGAuth)RestClient.Authenticator).ReLogin(RestClient);
                    }

                    throw new WebException(response.Content);
                }
            }
        }

        public Photo PostPhoto(string path) {
            IRestRequest request = new RestRequest(Photo, Method.POST);
            request.AddFile(path, "image/png");

            try {
                return Execute<Photo>(request);
            } catch (Exception ex) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
                return null;
            }
        }

        public async Task<Photo> PostPhotoAsync(string path) {
            IRestRequest request = new RestRequest(Photo, Method.POST);
            request.AddFile("photo", path, "image/png");

            try {
                return await ExecuteAsync<Photo>(request);
            } catch (Exception ex) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
                return null;
            }
        }
    }
}
