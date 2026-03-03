using ControleDeAcesso26.Domain.Entities;
using ControleDeAcesso26.Domain.Interfaces.ITemplateBiometriaUsuario;
using ControleDeAcesso26.Infrastructure.DataAccess.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace ControleDeAcesso26.Infrastructure.DataAccess.Repositories
{
    public class TemplateBiometriaUsuarioRepository : ITemplateBiometriaWriteRepository, ITemplateBiometriaReadOnlyRepository
    {
        private readonly ControleDeAcesso26DbContext _dbContext;
        public TemplateBiometriaUsuarioRepository(ControleDeAcesso26DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //criação
        public async Task ArmazenarTemplate(TemplateBiometriaUsuario template)
        {
            await _dbContext.TemplatesBiometriaUsuario.AddAsync(template);
        }
        //leitura
        public async Task<bool> IdSensor1JaExiste(long idSensor)
        {
            return await _dbContext.TemplatesBiometriaUsuario.AsNoTracking().AnyAsync(t => t.IdSensor1 == idSensor);
        }
        //atualização
        //exclusão
    }
}
