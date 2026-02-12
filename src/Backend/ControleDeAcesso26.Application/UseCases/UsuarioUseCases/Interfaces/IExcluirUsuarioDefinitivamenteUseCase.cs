using ControleDeAcesso26.Communication.Responses.ResponsesUsuario;

namespace ControleDeAcesso26.Application.UseCases.UsuarioUseCases.Interfaces
{
    public interface IExcluirUsuarioDefinitivamenteUseCase
    {
        public Task<ResponseExcluirUsuarioDefinitivamenteJson> Execute(long id);
    }
}
