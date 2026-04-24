using ControleDeAcesso26.Communication.Responses.ResponsesBiometriaUsuario;

namespace ControleDeAcesso26.Application.UseCases.BiometriaUsuarioUseCases.Interfaces
{
    public interface IListarBiometriasUseCase
    {
        public Task<List<ResponseListarBiometriasUsuarioJson>> Execute(int? idUsuario = null, int paginaAtual = 1);
    }
}
