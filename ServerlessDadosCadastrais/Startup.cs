using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using ServerlessDadosCadastrais.Mappings;
using ServerlessDadosCadastrais.Data;
using ServerlessDadosCadastrais.Clients;
using ServerlessDadosCadastrais.Business;

[assembly: FunctionsStartup(typeof(ServerlessDadosCadastrais.Startup))]
namespace ServerlessDadosCadastrais
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddScoped<CadastroRepository>();
            builder.Services.AddHttpClient<CanalSlackClient>();
            builder.Services.AddHttpClient<PowerAutomateClient>();
            builder.Services.AddScoped<CadastroServices>();
        }
    }
}