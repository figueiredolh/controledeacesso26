using ControleDeAcesso26.Infrastructure.DataAccess.DatabaseContext;
using ControleDeAcesso26.Infrastructure.DependencyInjection.AddDIEntityRepository;
using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ControleDeAcesso26.Infrastructure.DependencyInjection
{
    public static class InfrastructureDependencyInjection
    {
        public static void AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            AddDatabaseContext(services, configuration);
            AddRepositories(services);
            AddMigrationServices(services);
        }

        private static void AddDatabaseContext(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ControleDeAcesso26DbContext>(options =>
                options.UseMySQL(connectionString!));

            //---- Para Pomelo.EntityFrameworkCore.MySql

            /*services.AddDbContext<ControleDeAcesso26DbContext>(options =>
                options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 37))));*/

            /*services.AddDbContext<ControleDeAcesso26DbContext>(options => 
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));*/
        }

        private static void AddRepositories(IServiceCollection services)
        {
            DIUnitOfWork.Add(services);
            DIUsuarioRepository.Add(services);
        }

        private static void AddMigrationServices(IServiceCollection services)
        {
            services.AddFluentMigratorCore().ConfigureRunner(rb => 
                                                             rb.AddMySql8().WithGlobalConnectionString("DefaultConnection")
                                                             .ScanIn(typeof(InfrastructureDependencyInjection).Assembly));
        }
    }
}
