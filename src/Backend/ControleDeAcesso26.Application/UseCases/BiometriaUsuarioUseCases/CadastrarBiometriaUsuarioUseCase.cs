using ControleDeAcesso26.Application.UseCases.BiometriaUsuarioUseCases.Interfaces;

namespace ControleDeAcesso26.Application.UseCases.BiometriaUsuarioUseCases
{
    public class CadastrarBiometriaUsuarioUseCase : ICadastrarBiometriaUsuarioUseCase
    {
        public Task Execute(long idUsuario)
        {
            return Task.CompletedTask;
        }
    }
}
