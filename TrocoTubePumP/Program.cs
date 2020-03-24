using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Threading;


namespace TrocoTubePumP
{
    public class RootObject
    {
        public string uid { get; set; }
        public string token { get; set; }
    }

    class Program
    {
        private static string uid = "101618080588744034510";
        private static string token = "eyJhbGciOiJSUzI1NiIsImtpZCI6IjUzYzY2YWFiNTBjZmRkOTFhMTQzNTBhNjY0ODJkYjM4MDBjODNjNjMiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJodHRwczovL2FjY291bnRzLmdvb2dsZS5jb20iLCJhenAiOiI0OTcyMzMzODU1My1scWFzdTZva2lnaDBrMmo1cmJzcjk5YjZpNW5vaDAwaS5hcHBzLmdvb2dsZXVzZXJjb250ZW50LmNvbSIsImF1ZCI6IjQ5NzIzMzM4NTUzLTZkM25tN2loYnFrdXJra2J2ajFkMGw3amNkOGRqcDhlLmFwcHMuZ29vZ2xldXNlcmNvbnRlbnQuY29tIiwic3ViIjoiMTAxNjE4MDgwNTg4NzQ0MDM0NTEwIiwiZW1haWwiOiJmZXdleGtvQGdtYWlsLmNvbSIsImVtYWlsX3ZlcmlmaWVkIjp0cnVlLCJuYW1lIjoiRmV3SGFra28iLCJwaWN0dXJlIjoiaHR0cHM6Ly9saDMuZ29vZ2xldXNlcmNvbnRlbnQuY29tL2EtL0FPaDE0R2oxdjh5c1A3ZUJqT1M3WDI0TUpnamNRdGZUcEZaMHZnNG51N2ViPXM5Ni1jIiwiZ2l2ZW5fbmFtZSI6IkZld0hha2tvIiwibG9jYWxlIjoidGgiLCJpYXQiOjE1ODUwNDY3NjAsImV4cCI6MTU4NTA1MDM2MH0.TzuU3-UoVXjcYJT3NER5_5fEhioBV2CmKWpqjKEaP2rBgH7JNPmHDGz7RmalPnDmhBaGLYYKk-drr5zUR2PNRqiwW0a3k3EVdJibSnd9aT7HutDWpL7Cb8rg3m8abAr370Tey67MaaARK28Zxtlehv8ZR2NOHKGUUJuuIpg9RL04hOuKxAOuxdZHjvngnLrmnDHWO3YBk4N2hZMrbgHaTUhuHW2Cvm_nLnhjkvhqrA4S_go6aEePjtv5IHQWPHxIj4r-rz-foAHNRqx0kG_JR2mVPrcilynugieiuEfHmAvOFKgSXU7jTqzw8jVzvnBasafwC-MgH7vhgGVwP-gA4A";

        static void Main(string[] args)
        {
            Console.Title = $"ChanelName : null | Point : 0 || By : FewHakko";
            try
            {
                Thread t1 = new Thread(new ThreadStart(infodata));
                t1.Start();

                while (true)
                {
                    var path = "config.json";
                    string content = File.ReadAllText(path);
                    RootObject w = JsonConvert.DeserializeObject<RootObject>(content);
                    uid = w.uid;
                    token = w.token;
                    string json = getlistar();
                    Console.WriteLine("===================");
                    dynamic dynJson = JsonConvert.DeserializeObject(json);
                    foreach (var data in dynJson)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("canal_id : " + data.canal_id);
                        Console.WriteLine("dono_id : " + data.dono_id);
                        Console.WriteLine("ChanelName : " + data.nome);

                        string canal = data.canal_id;
                        string dono = data.dono_id;
                        string get = getcoin(canal, dono);
                        if (get.Contains("200"))
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"[{ DateTime.Now.ToLongTimeString()}] GetCoinsSuccess");
                        }
                        Console.WriteLine("===================");
                    }
                }
            }
            catch
            {

            }
        }

        private static void infodata()
        {
            while (true)
            {
                try
                {
                    string few;
                    var vm = new { app = 0, bloqueado = 0, compras_bloqueado = 0, dia_atual = 0, moedas_maxima = 0, moedas_media = 0, online = 0, premium = 0, primeiro_acesso = 0, status_code = 200, usuario_moedas = 0, uid = Program.uid, token = Program.token };
                    using (var client = new WebClient())
                    {
                        var dataString = JsonConvert.SerializeObject(vm);
                        client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                        few = client.UploadString(new Uri("https://newhopeapps.com/index.php/canal/usuario/buscar2"), "POST", dataString);
                        dynamic dynJson = JsonConvert.DeserializeObject(few);
                        foreach (var data in dynJson)
                        {
                            Console.Title = $"ChanelName : {data.nome} | Point : {data.moedas} || By : FewHakko";
                        }
                    }
                    Thread.Sleep(2000);
                }
                catch
                {

                }
            }
        }

        private static string getcoin(string canal_id, string dono_id)
        {
            try
            {
                string few;
                var vm = new { dono_id = dono_id, uid = Program.uid, bloqueado = 0, canal_id = canal_id, compras_bloqueado = 0, dia_atual = 0, moedas_maxima = 0, moedas_media = 0, online = 0, status_code = 200, token = Program.token, usuario_moedas = 0 };
                using (var client = new WebClient())
                {
                    var dataString = JsonConvert.SerializeObject(vm);
                    client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                    few = client.UploadString(new Uri("https://newhopeapps.com/index.php/transacao/inscrever2"), "POST", dataString);
                }
                return few;
            }
            catch
            {
                return "";
            }
        }

        private static string getlistar()
        {
            try
            {
                string few;
                var vm = new { app = 0, premium = 0, primeiro_acesso = 0, uid = Program.uid, bloqueado = 0, compras_bloqueado = 0, dia_atual = 0, moedas_maxima = 0, moedas_media = 0, online = 0, status_code = 200, token = Program.token, usuario_moedas = 0 };
                using (var client = new WebClient())
                {
                    var dataString = JsonConvert.SerializeObject(vm);
                    client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                    few = client.UploadString(new Uri("https://newhopeapps.com/index.php/canal/listar"), "POST", dataString);
                }
                return few;
            }
            catch
            {
                return "";
            }
        }
    }
}
