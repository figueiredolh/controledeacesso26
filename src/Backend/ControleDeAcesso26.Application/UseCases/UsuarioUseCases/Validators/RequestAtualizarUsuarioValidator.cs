using ControleDeAcesso26.Communication.Requests.RequestsUsuario;
using ControleDeAcesso26.Domain.Entities;
using ControleDeAcesso26.Exceptions.ValidatorsRulesResourceMessages;
using FluentValidation;

namespace ControleDeAcesso26.Application.UseCases.UsuarioUseCases.Validators
{
    public class RequestAtualizarUsuarioValidator : AbstractValidator<RequestAtualizarUsuarioJson>
    {
        public RequestAtualizarUsuarioValidator(Usuario usuarioBD)
        {
            //validação falha caso nenhum dos atributos (Nome e Apelido) sejam preenchidos no objeto de atualização
            When(req => string.IsNullOrWhiteSpace(req.Nome), () =>
            {
                RuleFor(req => req.Apelido).NotEmpty().WithMessage(ValidatorsRulesResourceMessages.USUARIO_FALHA_ATUALIZAR);
                
                RuleFor(req => req.Apelido).Must(apelido => !(apelido is not null && apelido.Equals(usuarioBD.Apelido)))
                                           .WithMessage(ValidatorsRulesResourceMessages.USUARIO_FALHA_ATUALIZAR_3);
            });

            When(req => !string.IsNullOrWhiteSpace(req.Nome) && req.Nome.Equals(usuarioBD.Nome), () =>
            {
                RuleFor(req => req.Apelido).Must(apelido => !(apelido is not null && apelido.Equals(usuarioBD.Apelido)))
                                           .WithMessage(ValidatorsRulesResourceMessages.USUARIO_FALHA_ATUALIZAR_2);
            });

            When(req => string.IsNullOrWhiteSpace(req.Apelido), () =>
            {
                RuleFor(req => req.Nome).Must(nome => !(nome is not null && nome.Equals(usuarioBD.Nome)))
                                           .WithMessage(ValidatorsRulesResourceMessages.USUARIO_FALHA_ATUALIZAR_4);
            });
        }
    }
}
