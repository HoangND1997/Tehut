using FluentMigrator;
using Tehut.Database.Schemas;

namespace Tehut.Database.Migrator.Migrations
{
    [Migration(2)]
    public class _02_CreateQuizQuestionTable : Migration
    {
        private const string quizForeignKeyName = $"FK_Questions_Quizzes_Id"; 

        public override void Up()
        {
            Create.Table(QuizQuestionTable.TableName)
                .WithColumn(QuizQuestionTable.Id).AsInt32().PrimaryKey().Identity()
                .WithColumn(QuizQuestionTable.Question).AsString().Nullable()
                .WithColumn(QuizQuestionTable.Quiz).AsInt32().ForeignKey(quizForeignKeyName, QuizTable.TableName, QuizTable.Id).Nullable(); 
        }

        public override void Down()
        {
            Delete.ForeignKey(quizForeignKeyName).OnTable(QuizQuestionTable.TableName); 
            Delete.Table(QuizQuestionTable.TableName);   
        }
    }
}
