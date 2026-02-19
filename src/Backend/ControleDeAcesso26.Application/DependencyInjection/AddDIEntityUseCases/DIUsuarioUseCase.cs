using ControleDeAcesso26.Application.UseCases.UsuarioUseCases;
using ControleDeAcesso26.Application.UseCases.UsuarioUseCases.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ControleDeAcesso26.Application.DependencyInjection.AddDIEntityUseCases
{
    internal static class DIUsuarioUseCase
    {
        internal static void Add(IServiceCollection services)
        {
            services.AddScoped<ICriarUsuarioUseCase, CriarUsuarioUseCase>();
            services.AddScoped<IRecuperarUsuariosUseCase, RecuperarUsuariosUseCase>();
            services.AddScoped<IAtualizarUsuarioUseCase, AtualizarUsuarioUseCase>();
            services.AddScoped<IAtualizarReativarUsuarioUseCase, AtualizarReativarUsuarioUseCase>();
            services.AddScoped<IExcluirUsuarioUseCase, ExcluirUsuarioUseCase>();
            services.AddScoped<IExcluirUsuarioDefinitivamenteUseCase, ExcluirUsuarioDefinitivamenteUseCase>();
        }
    }
}
