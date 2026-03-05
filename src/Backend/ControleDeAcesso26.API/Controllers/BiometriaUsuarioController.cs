using ControleDeAcesso26.API.Controllers.Base;
using ControleDeAcesso26.Application.UseCases.BiometriaUsuarioUseCases.Interfaces;
using ControleDeAcesso26.Communication.Responses.ResponsesBiometriaUsuario;
using ControleDeAcesso26.Exceptions.Exceptions.ResponseError;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeAcesso26.API.Controllers
{
    public class BiometriaUsuarioController : ControleDeAcesso26ControllerBase
    {
        [HttpPost("{idUsuario}/cadastrar")]
        [ProducesResponseType(typeof(ResponseCadastrarBiometriaUsuarioJson), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ResponseErrorJson))]
        public async Task<IActionResult> Cadastrar(long idUsuario, [FromServices] ICadastrarBiometriaUsuarioUseCase useCase)
        {
            var result = await useCase.Execute(idUsuario);
            return Ok(result);
        }

        [HttpDelete("{idSensor}/excluir")]
        [ProducesResponseType(typeof(ResponseExcluirBiometriaUsuarioJson), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ResponseErrorJson))]
        public async Task<IActionResult> ExcluirTemplate(int idSensor, [FromServices] IExcluirBiometriaUsuarioUseCase useCase)
        {
            var result = await useCase.Execute(idSensor);
            return Ok(result);
        }
    }
}
