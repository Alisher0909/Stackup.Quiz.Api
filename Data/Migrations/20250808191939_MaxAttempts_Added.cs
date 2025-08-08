using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stackup.Quiz.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class MaxAttempts_Added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "max_attempts",
                table: "quizes",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "max_attempts",
                table: "quizes");
        }
    }
}
