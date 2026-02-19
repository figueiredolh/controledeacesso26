using ControleDeAcesso26.Application.UseCases.UsuarioUseCases.Interfaces;
using ControleDeAcesso26.Application.UseCases.UsuarioUseCases.Validators;
using ControleDeAcesso26.Communication.Requests.RequestsUsuario;
using ControleDeAcesso26.Communication.Responses.ResponsesUsuario;
using ControleDeAcesso26.Domain.Entities;
using ControleDeAcesso26.Domain.Interfaces.IUnitOfWork;
using ControleDeAcesso26.Domain.Interfaces.IUsuario;
using ControleDeAcesso26.Exceptions.Exceptions;
using ControleDeAcesso26.Exceptions.ValidatorsRulesResourceMessages;
using Mapster;

namespace ControleDeAcesso26.Application.UseCases.UsuarioUseCases
{
    public class CriarUsuarioUseCase : ICriarUsuarioUseCase
    {
        private readonly IUsuarioWriteRepository _usuarioWriteRepository;
        private readonly IUsuarioReadOnlyRepository _usuarioReadOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CriarUsuarioUseCase(IUsuarioWriteRepository usuarioWriteRepository, IUsuarioReadOnlyRepository usuarioReadOnlyRepository,
            IUnitOfWork unitOfWork)
        {
            _usuarioWriteRepository = usuarioWriteRepository;
            _usuarioReadOnlyRepository = usuarioReadOnlyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseCriarUsuarioJson> Execute(RequestCriarUsuarioJson request)
        {
            await ValidateRequest(request);

            var entidadeUsuarioBD = request.Adapt<Usuario>();

            await _usuarioWriteRepository.CriarUsuario(entidadeUsuarioBD);
            await _unitOfWork.SalvarMudancas();

            var usuarioRespostaDto = entidadeUsuarioBD.Adapt<ResponseCriarUsuarioJson>();
            return usuarioRespostaDto;
        }

        private async Task ValidateRequest(RequestCriarUsuarioJson request)
        {
            var validator = new RequestCriarUsuarioValidator();
            var validationResults = validator.Validate(request);

            List<string> errorMessages = new List<string>();

            if (!validationResults.IsValid)
            {
                foreach (var failure in validationResults.Errors)
                {
                    var errorMessage = failure.ErrorMessage;
                    errorMessages.Add(errorMessage);
                }

                throw new ErrorOnValidationException(errorMessages);
            }

            var apelidoExiste = await _usuarioReadOnlyRepository.ApelidoJaExisteNoSistema(request.Apelido);

            if (apelidoExiste)
            {
                errorMessages.Add(ValidatorsRulesResourceMessages.USUARIO_APELIDO_JA_EXISTE);
                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
