using ControleDeAcesso26.Domain.Interfaces.IUsuario;
using ControleDeAcesso26.Infrastructure.DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ControleDeAcesso26.Infrastructure.DependencyInjection.AddDIEntityRepository
{
    public static class DIUsuarioRepository
    {
        public static void Add(IServiceCollection services)
        {
            services.AddScoped<IUsuarioReadOnlyRepository, UsuarioRepository>();
            services.AddScoped<IUsuarioWriteRepository, UsuarioRepository>();
            services.AddScoped<IUsuarioUpdateRepository, UsuarioRepository>();
        }
    }
}
