using FluentMigrator;
using FluentMigrator.Builders.Create.Table;

namespace ControleDeAcesso26.Infrastructure.Migrations.Versions
{
    public abstract class VersionBase : ForwardOnlyMigration
    {
        public ICreateTableColumnOptionOrWithColumnSyntax CreateTable(string table)
        {
            return Create.Table(table)
                    .WithColumn("Id").AsInt64().NotNullable().PrimaryKey().Identity()
                    .WithColumn("DataCriacao").AsDateTime().NotNullable()
                    .WithColumn("Ativo").AsBoolean().NotNullable();
        }
    }
}
