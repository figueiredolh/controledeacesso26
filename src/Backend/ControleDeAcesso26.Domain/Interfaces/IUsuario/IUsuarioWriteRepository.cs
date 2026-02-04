using ControleDeAcesso26.Domain.Entities;

namespace ControleDeAcesso26.Domain.Interfaces.IUsuario
{
    public interface IUsuarioWriteRepository
    {
        public Task CriarUsuario(Usuario usuario);
    }
}
