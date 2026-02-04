using ControleDeAcesso26.Application.UseCases.UsuarioUseCases.Interfaces;
using ControleDeAcesso26.Communication.Responses.ResponsesUsuario;
using ControleDeAcesso26.Domain.Interfaces.IUsuario;
using Mapster;

namespace ControleDeAcesso26.Application.UseCases.UsuarioUseCases
{
    public class RecuperarUsuariosUseCase : IRecuperarUsuariosUseCase
    {
        private readonly IUsuarioReadOnlyRepository _usuarioReadOnlyRepository;
        public RecuperarUsuariosUseCase(IUsuarioReadOnlyRepository usuarioReadOnlyRepository)
        {
            _usuarioReadOnlyRepository = usuarioReadOnlyRepository;
        }

        public async Task<List<ResponseRecuperarUsuariosJson>> Execute(bool incluirInativos)
        {
            var usuariosDoBanco = await _usuarioReadOnlyRepository.RecuperarUsuarios(incluirInativos);
            var usuariosObjetoDto = usuariosDoBanco.Adapt<List<ResponseRecuperarUsuariosJson>>();

            return usuariosObjetoDto;
        }
    }
}
