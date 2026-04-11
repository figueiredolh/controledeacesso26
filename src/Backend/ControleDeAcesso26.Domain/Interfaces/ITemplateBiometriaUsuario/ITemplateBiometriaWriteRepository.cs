using ControleDeAcesso26.Domain.Entities;

namespace ControleDeAcesso26.Domain.Interfaces.ITemplateBiometriaUsuario
{
    public interface ITemplateBiometriaWriteRepository
    {
        public Task ArmazenarTemplate(TemplateBiometriaUsuario template);
    }
}
