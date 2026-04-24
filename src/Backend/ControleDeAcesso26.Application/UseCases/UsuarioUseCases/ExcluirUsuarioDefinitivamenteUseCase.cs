using ControleDeAcesso26.Application.UseCases.UsuarioUseCases.Interfaces;
using ControleDeAcesso26.Communication.Responses.ResponsesUsuario;
using ControleDeAcesso26.Domain.Interfaces.IUsuario;
using ControleDeAcesso26.Exceptions.Exceptions;
using ControleDeAcesso26.Exceptions.ValidatorsRulesResourceMessages;
using Mapster;
using MySql.Data.MySqlClient;

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

            try
            {
                await _usuarioDeleteRepository.ExcluirUsuarioDefinitivamente(usuarioBD.Id);
            }
            catch (MySqlException)
            {
                throw new DbDeleteUsuarioException(ValidatorsRulesResourceMessages.ERRO_USUARIO_BIOMETRIAS_ASSOCIADAS);
            }

            var usuarioDeleteResponse = usuarioBD.Adapt<ResponseExcluirUsuarioDefinitivamenteJson>();
            return usuarioDeleteResponse;
        }
    }
}
