using ControleDeAcesso26.Communication.Requests.RequestsUsuario;
using ControleDeAcesso26.Communication.Responses.ResponsesUsuario;

namespace ControleDeAcesso26.Application.UseCases.UsuarioUseCases.Interfaces
{
    public interface IAtualizarUsuarioUseCase
    {
        public Task<ResponseAtualizarUsuarioJson> Execute(long id, RequestAtualizarUsuarioJson request);
    }
}
