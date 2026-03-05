using ControleDeAcesso26.Application.UseCases.BiometriaUsuarioUseCases;
using ControleDeAcesso26.Application.UseCases.BiometriaUsuarioUseCases.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControleDeAcesso26.Application.DependencyInjection.AddDIEntityUseCases
{
    internal static class DIBiometriaUsuario
    {
        internal static void Add(IServiceCollection services)
        {
            services.AddScoped<ICadastrarBiometriaUsuarioUseCase, CadastrarBiometriaUsuarioUseCase>();
            services.AddScoped<IExcluirBiometriaUsuarioUseCase, ExcluirBiometriaUsuarioUseCase>();
        }
    }
}
