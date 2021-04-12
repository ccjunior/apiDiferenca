using Avaliacao.Core.Commom;
using Avaliacao.Core.Model;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Avaliacao.Cliente
{
    public class Program
    {
        public static HttpClient client;
        public static string uri = string.Empty;
        public static Guid id;
        public Program()
        {
          

        }

        public static void Main(string[] args)
        {
            client = new HttpClient();
            uri = "https://localhost:44326/api/v1/Diff";
            id = Guid.NewGuid();

            left(client, uri, id);
            right();
        }

        public static void left(HttpClient client, string uri, Guid id)
        {
            
            string dado = "Estou enviando um dados para o metodo left";
            var data = Conversor.SerializeToString<string>(dado);


            try
            {
                Request request = new Request();
                request.Id = id;
                request.Dados = data;
                request.DataEnvio = DateTime.Now;
                var jsonInString = JsonSerializer.Serialize(request);
                client.PostAsync(uri + "/left", new StringContent(jsonInString, Encoding.UTF8, "application/json"));
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
            
        }

        public static void right()
        {
            string dado = "Estou enviando um dados para o metodo right";
            var data = Conversor.SerializeToString<string>(dado);

            Request request = new Request();
            request.Id = id;
            request.Dados = data;
            request.DataEnvio = DateTime.Now;
            var jsonInString = JsonSerializer.Serialize(request);
            client.PostAsync(uri, new StringContent(jsonInString, Encoding.UTF8, "application/json"));
        }

        public void resultado(HttpClient client, string uri,Guid id)
        {
            //var jsonInString = JsonSerializer.Serialize(message);
            //client.PostAsync(uri, new StringContent(jsonInString, Encoding.UTF8, "application/json"));
        }
    }
}
