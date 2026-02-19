using ControleDeAcesso26.Infrastructure.DatabaseMigrationTablesVersion;
using FluentMigrator;

namespace ControleDeAcesso26.Infrastructure.Migrations.Versions
{
    [Migration(DatabaseTablesVersion.Usuarios, "Criação da tabela Usuarios")]
    public class Version0001 : VersionBase
    {
        public override void Up()
        {
            CreateTable("Usuarios")
                .WithColumn("Nome").AsString(200).NotNullable()
                .WithColumn("Apelido").AsString(50).NotNullable().Unique();
        }
    }
}
