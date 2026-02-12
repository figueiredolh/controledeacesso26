using ControleDeAcesso26.Application.UseCases.UsuarioUseCases.Interfaces;
using ControleDeAcesso26.Communication.Responses.ResponsesUsuario;
using ControleDeAcesso26.Domain.Interfaces.IUnitOfWork;
using ControleDeAcesso26.Domain.Interfaces.IUsuario;
using ControleDeAcesso26.Exceptions.Exceptions;
using ControleDeAcesso26.Exceptions.ValidatorsRulesResourceMessages;
using Mapster;

namespace ControleDeAcesso26.Application.UseCases.UsuarioUseCases
{
    public class ExcluirUsuarioDefinitivamenteUseCase : IExcluirUsuarioDefinitivamenteUseCase
    {
        private readonly IUsuarioReadOnlyRepository _usuarioReadOnlyRepository;
        private readonly IUsuarioDeleteRepository _usuarioDeleteRepository;

        public ExcluirUsuarioDefinitivamenteUseCase(IUsuarioReadOnlyRepository usuarioReadOnlyRepository, 
                                                    IUsuarioDeleteRepository usuarioDeleteRepository)
        {
            _usuarioReadOnlyRepository = usuarioReadOnlyRepository;
            _usuarioDeleteRepository = usuarioDeleteRepository;
        }
        public async Task<ResponseExcluirUsuarioDefinitivamenteJson> Execute(long id)
        {
            var usuarioBD = await _usuarioReadOnlyRepository.RecuperarUsuarioPorId(id, false, true);

            if (usuarioBD is null)
            {
                throw new NotFoundException(ValidatorsRulesResourceMessages.USUARIO_NAO_ENCONTRADO_OU_ATIVO);
            }

            await _usuarioDeleteRepository.ExcluirUsuarioDefinitivamente(usuarioBD.Id);

            var usuarioDeleteResponse = usuarioBD.Adapt<ResponseExcluirUsuarioDefinitivamenteJson>();
            return usuarioDeleteResponse;
        }
    }
}
