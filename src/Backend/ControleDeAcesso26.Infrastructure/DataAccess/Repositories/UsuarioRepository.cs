using ControleDeAcesso26.Domain.Entities;
using ControleDeAcesso26.Domain.Interfaces.IUsuario;
using ControleDeAcesso26.Infrastructure.DataAccess.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace ControleDeAcesso26.Infrastructure.DataAccess.Repositories
{
    public class UsuarioRepository : IUsuarioReadOnlyRepository, IUsuarioWriteRepository, IUsuarioUpdateRepository, IUsuarioDeleteRepository
    {
        private readonly ControleDeAcesso26DbContext dbContext;
        public UsuarioRepository(ControleDeAcesso26DbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task<List<Usuario>> RecuperarUsuarios(bool incluirInativos)
        {
            var IQueryableUsuarios = dbContext.Usuarios.AsNoTracking();

            if (incluirInativos == false)
            {
                return await IQueryableUsuarios.Where(user => user.Ativo == true).ToListAsync();
            }

            return await IQueryableUsuarios.ToListAsync();
        }

        public async Task<Usuario?> RecuperarUsuarioPorId(long id, bool usuarioAtivo = true, bool asNoTracking = false)
        {
            var usuarioDbSet = dbContext.Usuarios;

            if (asNoTracking)
            {
                return await usuarioDbSet.AsNoTracking().FirstOrDefaultAsync(usuario => usuario.Id == id && usuario.Ativo == usuarioAtivo);
            }

            return await usuarioDbSet.FirstOrDefaultAsync(usuario => usuario.Id == id && usuario.Ativo == usuarioAtivo);
        }

        public async Task<bool> ApelidoJaExisteNoSistema(string apelido)
        {
            return await dbContext.Usuarios.AsNoTracking().AnyAsync(usuario => usuario.Apelido.Equals(apelido));
        }

        public async Task CriarUsuario(Usuario usuario)
        {
            await dbContext.Usuarios.AddAsync(usuario);
        }

        public void AtualizarUsuario(Usuario usuario)
        {
            dbContext.Usuarios.Update(usuario);
        }

        public async Task ExcluirUsuarioDefinitivamente(long id)
        {
            await dbContext.Usuarios.Where(usuario => usuario.Id == id).ExecuteDeleteAsync();
        }
    }
}
