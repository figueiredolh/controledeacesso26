using ControleDeAcesso26.Communication.Requests.RequestsUsuario;
using ControleDeAcesso26.Communication.Responses.ResponsesUsuario;

namespace ControleDeAcesso26.Application.UseCases.UsuarioUseCases.Interfaces
{
    public interface IAtualizarReativarUsuarioUseCase
    {
        public Task<ResponseAtualizarReativarUsuarioJson> Execute(long id, RequestAtualizarReativarUsuarioJson request);
    }
}
