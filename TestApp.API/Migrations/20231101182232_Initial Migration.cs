using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TestApp.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuestionTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SurveyTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleID = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Users_UserRoles_RoleID",
                        column: x => x.RoleID,
                        principalTable: "UserRoles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Surveys",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TypeID = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Surveys", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Surveys_SurveyTypes_TypeID",
                        column: x => x.TypeID,
                        principalTable: "SurveyTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Surveys_Users_CreatorUserID",
                        column: x => x.CreatorUserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Assigns",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SurveyID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assigns", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Assigns_Surveys_SurveyID",
                        column: x => x.SurveyID,
                        principalTable: "Surveys",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Assigns_Users_CreatorUserID",
                        column: x => x.CreatorUserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Assigns_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SurveyID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TypeID = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Index = table.Column<int>(type: "int", nullable: false),
                    IsRequired = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Questions_QuestionTypes_TypeID",
                        column: x => x.TypeID,
                        principalTable: "QuestionTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Questions_Surveys_SurveyID",
                        column: x => x.SurveyID,
                        principalTable: "Surveys",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Questions_Users_CreatorUserID",
                        column: x => x.CreatorUserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Submits",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SurveyID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Submits", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Submits_Surveys_SurveyID",
                        column: x => x.SurveyID,
                        principalTable: "Surveys",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Submits_Users_CreatorUserID",
                        column: x => x.CreatorUserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Submits_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "QuestionChoices",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Index = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionChoices", x => x.ID);
                    table.ForeignKey(
                        name: "FK_QuestionChoices_Questions_QuestionID",
                        column: x => x.QuestionID,
                        principalTable: "Questions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_QuestionChoices_Users_CreatorUserID",
                        column: x => x.CreatorUserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuestionChoiceID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Answers_QuestionChoices_QuestionChoiceID",
                        column: x => x.QuestionChoiceID,
                        principalTable: "QuestionChoices",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Answers_Questions_QuestionID",
                        column: x => x.QuestionID,
                        principalTable: "Questions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Answers_Users_CreatorUserID",
                        column: x => x.CreatorUserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Answers_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.InsertData(
                table: "QuestionTypes",
                columns: new[] { "ID", "CreatedAt", "IsActive", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 11, 1, 21, 22, 32, 495, DateTimeKind.Local).AddTicks(5830), true, false, "text" },
                    { 2, new DateTime(2023, 11, 1, 21, 22, 32, 495, DateTimeKind.Local).AddTicks(5832), true, false, "true/false" },
                    { 3, new DateTime(2023, 11, 1, 21, 22, 32, 495, DateTimeKind.Local).AddTicks(5833), true, false, "sa multiple choice" },
                    { 4, new DateTime(2023, 11, 1, 21, 22, 32, 495, DateTimeKind.Local).AddTicks(5834), true, false, "ma multiple choice" }
                });

            migrationBuilder.InsertData(
                table: "SurveyTypes",
                columns: new[] { "ID", "CreatedAt", "IsActive", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 11, 1, 21, 22, 32, 495, DateTimeKind.Local).AddTicks(5807), true, false, "single page" },
                    { 2, new DateTime(2023, 11, 1, 21, 22, 32, 495, DateTimeKind.Local).AddTicks(5809), true, false, "multi page" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "ID", "CreatedAt", "IsActive", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 11, 1, 21, 22, 32, 495, DateTimeKind.Local).AddTicks(5645), true, false, "end user" },
                    { 2, new DateTime(2023, 11, 1, 21, 22, 32, 495, DateTimeKind.Local).AddTicks(5660), true, false, "admin" },
                    { 3, new DateTime(2023, 11, 1, 21, 22, 32, 495, DateTimeKind.Local).AddTicks(5661), true, false, "root" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "ID", "CreatedAt", "CreatorUserID", "Email", "IsActive", "IsDeleted", "ModifiedAt", "Name", "Password", "RoleID", "Username" },
                values: new object[] { new Guid("62539a22-ba5c-4dd8-95db-67e0926bd1df"), new DateTime(2023, 11, 1, 21, 22, 32, 495, DateTimeKind.Local).AddTicks(5927), new Guid("00000000-0000-0000-0000-000000000000"), "", true, false, new DateTime(2023, 11, 1, 21, 22, 32, 495, DateTimeKind.Local).AddTicks(5927), "root", "root", 3, "root" });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_CreatorUserID",
                table: "Answers",
                column: "CreatorUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionChoiceID",
                table: "Answers",
                column: "QuestionChoiceID");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionID",
                table: "Answers",
                column: "QuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_UserID",
                table: "Answers",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Assigns_CreatorUserID",
                table: "Assigns",
                column: "CreatorUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Assigns_SurveyID",
                table: "Assigns",
                column: "SurveyID");

            migrationBuilder.CreateIndex(
                name: "IX_Assigns_UserID",
                table: "Assigns",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionChoices_CreatorUserID",
                table: "QuestionChoices",
                column: "CreatorUserID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionChoices_QuestionID",
                table: "QuestionChoices",
                column: "QuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_CreatorUserID",
                table: "Questions",
                column: "CreatorUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_SurveyID",
                table: "Questions",
                column: "SurveyID");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_TypeID",
                table: "Questions",
                column: "TypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Submits_CreatorUserID",
                table: "Submits",
                column: "CreatorUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Submits_SurveyID",
                table: "Submits",
                column: "SurveyID");

            migrationBuilder.CreateIndex(
                name: "IX_Submits_UserID",
                table: "Submits",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Surveys_CreatorUserID",
                table: "Surveys",
                column: "CreatorUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Surveys_TypeID",
                table: "Surveys",
                column: "TypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleID",
                table: "Users",
                column: "RoleID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "Assigns");

            migrationBuilder.DropTable(
                name: "Submits");

            migrationBuilder.DropTable(
                name: "QuestionChoices");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "QuestionTypes");

            migrationBuilder.DropTable(
                name: "Surveys");

            migrationBuilder.DropTable(
                name: "SurveyTypes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "UserRoles");
        }
    }
}
