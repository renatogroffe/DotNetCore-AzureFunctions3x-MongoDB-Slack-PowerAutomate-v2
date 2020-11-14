using AutoMapper;
using ServerlessDadosCadastrais.Models;
using ServerlessDadosCadastrais.Documents;

namespace ServerlessDadosCadastrais.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Cadastro, CadastroDocument>()
                .ForMember(dest => dest.Nome, m => m.MapFrom(c => c.nome))
                .ForMember(dest => dest.NomePai, m => m.MapFrom(c => c.nome_pai))
                .ForMember(dest => dest.NomeMae, m => m.MapFrom(c => c.nome_mae))
                .ForMember(dest => dest.Tecnologia, m => m.MapFrom(c => c.tecnologia))
                .ForMember(dest => dest.Idade, m => m.MapFrom(c => c.idade))
                .ForMember(dest => dest.Localidade, m => m.MapFrom(c => c.cidade))
                .ForMember(dest => dest.ReceberNovidades,
                    m => m.MapFrom(c => c.aceito_novidades.Value ? "Sim" : "Não"));                

            CreateMap<CadastroDocument, Cadastro>()
                .ForMember(dest => dest.nome, m => m.MapFrom(c => c.Nome))
                .ForMember(dest => dest.nome_pai, m => m.MapFrom(c => c.NomePai))
                .ForMember(dest => dest.nome_mae, m => m.MapFrom(c => c.NomeMae))
                .ForMember(dest => dest.tecnologia, m => m.MapFrom(c => c.Tecnologia))
                .ForMember(dest => dest.idade, m => m.MapFrom(c => c.Idade))
                .ForMember(dest => dest.cidade, m => m.MapFrom(c => c.Localidade))
                .ForMember(dest => dest.aceito_novidades,
                    m => m.MapFrom(c => c.ReceberNovidades == "Sim"));                
        }
    }
}