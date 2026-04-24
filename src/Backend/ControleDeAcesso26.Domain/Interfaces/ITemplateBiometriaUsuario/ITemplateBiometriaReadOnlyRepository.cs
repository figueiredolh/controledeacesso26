using ControleDeAcesso26.Domain.Entities;

namespace ControleDeAcesso26.Domain.Interfaces.ITemplateBiometriaUsuario
{
    public interface ITemplateBiometriaReadOnlyRepository
    {
        public Task<List<TemplateBiometriaUsuario>> BuscarTemplates(int? idUsuario = null, int currentPage = 1, int pageSize = 10);
        public Task<bool> IdSensor1JaExiste(int idSensor);
        public Task<TemplateBiometriaUsuario?> BuscarTemplatePorId(int idSensor);
        public Task<bool> UsuarioAtivo(int idSensor, int sensor = 1);
    }
}
