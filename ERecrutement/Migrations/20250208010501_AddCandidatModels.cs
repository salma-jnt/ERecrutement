using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERecrutement.Migrations
{
    /// <inheritdoc />
    public partial class AddCandidatModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CV",
                table: "Candidatures");

            migrationBuilder.DropColumn(
                name: "LettreMotivation",
                table: "Candidatures");

            migrationBuilder.AlterColumn<string>(
                name: "CV",
                table: "Candidats",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Candidats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Diplome",
                table: "Candidats",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "NombreAnneeExperience",
                table: "Candidats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Prenom",
                table: "Candidats",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Titre",
                table: "Candidats",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "Candidats");

            migrationBuilder.DropColumn(
                name: "Diplome",
                table: "Candidats");

            migrationBuilder.DropColumn(
                name: "NombreAnneeExperience",
                table: "Candidats");

            migrationBuilder.DropColumn(
                name: "Prenom",
                table: "Candidats");

            migrationBuilder.DropColumn(
                name: "Titre",
                table: "Candidats");

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

            migrationBuilder.AlterColumn<string>(
                name: "CV",
                table: "Candidats",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
