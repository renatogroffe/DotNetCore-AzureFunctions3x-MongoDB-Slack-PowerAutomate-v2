using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ServerlessDadosCadastrais.Business;

namespace ServerlessDadosCadastrais
{
    public class DadosCadastrais
    {
        private readonly CadastroServices _cadastroSvc;

        public DadosCadastrais(CadastroServices cadastroSvc)
        {
            _cadastroSvc = cadastroSvc;
        }

        [FunctionName("DadosCadastrais")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Acessada a Function DadosCadastrais");
            log.LogInformation($"Operação: {req.Method}");

            string strDadosOriginais =
                new StreamReader(req.Body).ReadToEndAsync().Result;
            log.LogInformation($"Dados originais: {strDadosOriginais}");

            switch (req.Method)
            {
                case "GET":
                    return _cadastroSvc.Get();                
                case "POST":
                    return _cadastroSvc.Insert(strDadosOriginais);
            }

            return new BadRequestResult();
        }
    }
}