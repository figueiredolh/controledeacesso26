using ControleDeAcesso26.Application.UseCases.UsuarioUseCases.Interfaces;
using ControleDeAcesso26.Application.UseCases.UsuarioUseCases.Validators;
using ControleDeAcesso26.Communication.Requests.RequestsUsuario;
using ControleDeAcesso26.Communication.Responses.ResponsesUsuario;
using ControleDeAcesso26.Domain.Interfaces.IUnitOfWork;
using ControleDeAcesso26.Domain.Interfaces.IUsuario;
using ControleDeAcesso26.Exceptions.Exceptions;
using ControleDeAcesso26.Exceptions.ValidatorsRulesResourceMessages;
using Mapster;

namespace ControleDeAcesso26.Application.UseCases.UsuarioUseCases
{
    public class AtualizarReativarUsuarioUseCase : IAtualizarReativarUsuarioUseCase
    {
        private readonly IUsuarioReadOnlyRepository _usuarioReadOnlyRepository;
        private readonly IUsuarioUpdateRepository _usuarioUpdateRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AtualizarReativarUsuarioUseCase(IUsuarioReadOnlyRepository usuarioReadOnlyRepository,
                                       IUsuarioUpdateRepository usuarioUpdateRepository,
                                       IUnitOfWork unitOfWork)
        {
            _usuarioReadOnlyRepository = usuarioReadOnlyRepository;
            _usuarioUpdateRepository = usuarioUpdateRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<ResponseAtualizarReativarUsuarioJson> Execute(long id, RequestAtualizarReativarUsuarioJson request)
        {
            var usuarioBD = await _usuarioReadOnlyRepository.RecuperarUsuarioPorId(id, false);

            if (usuarioBD is null)
                throw new NotFoundException(ValidatorsRulesResourceMessages.USUARIO_NAO_ENCONTRADO_OU_ATIVO);

            ValidateRequest(request);

            usuarioBD.Ativo = request.Ativo;

            _usuarioUpdateRepository.AtualizarUsuario(usuarioBD);
            await _unitOfWork.SalvarMudancas();

            var usuarioUpdateResponse = usuarioBD.Adapt<ResponseAtualizarReativarUsuarioJson>();
            return usuarioUpdateResponse;
        }

        private static void ValidateRequest(RequestAtualizarReativarUsuarioJson request)
        {
            var validator = new RequestAtualizarReativarUsuarioValidator();
            var validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
            {
                var errorMessage = validationResult.Errors.First().ErrorMessage;
                throw new ErrorOnValidationException(errorMessage);
            }
        }
    }
}
