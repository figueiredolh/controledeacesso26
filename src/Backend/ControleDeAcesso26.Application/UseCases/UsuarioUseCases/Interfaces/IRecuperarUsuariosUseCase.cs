using ControleDeAcesso26.Communication.Responses.ResponsesUsuario;

namespace ControleDeAcesso26.Application.UseCases.UsuarioUseCases.Interfaces
{
    public interface IRecuperarUsuariosUseCase
    {
        public Task<List<ResponseRecuperarUsuariosJson>> Execute(bool incluirInativos = false);
    }
}
