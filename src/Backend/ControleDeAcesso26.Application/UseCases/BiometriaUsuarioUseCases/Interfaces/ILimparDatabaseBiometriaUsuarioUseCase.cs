using ControleDeAcesso26.Communication.Responses.ResponsesBiometriaUsuario;

namespace ControleDeAcesso26.Application.UseCases.BiometriaUsuarioUseCases.Interfaces
{
    public interface ILimparDatabaseBiometriaUsuarioUseCase
    {
        public Task<ResponseLimparDatabaseBiometriaUsuarioJson> Execute(string palavraConfirmacao);
    }
}
