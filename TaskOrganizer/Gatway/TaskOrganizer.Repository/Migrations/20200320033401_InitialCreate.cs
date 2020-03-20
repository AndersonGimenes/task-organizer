using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace TaskOrganizer.Repository.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProgressTypes",
                columns: table => new
                {
                    ProgressId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Fk_ProgressType", x => x.ProgressId);
                });

            migrationBuilder.CreateTable(
                name: "RepositoryTasks",
                columns: table => new
                {
                    TaskId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "varchar(40)", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ProgressId = table.Column<int>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    EstimetedDate = table.Column<DateTime>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Fk_Task", x => x.TaskId);
                    table.ForeignKey(
                        name: "FK_RepositoryTasks_ProgressTypes_ProgressId",
                        column: x => x.ProgressId,
                        principalTable: "ProgressTypes",
                        principalColumn: "ProgressId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RepositoryTasks_ProgressId",
                table: "RepositoryTasks",
                column: "ProgressId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RepositoryTasks");

            migrationBuilder.DropTable(
                name: "ProgressTypes");
        }
    }
}
