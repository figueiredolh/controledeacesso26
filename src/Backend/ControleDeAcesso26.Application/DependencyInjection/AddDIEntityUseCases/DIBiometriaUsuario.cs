using ControleDeAcesso26.Application.UseCases.BiometriaUsuarioUseCases;
using ControleDeAcesso26.Application.UseCases.BiometriaUsuarioUseCases.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ControleDeAcesso26.Application.DependencyInjection.AddDIEntityUseCases
{
    internal static class DIBiometriaUsuario
    {
        internal static void Add(IServiceCollection services)
        {
            services.AddScoped<ICadastrarBiometriaUsuarioUseCase, CadastrarBiometriaUsuarioUseCase>();
            services.AddScoped<IExcluirBiometriaUsuarioUseCase, ExcluirBiometriaUsuarioUseCase>();
            services.AddScoped<IListarBiometriasUseCase, ListarBiometriasUseCase>();
            services.AddScoped<IVerificarBiometriaUsuarioUseCase, VerificarBiometriaUsuarioUseCase>();
            services.AddScoped<ILimparDatabaseBiometriaUsuarioUseCase, LimparDatabaseBiometriaUsuarioUseCase>();
        }
    }
}
