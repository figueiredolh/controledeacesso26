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
    public class AtualizarUsuarioUseCase : IAtualizarUsuarioUseCase
    {
        private readonly IUsuarioReadOnlyRepository _usuarioReadOnlyRepository;
        private readonly IUsuarioUpdateRepository _usuarioUpdateRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AtualizarUsuarioUseCase(IUsuarioReadOnlyRepository usuarioReadOnlyRepository, 
                                       IUsuarioUpdateRepository usuarioUpdateRepository, 
                                       IUnitOfWork unitOfWork)
        {
            _usuarioReadOnlyRepository = usuarioReadOnlyRepository;
            _usuarioUpdateRepository = usuarioUpdateRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<ResponseAtualizarUsuarioJson> Execute(long id, RequestAtualizarUsuarioJson request)
        {
            //validar requisição
            //buscar usuário pelo id
            //verificar se usuário existe - se é null
            //verificar quais campos do request foram preenchidos
                //os que foram preenchidos, armazenar/atualizar no objeto da entidade
            //retornar objeto de resposta

            var usuarioBD = await _usuarioReadOnlyRepository.RecuperarUsuarioPorId(id);

            if (usuarioBD is null)
                throw new NotFoundException(ValidatorsRulesResourceMessages.USUARIO_NAO_ENCONTRADO);
            
            await ValidateRequest(usuarioBD, request);

            if (!string.IsNullOrWhiteSpace(request.Nome))
                usuarioBD.Nome = request.Nome;

            if (!string.IsNullOrWhiteSpace(request.Apelido))
                usuarioBD.Apelido = request.Apelido;

            _usuarioUpdateRepository.AtualizarUsuario(usuarioBD);
            await _unitOfWork.SalvarMudancas();

            var usuarioUpdateResponse = usuarioBD.Adapt<ResponseAtualizarUsuarioJson>();
            return usuarioUpdateResponse;
        }

        private async Task ValidateRequest(Usuario usuarioBD, RequestAtualizarUsuarioJson request)
        {
            var validator = new RequestAtualizarUsuarioValidator(usuarioBD);
            var validationResult = validator.Validate(request);

            List<string> errorMessages = new List<string>();

            if (!validationResult.IsValid)
            {
                foreach (var failure in validationResult.Errors)
                {
                    var errorMessage = failure.ErrorMessage;
                    errorMessages.Add(errorMessage);
                }

                throw new ErrorOnValidationException(errorMessages);
            }

            if (request.Nome is not null && request.Nome.Equals(usuarioBD.Nome))
            {
                request.Nome = null;
            }

            if(request.Apelido is not null)
            {
                if (request.Apelido.Equals(usuarioBD.Apelido))
                {
                    request.Apelido = null;
                    return;
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
}
