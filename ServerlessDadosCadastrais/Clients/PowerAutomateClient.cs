using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using ServerlessDadosCadastrais.Models;

namespace ServerlessDadosCadastrais.Clients
{
    public class PowerAutomateClient
    {
        private HttpClient _client;

        public PowerAutomateClient(HttpClient client)
        {
            _client = client;
        }

        public void EnviarCadastro(Cadastro cadastro)
        {
            _client.BaseAddress = new Uri(
                Environment.GetEnvironmentVariable("UrlPowerAutomate"));
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var requestMessage =
                  new HttpRequestMessage(HttpMethod.Post, String.Empty);

            requestMessage.Content = new StringContent(
                JsonSerializer.Serialize(cadastro),
                Encoding.UTF8, "application/json");

            var respPowerAutomate = _client
                .SendAsync(requestMessage).Result;
            respPowerAutomate.EnsureSuccessStatusCode();
        }        
    }
}