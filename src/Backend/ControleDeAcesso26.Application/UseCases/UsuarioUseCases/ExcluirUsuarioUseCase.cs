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
    public class ExcluirUsuarioUseCase : IExcluirUsuarioUseCase
    {
        private readonly IUsuarioReadOnlyRepository _usuarioReadOnlyRepository;
        private readonly IUsuarioDeleteRepository _usuarioUpdateRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ExcluirUsuarioUseCase(IUsuarioReadOnlyRepository usuarioReadOnlyRepository,
                                       IUsuarioDeleteRepository usuarioDeleteRepository,
                                       IUnitOfWork unitOfWork)
        {
            _usuarioReadOnlyRepository = usuarioReadOnlyRepository;
            _usuarioUpdateRepository = usuarioDeleteRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<ResponseExcluirUsuarioJson> Execute(long id, RequestExcluirUsuarioJson request)
        {
            var usuarioBD = await _usuarioReadOnlyRepository.RecuperarUsuarioPorId(id);

            if (usuarioBD is null)
                throw new NotFoundException(ValidatorsRulesResourceMessages.USUARIO_NAO_ENCONTRADO);

            ValidateRequest(request);

            usuarioBD.Ativo = request.Ativo;

            _usuarioUpdateRepository.AtualizarUsuario(usuarioBD);
            await _unitOfWork.SalvarMudancas();

            var usuarioDeleteResponse = usuarioBD.Adapt<ResponseExcluirUsuarioJson>();
            return usuarioDeleteResponse;
        }

        private static void ValidateRequest(RequestExcluirUsuarioJson request)
        {
            var validator = new RequestExcluirUsuarioValidator();
            var validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
            {
                var errorMessage = validationResult.Errors.First().ErrorMessage;
                throw new ErrorOnValidationException(errorMessage);
            }
        }
    }
}
