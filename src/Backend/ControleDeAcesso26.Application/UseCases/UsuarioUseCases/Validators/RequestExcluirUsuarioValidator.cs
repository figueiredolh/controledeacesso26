using ControleDeAcesso26.Communication.Requests.RequestsUsuario;
using ControleDeAcesso26.Exceptions.ValidatorsRulesResourceMessages;
using FluentValidation;

namespace ControleDeAcesso26.Application.UseCases.UsuarioUseCases.Validators
{
    public class RequestExcluirUsuarioValidator : AbstractValidator<RequestExcluirUsuarioJson>
    {
        public RequestExcluirUsuarioValidator()
        {
            RuleFor(req => req.Ativo).Must(ativo => ativo == false).WithMessage(ValidatorsRulesResourceMessages.SEM_ALTERACOES_FALHA);
        }
    }
}
