namespace ControleDeAcesso26.Domain.Interfaces.ITemplateBiometriaUsuario
{
    public interface ITemplateBiometriaReadOnlyRepository
    {
        public Task<bool> IdSensor1JaExiste(long idSensor);
    }
}
