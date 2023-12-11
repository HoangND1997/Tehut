using FluentMigrator;
using Tehut.Database.Schemas;

namespace Tehut.Database.Migrator.Migrations
{
    [Migration(3)]
    public class _03_CreateQuizAnswerTable : Migration
    {
        private const string foreignKeyName = $"FK_Answers_Questions_Id";

        public override void Up()
        {
            Create.Table(QuizAnswerTable.TableName)
                .WithColumn(QuizAnswerTable.Id).AsInt32().PrimaryKey().Identity()
                .WithColumn(QuizAnswerTable.Answer).AsString().Nullable()
                .WithColumn(QuizAnswerTable.Question).AsInt32().Nullable().ForeignKey(foreignKeyName, QuizQuestionTable.TableName, QuizQuestionTable.Id); 
        }

        public override void Down()
        {
            Delete.ForeignKey(foreignKeyName).OnTable(QuizAnswerTable.TableName);
            Delete.Table(QuizAnswerTable.TableName); 
        }
    }
}
