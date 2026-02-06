using ControleDeAcesso26.Application.UseCases.UsuarioUseCases.Validators;
using ControleDeAcesso26.Exceptions.ValidatorsRulesResourceMessages;
using Shouldly;
using TestUtilities.Builders.Requests;

namespace Validators.Test
{
    public class RequestCriarUsuarioValidatorTest
    {
        [Fact]
        public void Success()
        {
            var request = RequestCriarUsuarioJsonBuilder.Build();

            var validator = new RequestCriarUsuarioValidator();
            var validationResult = validator.Validate(request);

            validationResult.IsValid.ShouldBeTrue();
            validationResult.Errors.Count.ShouldBe(0);
        }

        [Fact]
        public void FailureOnNomeEmptyProperty()
        {
            var request = RequestCriarUsuarioJsonBuilder.Build();
            request.Nome = "";

            var validator = new RequestCriarUsuarioValidator();
            var validationResult = validator.Validate(request);

            validationResult.IsValid.ShouldBeFalse();
            validationResult.Errors.Count.ShouldBe(1);
            validationResult.Errors.Select(error => error.ErrorMessage).FirstOrDefault().ShouldBe(ValidatorsRulesResourceMessages.USUARIO_NOME_VAZIO);
        }

        [Fact]
        public void FailureOnNomeAndApelidoNullProperty()
        {
            var request = RequestCriarUsuarioJsonBuilder.Build();
            request.Nome = null!;
            request.Apelido = null!;

            var validator = new RequestCriarUsuarioValidator();
            var validationResult = validator.Validate(request);

            validationResult.IsValid.ShouldBeFalse();
            validationResult.Errors.Count.ShouldBe(2);
            var listErrorMessages = validationResult.Errors.Select(error => error.ErrorMessage);

            listErrorMessages.ShouldContain(ValidatorsRulesResourceMessages.USUARIO_NOME_VAZIO);
            listErrorMessages.ShouldContain(ValidatorsRulesResourceMessages.USUARIO_APELIDO_VAZIO);
        }

        [Fact]
        public void FailureOnApelidoEmptyProperty()
        {
            var request = RequestCriarUsuarioJsonBuilder.Build();
            request.Apelido = "";

            var validator = new RequestCriarUsuarioValidator();
            var validationResult = validator.Validate(request);

            validationResult.IsValid.ShouldBeFalse();
            validationResult.Errors.Count.ShouldBe(1);
            validationResult.Errors.Select(error => error.ErrorMessage).FirstOrDefault().ShouldBe(ValidatorsRulesResourceMessages.USUARIO_APELIDO_VAZIO);
        }
    }
}
