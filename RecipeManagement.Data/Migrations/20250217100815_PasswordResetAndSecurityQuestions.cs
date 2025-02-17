using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeManagement.Data.Migrations
{
    /// <inheritdoc />
    public partial class PasswordResetAndSecurityQuestions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PasswordResetToken",
                table: "Users",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PasswordResetTokenExpiration",
                table: "Users",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SecurityQuestionId",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PasswordResetTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    Token = table.Column<string>(type: "TEXT", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasswordResetTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PasswordResetTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SecurityQuestions",
                columns: table => new
                {
                    SecurityQuestionId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    Question = table.Column<string>(type: "TEXT", nullable: false),
                    Answer = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecurityQuestions", x => x.SecurityQuestionId);
                });

                // Insert a default security question record for all existing users (assigning UserId to the first user)
                migrationBuilder.Sql(@"
                INSERT INTO SecurityQuestions (Question, Answer, UserId)
                VALUES ('Default security question?', 'Default answer', 1)  -- Assuming UserId = 1 exists
                ");

                // After that, update existing users to reference this default security question
                migrationBuilder.Sql(@"
                UPDATE Users
                SET SecurityQuestionId = (SELECT SecurityQuestionId FROM SecurityQuestions WHERE Question = 'Default security question?')
                ");

    

            migrationBuilder.CreateIndex(
                name: "IX_Users_SecurityQuestionId",
                table: "Users",
                column: "SecurityQuestionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PasswordResetTokens_UserId",
                table: "PasswordResetTokens",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_SecurityQuestions_SecurityQuestionId",
                table: "Users",
                column: "SecurityQuestionId",
                principalTable: "SecurityQuestions",
                principalColumn: "SecurityQuestionId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_SecurityQuestions_SecurityQuestionId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "PasswordResetTokens");

            migrationBuilder.DropTable(
                name: "SecurityQuestions");

            migrationBuilder.DropIndex(
                name: "IX_Users_SecurityQuestionId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PasswordResetToken",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PasswordResetTokenExpiration",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SecurityQuestionId",
                table: "Users");
        }
    }
}
