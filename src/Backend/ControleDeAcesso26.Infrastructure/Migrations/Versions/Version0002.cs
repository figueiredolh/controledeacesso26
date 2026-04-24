using ControleDeAcesso26.Infrastructure.DatabaseMigrationTablesVersion;
using FluentMigrator;

namespace ControleDeAcesso26.Infrastructure.Migrations.Versions
{
    [Migration(DatabaseTablesVersion.TemplatesBiometriaUsuario, "Criação da tabela TemplatesBiometriaUsuario")]
    public class Version0002 : VersionBase
    {
        public override void Up()
        {
            CreateTable("TemplatesBiometriaUsuario")
                .WithColumn("IdSensor1").AsInt64().NotNullable().Unique()
                .WithColumn("IdSensor2").AsInt64().Nullable().Unique()
                .WithColumn("Template").AsBinary(512).NotNullable()
                .WithColumn("IdUsuario").AsInt64().NotNullable();

            Create.ForeignKey("FK_Usuarios_TemplatesBiometriaUsuario")
                .FromTable("TemplatesBiometriaUsuario").ForeignColumn("IdUsuario")
                .ToTable("Usuarios").PrimaryColumn("Id");
        }
    }
}
