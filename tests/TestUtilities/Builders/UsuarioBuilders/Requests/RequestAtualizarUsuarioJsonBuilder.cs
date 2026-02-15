using Bogus;
using ControleDeAcesso26.Communication.Requests.RequestsUsuario;

namespace TestUtilities.Builders.UsuarioBuilders.Requests
{
    public static class RequestAtualizarUsuarioJsonBuilder
    {
        public static RequestAtualizarUsuarioJson Build()
        {
            return new Faker<RequestAtualizarUsuarioJson>()
                .RuleFor(request => request.Nome, (f) => f.Name.FullName())
                .RuleFor(request => request.Apelido, (f) => f.Internet.UserName());
        }
    }
}
