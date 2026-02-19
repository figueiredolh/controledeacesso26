using Bogus;
using ControleDeAcesso26.Communication.Requests.RequestsUsuario;

namespace TestUtilities.Builders.UsuarioBuilders.Requests
{
    public static class RequestExcluirUsuarioJsonBuilder
    {
        public static RequestExcluirUsuarioJson Build()
        {
            return new Faker<RequestExcluirUsuarioJson>()
                .RuleFor(request => request.Ativo, () => false);
        }
    }
}
