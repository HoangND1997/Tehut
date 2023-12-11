using FluentMigrator;
using Tehut.Database.Schemas;

namespace Tehut.Database.Migrator.Migrations
{
    [Migration(1)]
    public class _01_CreateQuizTable : Migration
    {
        public override void Up()
        {
            Create.Table(QuizTable.TableName)
                .WithColumn(QuizTable.Id).AsInt32().PrimaryKey().Identity()
                .WithColumn(QuizTable.Name).AsString().Nullable(); 
        }

        public override void Down()
        {
            Delete.Table(QuizTable.TableName); 
        }
    }
}
