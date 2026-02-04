using ControleDeAcesso26.API.Controllers.Base;
using ControleDeAcesso26.Application.UseCases.UsuarioUseCases.Interfaces;
using ControleDeAcesso26.Communication.Requests.RequestsUsuario;
using ControleDeAcesso26.Communication.Responses.ResponsesUsuario;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeAcesso26.API.Controllers
{
    public class UsuarioController : ControleDeAcesso26ControllerBase
    {
        [HttpGet(nameof(RecuperarUsuarios))]
        [ProducesResponseType(typeof(ResponseRecuperarUsuariosJson), StatusCodes.Status200OK)]
        public async Task<IActionResult> RecuperarUsuarios([FromServices] IRecuperarUsuariosUseCase recuperaUsuariosUseCase, 
                                                           [FromQuery] bool incluirInativos = false)
        {
            var listaDeUsuariosResult = await recuperaUsuariosUseCase.Execute(incluirInativos);
            return Ok(listaDeUsuariosResult);
        }

        [HttpPost(nameof(CriarUsuario))]
        [ProducesResponseType(typeof(ResponseCriarUsuarioJson), StatusCodes.Status201Created)]
        public async Task<IActionResult> CriarUsuario([FromServices] ICriarUsuarioUseCase criaUsuarioUseCase, 
                                         [FromBody] RequestCriarUsuarioJson requestCriaUsuario)
        {
            var usuarioResult = await criaUsuarioUseCase.Execute(requestCriaUsuario);
            return Created(string.Empty, usuarioResult);
        }
    }
}
