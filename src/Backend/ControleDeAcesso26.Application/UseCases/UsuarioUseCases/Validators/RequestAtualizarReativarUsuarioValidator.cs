using ControleDeAcesso26.Communication.Requests.RequestsUsuario;
using ControleDeAcesso26.Exceptions.ValidatorsRulesResourceMessages;
using FluentValidation;

namespace ControleDeAcesso26.Application.UseCases.UsuarioUseCases.Validators
{
    public class RequestAtualizarReativarUsuarioValidator : AbstractValidator<RequestAtualizarReativarUsuarioJson>
    {
        public RequestAtualizarReativarUsuarioValidator()
        {
            RuleFor(req => req.Ativo).Must(ativo => ativo == true).WithMessage(ValidatorsRulesResourceMessages.SEM_ALTERACOES_FALHA);
        }
    }
}
