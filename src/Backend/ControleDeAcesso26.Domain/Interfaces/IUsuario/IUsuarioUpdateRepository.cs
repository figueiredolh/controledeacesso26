using ControleDeAcesso26.Domain.Entities;

namespace ControleDeAcesso26.Domain.Interfaces.IUsuario
{
    public interface IUsuarioUpdateRepository
    {
        public void AtualizarUsuario(Usuario usuario);
    }
}
