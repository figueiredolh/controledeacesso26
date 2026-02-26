namespace ControleDeAcesso26.Application.UseCases.BiometriaUsuarioUseCases.Interfaces
{
    public interface ICadastrarBiometriaUsuarioUseCase
    {
        public Task Execute(long idUsuario);
    }
}
