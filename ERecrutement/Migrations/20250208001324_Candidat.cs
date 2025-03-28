using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERecrutement.Migrations
{
    /// <inheritdoc />
    public partial class Candidat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CV",
                table: "Candidatures",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LettreMotivation",
                table: "Candidatures",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CV",
                table: "Candidatures");

            migrationBuilder.DropColumn(
                name: "LettreMotivation",
                table: "Candidatures");
        }
    }
}
