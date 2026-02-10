using ControleDeAcesso26.API.Controllers.Base;
using ControleDeAcesso26.Application.UseCases.UsuarioUseCases.Interfaces;
using ControleDeAcesso26.Communication.Requests.RequestsUsuario;
using ControleDeAcesso26.Communication.Responses.ResponsesUsuario;
using ControleDeAcesso26.Exceptions.Exceptions.ResponseError;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeAcesso26.API.Controllers
{
    public class UsuarioController : ControleDeAcesso26ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(ResponseRecuperarUsuariosJson), StatusCodes.Status200OK)]
        public async Task<IActionResult> RecuperarUsuarios([FromServices] IRecuperarUsuariosUseCase recuperarUsuariosUseCase,
                                                           [FromQuery] bool incluirInativos = false)
        {
            var listaDeUsuariosResult = await recuperarUsuariosUseCase.Execute(incluirInativos);
            return Ok(listaDeUsuariosResult);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseCriarUsuarioJson), StatusCodes.Status201Created)]
        [ProducesErrorResponseType(typeof(ResponseErrorJson))]
        public async Task<IActionResult> CriarUsuario([FromServices] ICriarUsuarioUseCase criarUsuarioUseCase,
                                         [FromBody] RequestCriarUsuarioJson requestCriarUsuario)
        {
            var usuarioResult = await criarUsuarioUseCase.Execute(requestCriarUsuario);
            return Created(string.Empty, usuarioResult);
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(ResponseAtualizarUsuarioJson), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ResponseErrorJson))]
        public async Task<IActionResult> AtualizarUsuario(long id, [FromServices] IAtualizarUsuarioUseCase atualizarUsuarioUseCase, 
                                                         [FromBody] RequestAtualizarUsuarioJson requestAtualizarUsuario)
        {
            var usuarioUpdateResult = await atualizarUsuarioUseCase.Execute(id, requestAtualizarUsuario);
            return Ok(usuarioUpdateResult);
        }

        [HttpPatch("reativar/{id}")]
        [ProducesResponseType(typeof(ResponseAtualizarReativarUsuarioJson), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ResponseErrorJson))]
        public async Task<IActionResult> AtualizarReativarUsuario(long id, [FromServices] IAtualizarReativarUsuarioUseCase atualizarReativarUsuarioUseCase,
                                                         [FromBody] RequestAtualizarReativarUsuarioJson requestAtualizarReativarUsuario)
        {
            var usuarioUpdateResult = await atualizarReativarUsuarioUseCase.Execute(id, requestAtualizarReativarUsuario);
            return Ok(usuarioUpdateResult);
        }
    }
}
