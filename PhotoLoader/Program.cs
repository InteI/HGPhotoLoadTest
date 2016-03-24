using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json;
using Newtonsoft;
using Newtonsoft.Json.Serialization;
using HGApi;
using HGApi.Models;

namespace PhotoLoader
{
    class Program {
        private Client Client { get; set; }

        static void Main(string[] args) {
            new Program().Init();
        }

        private void Init() {
            Client = new Client();

            for (int i = 0; i < 100; i++) {
                new Thread(Start).Start();
            }

            Console.ReadKey(true);
        }

        private void Start() {
            List<Task> tasks = new List<Task>();

            for (int i = 0; i < 100; i++) {
                //tasks.Add(UploadPhoto);
                Task.Run(UploadPhoto);
            }

            Task.WaitAll(tasks.ToArray());
        }

        private async Task UploadPhoto() {
            Photo photo = await Client.PostPhotoAsync("image-67f4f42ddb226dd6a23b487cf22a6362c0652128922f2de051567a497fdbf536-V.jpeg");

            if (photo != null) {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("UploadSuccess!");
                Console.ResetColor();
            }
        }
    }
}
