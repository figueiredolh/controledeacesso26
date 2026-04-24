using Bogus;
using ControleDeAcesso26.Communication.Requests.RequestsUsuario;

namespace TestUtilities.Builders.UsuarioBuilders.Requests
{
    public class RequestAtualizarReativarUsuarioJsonBuilder
    {
        public static RequestAtualizarReativarUsuarioJson Build()
        {
            return new Faker<RequestAtualizarReativarUsuarioJson>()
                .RuleFor(request => request.Ativo, () => true);
        }
    }
}
