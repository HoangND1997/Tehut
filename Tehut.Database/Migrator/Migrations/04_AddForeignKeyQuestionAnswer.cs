using FluentMigrator;
using Tehut.Database.Schemas;

namespace Tehut.Database.Migrator.Migrations
{
    [Migration(4)]
    public class _04_AddForeignKeyQuestionAnswer : Migration
    {
        private const string foreignKeyName = "FK_Questions_Answers"; 

        public override void Up()
        {
            Execute.Sql($"""Alter Table {QuizQuestionTable.TableName} Add Column {QuizQuestionTable.CorrectAnswer} References {QuizAnswerTable.TableName}({QuizAnswerTable.Id});""");
        }

        public override void Down()
        {
            Delete.ForeignKey(foreignKeyName).OnTable(QuizQuestionTable.TableName);
            Delete.Column(QuizQuestionTable.CorrectAnswer).FromTable(QuizQuestionTable.TableName);
        }
    }
}
