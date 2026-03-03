using ControleDeAcesso26.Domain.Interfaces.ITemplateBiometriaUsuario;
using ControleDeAcesso26.Infrastructure.DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ControleDeAcesso26.Infrastructure.DependencyInjection.AddDIEntityRepository
{
    public static class DITemplateBiometriaUsuarioRepository
    {
        public static void Add(IServiceCollection services)
        {
            services.AddScoped<ITemplateBiometriaWriteRepository, TemplateBiometriaUsuarioRepository>();
            services.AddScoped<ITemplateBiometriaReadOnlyRepository, TemplateBiometriaUsuarioRepository>();
        }
    }
}
