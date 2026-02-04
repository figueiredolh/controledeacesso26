using ControleDeAcesso26.Communication.Requests.RequestsUsuario;
using ControleDeAcesso26.Exceptions.ValidatorsRulesResourceMessages;
using FluentValidation;

namespace ControleDeAcesso26.Application.UseCases.UsuarioUseCases.Validators
{
    public class RequestCriarUsuarioValidator : AbstractValidator<RequestCriarUsuarioJson>
    {
        public RequestCriarUsuarioValidator()
        {
            RuleFor(req => req.Nome).NotEmpty().WithMessage(ValidatorsRulesResourceMessages.USUARIO_NOME_VAZIO);
            RuleFor(req => req.Apelido).NotEmpty().WithMessage(ValidatorsRulesResourceMessages.USUARIO_APELIDO_VAZIO);
        }
    }
}
