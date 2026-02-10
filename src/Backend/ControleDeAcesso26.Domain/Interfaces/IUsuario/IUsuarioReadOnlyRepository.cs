using ControleDeAcesso26.Domain.Entities;

namespace ControleDeAcesso26.Domain.Interfaces.IUsuario
{
    public interface IUsuarioReadOnlyRepository
    {
        public Task<List<Usuario>> RecuperarUsuarios(bool incluirInativos);
        public Task<Usuario?> RecuperarUsuarioPorId(long id, bool usuarioAtivo = true);
        public Task<bool> ApelidoJaExisteNoSistema(string apelido);
    }
}
