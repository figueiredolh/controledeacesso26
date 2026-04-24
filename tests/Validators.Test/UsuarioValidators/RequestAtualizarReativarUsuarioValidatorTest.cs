using ControleDeAcesso26.Application.UseCases.UsuarioUseCases.Validators;
using ControleDeAcesso26.Exceptions.ValidatorsRulesResourceMessages;
using Shouldly;
using TestUtilities.Builders.UsuarioBuilders.Requests;

namespace Validators.Test.UsuarioValidators
{
    public class RequestAtualizarReativarUsuarioValidatorTest
    {
        [Fact]
        public void Success()
        {
            var request = RequestAtualizarReativarUsuarioJsonBuilder.Build();
            var validator = new RequestAtualizarReativarUsuarioValidator();

            var validatorResult = validator.Validate(request);

            validatorResult.IsValid.ShouldBe(true);
            validatorResult.Errors.ShouldBeEmpty();
        }

        [Fact]
        public void FailureRequestAtivoAsFalse()
        {
            var request = RequestAtualizarReativarUsuarioJsonBuilder.Build();
            request.Ativo = false;

            var validator = new RequestAtualizarReativarUsuarioValidator();

            var validatorResult = validator.Validate(request);

            validatorResult.IsValid.ShouldBe(false);
            validatorResult.Errors.Count.ShouldBe(1);
            validatorResult.Errors.Select(error => error.ErrorMessage).FirstOrDefault().ShouldBe(ValidatorsRulesResourceMessages.SEM_ALTERACOES_FALHA);
        }
    }
}
