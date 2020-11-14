using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;
using AutoMapper;
using ServerlessDadosCadastrais.Data;
using ServerlessDadosCadastrais.Documents;
using ServerlessDadosCadastrais.Models;
using ServerlessDadosCadastrais.Validators;
using ServerlessDadosCadastrais.Clients;

namespace ServerlessDadosCadastrais.Business
{
    public class CadastroServices
    {
        private readonly IMapper _mapper;
        private readonly CadastroRepository _repository;
        private readonly CanalSlackClient _clientSlack;
        private readonly PowerAutomateClient _clientPowerAutomate;

        public CadastroServices(IMapper mapper,
            CadastroRepository repository,
            CanalSlackClient clientSlack,
            PowerAutomateClient clientPowerAutomate)
        {
            _mapper = mapper;
            _repository = repository;
            _clientSlack = clientSlack;
            _clientPowerAutomate = clientPowerAutomate;
        }

        public IActionResult Get()
        {
            return new OkObjectResult(
                _mapper.Map<List<Cadastro>>(_repository.ListAll()));
        }

        public IActionResult Insert(string strDadosCadastrais)
        {
            var dadosCadastrais = DeserializeDadosCadastrais(strDadosCadastrais);
            var resultado = DadosValidos(dadosCadastrais);
            resultado.Acao = "Inclusão de Dados Cadastrais";
        
            if (resultado.Inconsistencias.Count == 0)
            {
                _repository.Save(_mapper.Map<CadastroDocument>(dadosCadastrais));
                _clientSlack.GerarAvisoInclusao(dadosCadastrais.nome);
                _clientPowerAutomate.EnviarCadastro(dadosCadastrais);
                return new OkObjectResult(resultado);
            }
            else
            {
                _clientSlack.GerarAvisoFalhaCadastro(
                    strDadosCadastrais, resultado.Inconsistencias);
                return new BadRequestObjectResult(resultado);
            }
        }

        private Cadastro DeserializeDadosCadastrais(string dados)
        {
            Cadastro dadosCadastrais;
            try
            {
                dadosCadastrais = JsonSerializer.Deserialize<Cadastro>(dados,
                    new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
            catch
            {
                dadosCadastrais = null;
            }

            return dadosCadastrais;
        }

        private Resultado DadosValidos(Cadastro cadastro)
        {
            var resultado = new Resultado();
            if (cadastro == null)
            {
                resultado.Inconsistencias.Add(
                    "Preencha os Dados Cadastrais");
            }
            else
            {
                var validator = new CadastroValidator().Validate(cadastro);
                if (!validator.IsValid)
                {
                    foreach (var errors in validator.Errors)
                        resultado.Inconsistencias.Add(errors.ErrorMessage);
                }
            }

            return resultado;
        }
    }
}