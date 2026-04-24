using ControleDeAcesso26.Communication.Responses.ResponsesBiometriaUsuario;

namespace ControleDeAcesso26.Application.UseCases.BiometriaUsuarioUseCases.Interfaces
{
    public interface IExcluirBiometriaUsuarioUseCase
    {
        public Task<ResponseExcluirBiometriaUsuarioJson> Execute(int idSensor);
    }
}
