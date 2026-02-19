using ControleDeAcesso26.Communication.Requests.RequestsUsuario;
using ControleDeAcesso26.Communication.Responses.ResponsesUsuario;

namespace ControleDeAcesso26.Application.UseCases.UsuarioUseCases.Interfaces
{
    public interface IExcluirUsuarioUseCase
    {
        public Task<ResponseExcluirUsuarioJson> Execute(long id, RequestExcluirUsuarioJson request);
    }
}
