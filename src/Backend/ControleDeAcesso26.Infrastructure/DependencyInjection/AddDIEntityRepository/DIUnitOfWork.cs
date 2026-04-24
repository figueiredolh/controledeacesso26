using ControleDeAcesso26.Domain.Interfaces.IUnitOfWork;
using ControleDeAcesso26.Infrastructure.DataAccess.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace ControleDeAcesso26.Infrastructure.DependencyInjection.AddDIEntityRepository
{
    public static class DIUnitOfWork
    {
        public static void Add(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
