using ControleDeAcesso26.Application.DependencyInjection.AddDIEntityUseCases;
using Microsoft.Extensions.DependencyInjection;

namespace ControleDeAcesso26.Application.DependencyInjection
{
    public static class ApplicationDependencyInjection
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            AddUseCases(services);
        }

        private static void AddUseCases(IServiceCollection services)
        {
            DIUsuarioUseCase.Add(services);
            DIBiometriaUsuario.Add(services);
            //DIRfidUsuario.Add(services);
        }
    }
}
