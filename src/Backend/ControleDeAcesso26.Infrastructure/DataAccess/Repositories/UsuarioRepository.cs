using ControleDeAcesso26.Domain.Entities;
using ControleDeAcesso26.Domain.Interfaces.IUsuario;
using ControleDeAcesso26.Infrastructure.DataAccess.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace ControleDeAcesso26.Infrastructure.DataAccess.Repositories
{
    public class UsuarioRepository : IUsuarioReadOnlyRepository, IUsuarioWriteRepository
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

        public async Task CriarUsuario(Usuario usuario)
        {
            await dbContext.Usuarios.AddAsync(usuario);
        }

        public async Task<bool> ApelidoJaExisteNoSistema(string apelido)
        {
            return await dbContext.Usuarios.AsNoTracking().AnyAsync(usuario => usuario.Apelido.Equals(apelido));
        }
    }
}
