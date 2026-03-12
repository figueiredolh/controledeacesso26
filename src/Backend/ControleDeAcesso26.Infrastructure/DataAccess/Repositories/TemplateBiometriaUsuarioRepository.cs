using ControleDeAcesso26.Domain.Entities;
using ControleDeAcesso26.Domain.Interfaces.ITemplateBiometriaUsuario;
using ControleDeAcesso26.Infrastructure.DataAccess.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace ControleDeAcesso26.Infrastructure.DataAccess.Repositories
{
    public class TemplateBiometriaUsuarioRepository : ITemplateBiometriaWriteRepository, ITemplateBiometriaReadOnlyRepository, ITemplateBiometriaDeleteRepository
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
        public async Task<List<TemplateBiometriaUsuario>> BuscarTemplates(int? idUsuario = null, int paginaAtual = 1, int tamanhoPagina = 10)
        {
            int pagesToSkip = (paginaAtual - 1) * tamanhoPagina;
            var query = _dbContext.TemplatesBiometriaUsuario.AsNoTracking();

            if (idUsuario is not null && idUsuario > 0)
            {
                query = query.Where(t => t.Usuario.Id == idUsuario);
            }

            return await query.Include(t => t.Usuario).OrderBy(t => t.Id).Skip(pagesToSkip).Take(tamanhoPagina).ToListAsync();
        }
        public async Task<bool> IdSensor1JaExiste(int idSensor)
        {
            return await _dbContext.TemplatesBiometriaUsuario.AsNoTracking().AnyAsync(t => t.IdSensor1 == idSensor);
        }

        public async Task<TemplateBiometriaUsuario?> BuscarTemplatePorId(int idSensor)
        {
            return await _dbContext.TemplatesBiometriaUsuario.Include(usuario => usuario.Usuario).AsNoTracking().FirstOrDefaultAsync(template => template.IdSensor1 == idSensor);
        }
        //atualização
        //exclusão
        public async Task ExcluirTemplate(int idSensor)
        {
            await _dbContext.TemplatesBiometriaUsuario.Where(template => template.IdSensor1 == idSensor).ExecuteDeleteAsync();
        }
    }
}
