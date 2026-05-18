namespace ControleDeAcesso26.Domain.Interfaces.ITemplateBiometriaUsuario
{
    public interface ITemplateBiometriaDeleteRepository
    {
        public Task ExcluirTemplate(int idSensor);
        public Task LimparDatabase();
    }
}
