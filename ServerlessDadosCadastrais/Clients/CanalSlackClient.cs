using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace ServerlessDadosCadastrais.Clients
{
    public class CanalSlackClient
    {
        private HttpClient _client;

        public CanalSlackClient(HttpClient client)
        {
            _client = client;
        }

        public void GerarAvisoInclusao(string nomeCadastrado)
        {
            PostMessage(Environment.GetEnvironmentVariable("UrlCanalSlackInclusao"),
                "Inclusao de dados",
               $"Realizado o cadastro de '{nomeCadastrado}'");
        }

        public void GerarAvisoFalhaCadastro(string strDadosCadastrais,
            List<string> falhas)
        {
            var strbFalhas = new StringBuilder();
            strbFalhas.Append(strDadosCadastrais);
            foreach (string falha in falhas)
            {
                strbFalhas.Append("|");
                strbFalhas.Append(falha);
            }

            PostMessage(Environment.GetEnvironmentVariable("UrlCanalSlackFalhaInclusao"),
                "Falha no cadastramento", strbFalhas.ToString());
        }
        
        private void PostMessage(
            string urlLogicApp, string descricaoAcao, string detalhesAcao)
        {
            _client.BaseAddress = new Uri(urlLogicApp);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var requestMessage =
                  new HttpRequestMessage(HttpMethod.Post, String.Empty);

            requestMessage.Content = new StringContent(
                JsonSerializer.Serialize(new
                {
                    descricao = descricaoAcao,
                    detalhes = detalhesAcao,
                }), Encoding.UTF8, "application/json");

            var respLogicApp = _client
                .SendAsync(requestMessage).Result;
            respLogicApp.EnsureSuccessStatusCode();
        }        
    }
}