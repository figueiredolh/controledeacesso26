using ControleDeAcesso26.Communication.Responses.ResponsesBiometriaUsuario;
using ControleDeAcesso26.Domain.Entities;
using Mapster;

namespace ControleDeAcesso26.Application.MapsterConfiguration
{
    public class MappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<TemplateBiometriaUsuario, ResponseListarBiometriasUsuarioJson>()
                  .Map(src => src.NomeUsuario, dest => dest.Usuario.Nome);
        }
    }
}
