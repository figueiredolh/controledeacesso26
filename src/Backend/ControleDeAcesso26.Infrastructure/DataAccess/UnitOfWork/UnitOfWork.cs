using ControleDeAcesso26.Domain.Interfaces.IUnitOfWork;
using ControleDeAcesso26.Infrastructure.DataAccess.DatabaseContext;

namespace ControleDeAcesso26.Infrastructure.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ControleDeAcesso26DbContext dbContext;

        public UnitOfWork(ControleDeAcesso26DbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task SalvarMudancas()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
