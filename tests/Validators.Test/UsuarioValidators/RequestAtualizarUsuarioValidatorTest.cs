using ControleDeAcesso26.Application.UseCases.UsuarioUseCases.Validators;
using ControleDeAcesso26.Exceptions.ValidatorsRulesResourceMessages;
using Shouldly;
using TestUtilities.Builders.UsuarioBuilders.Entity;
using TestUtilities.Builders.UsuarioBuilders.Requests;

namespace Validators.Test.UsuarioValidators
{
    public class RequestAtualizarUsuarioValidatorTest
    {
        [Fact]
        public void Success()
        {
            var request = RequestAtualizarUsuarioJsonBuilder.Build();
            var usuarioQualquerExistenteNoBD = UsuarioBuilder.Build();

            var validator = new RequestAtualizarUsuarioValidator(usuarioQualquerExistenteNoBD);
            var validationResult = validator.Validate(request);

            validationResult.IsValid.ShouldBe(true);
            validationResult.Errors.Count.ShouldBe(0);
        }

        [Fact]
        public void FailureNomeAndApelidoEmpties() 
        {
            var request = RequestAtualizarUsuarioJsonBuilder.Build();
            request.Nome = string.Empty;
            request.Apelido = string.Empty;

            var usuarioQualquerExistenteNoBD = UsuarioBuilder.Build();

            var validator = new RequestAtualizarUsuarioValidator(usuarioQualquerExistenteNoBD);
            var validationResult = validator.Validate(request);

            validationResult.IsValid.ShouldBe(false);
            validationResult.Errors.Count.ShouldBe(1);

            validationResult.Errors.Select(error => error.ErrorMessage).FirstOrDefault().ShouldBe(ValidatorsRulesResourceMessages.USUARIO_FALHA_ATUALIZAR);
        }

        [Fact]
        public void FailureNomeEmptyAndApelidoEqualsUsuarioApelido()
        {
            var request = RequestAtualizarUsuarioJsonBuilder.Build();
            request.Nome = string.Empty;

            var usuarioQualquerExistenteNoBD = UsuarioBuilder.Build(null, request.Apelido!);

            var validator = new RequestAtualizarUsuarioValidator(usuarioQualquerExistenteNoBD);
            var validationResult = validator.Validate(request);

            validationResult.IsValid.ShouldBe(false);
            validationResult.Errors.Count.ShouldBe(1);

            validationResult.Errors.Select(error => error.ErrorMessage).FirstOrDefault().ShouldBe(ValidatorsRulesResourceMessages.USUARIO_FALHA_ATUALIZAR_3);
        }

        [Fact]
        public void FailureNomeAndApelidoEqualsUsuarioNomeAndApelido()
        {
            var request = RequestAtualizarUsuarioJsonBuilder.Build();

            var usuarioQualquerExistenteNoBD = UsuarioBuilder.Build(request.Nome!, request.Apelido!);

            var validator = new RequestAtualizarUsuarioValidator(usuarioQualquerExistenteNoBD);
            var validationResult = validator.Validate(request);

            validationResult.IsValid.ShouldBe(false);
            validationResult.Errors.Count.ShouldBe(1);

            validationResult.Errors.Select(error => error.ErrorMessage).FirstOrDefault().ShouldBe(ValidatorsRulesResourceMessages.USUARIO_FALHA_ATUALIZAR_2);
        }

        [Fact]
        public void FailureNomeEqualsUsuarioNomeAndApelidoEmpty()
        {
            var request = RequestAtualizarUsuarioJsonBuilder.Build();
            request.Apelido = string.Empty;

            var usuarioQualquerExistenteNoBD = UsuarioBuilder.Build(request.Nome, null);

            var validator = new RequestAtualizarUsuarioValidator(usuarioQualquerExistenteNoBD);
            var validationResult = validator.Validate(request);

            validationResult.IsValid.ShouldBe(false);
            validationResult.Errors.Count.ShouldBe(1);

            validationResult.Errors.Select(error => error.ErrorMessage).FirstOrDefault().ShouldBe(ValidatorsRulesResourceMessages.USUARIO_FALHA_ATUALIZAR_4);
        }
    }
}
