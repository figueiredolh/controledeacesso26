using Dapper;
using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;

namespace ControleDeAcesso26.Infrastructure.Migrations
{
    public static class DatabaseMigration
    {
        public static void MigrateDatabase(IConfiguration configuration, IServiceProvider serviceProvider)
        {
            EnsureMySqlSchemaDatabaseCreated(configuration);
            RunnerMigrateUp(serviceProvider);
        }

        private static void EnsureMySqlSchemaDatabaseCreated(IConfiguration configuration)
        {
            //recuperar schema do banco de dados            
            //verificar se o schema existe no banco de dados (consultar sem o nome do schema na connection string)
            //se existir, ignorar
            //se não existir, criar o schema
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var mySqlConnectionStringBuilder = new MySqlConnectionStringBuilder(connectionString!);

            var schemaName = mySqlConnectionStringBuilder.Database;
            mySqlConnectionStringBuilder.Remove("Database");

            var parameters = new DynamicParameters();
            parameters.Add("nome", schemaName);

            using var dbConnection = new MySqlConnection(mySqlConnectionStringBuilder.ConnectionString);
            var sqlCheck = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = @nome";

            var countRecords = dbConnection.ExecuteScalar<int>(sqlCheck, parameters);

            if (countRecords == 0)
            {
                dbConnection.Execute($"CREATE DATABASE {schemaName}");
            }
        }

        private static void RunnerMigrateUp(IServiceProvider serviceProvider)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
        }
    }
}
