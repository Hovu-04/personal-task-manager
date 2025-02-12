using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagerBackEnd.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabaseSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "priority",
                table: "tasks",
                type: "task_priority",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "priority",
                table: "tasks",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "task_priority");
        }
    }
}
