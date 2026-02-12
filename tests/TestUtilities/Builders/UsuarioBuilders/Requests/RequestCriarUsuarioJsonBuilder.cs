using Bogus;
using ControleDeAcesso26.Communication.Requests.RequestsUsuario;

namespace TestUtilities.Builders.UsuarioBuilders.Requests
{
    public static class RequestCriarUsuarioJsonBuilder
    {
        public static RequestCriarUsuarioJson Build()
        {
            return new Faker<RequestCriarUsuarioJson>()
                .RuleFor(request => request.Nome, (f) => f.Name.FullName())
                .RuleFor(request => request.Apelido, (f) => f.Internet.UserName());
        }
    }
}
