using Bogus;
using ControleDeAcesso26.Domain.Entities;

namespace TestUtilities.Builders.UsuarioBuilders.Entity
{
    public static class UsuarioBuilder
    {
        public static Usuario Build()
        {
            return new Faker<Usuario>()
                .RuleFor(usuario => usuario.Id, u => u.IndexFaker + 1)
                .RuleFor(usuario => usuario.DataCriacao, u => DateTime.Now)
                .RuleFor(usuario => usuario.Ativo, u => true)
                .RuleFor(usuario => usuario.Nome, u => u.Name.FullName())
                .RuleFor(usuario => usuario.Apelido, u => u.Internet.UserName());
        }

        public static Usuario Build(string? nome, string? apelido = null)
        {
            string _nome = nome ?? new Faker().Name.FullName();
            string _apelido = apelido ?? new Faker().Internet.UserName();

            return new Faker<Usuario>()
                .RuleFor(usuario => usuario.Id, u => u.IndexFaker + 1)
                .RuleFor(usuario => usuario.DataCriacao, u => DateTime.Now)
                .RuleFor(usuario => usuario.Ativo, u => true)
                .RuleFor(usuario => usuario.Nome, () => _nome)
                .RuleFor(usuario => usuario.Apelido, () => _apelido);
        }
    }
}
