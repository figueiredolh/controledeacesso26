using ControleDeAcesso26.Communication.Responses.ResponsesBiometriaUsuario;

namespace ControleDeAcesso26.Application.UseCases.BiometriaUsuarioUseCases.Interfaces
{
    public interface ICadastrarBiometriaUsuarioUseCase
    {
        public Task<ResponseCadastrarBiometriaUsuarioJson> Execute(long idUsuario);
    }
}
