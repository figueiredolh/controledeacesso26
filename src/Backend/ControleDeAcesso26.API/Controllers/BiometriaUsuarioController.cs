using ControleDeAcesso26.API.Controllers.Base;
using ControleDeAcesso26.Application.UseCases.BiometriaUsuarioUseCases.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeAcesso26.API.Controllers
{
    public class BiometriaUsuarioController : ControleDeAcesso26ControllerBase
    {
        [HttpPost("{idUsuario}")]
        public async Task<IActionResult> Cadastrar(long idUsuario, [FromServices] ICadastrarBiometriaUsuarioUseCase useCase)
        {
            var result = useCase.Execute(idUsuario);
            return Ok();
        }
    }
}
