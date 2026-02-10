using ControleDeAcesso26.Domain.Entities;

namespace ControleDeAcesso26.Domain.Interfaces.IUsuario
{
    public interface IUsuarioDeleteRepository
    {
        public void AtualizarUsuario(Usuario usuario);
    }
}
