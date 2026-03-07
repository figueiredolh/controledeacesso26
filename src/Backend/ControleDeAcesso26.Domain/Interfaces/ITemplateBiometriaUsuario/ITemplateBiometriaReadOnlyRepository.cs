using ControleDeAcesso26.Domain.Entities;

namespace ControleDeAcesso26.Domain.Interfaces.ITemplateBiometriaUsuario
{
    public interface ITemplateBiometriaReadOnlyRepository
    {
        public Task<bool> IdSensor1JaExiste(int idSensor);
        public Task<TemplateBiometriaUsuario?> BuscarTemplatePorId(int idSensor);
    }
}
