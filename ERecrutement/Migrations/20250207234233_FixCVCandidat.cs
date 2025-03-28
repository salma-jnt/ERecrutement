using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERecrutement.Migrations
{
    /// <inheritdoc />
    public partial class FixCVCandidat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Recruteurs_UserId",
                table: "Recruteurs");

            migrationBuilder.AlterColumn<string>(
                name: "CV",
                table: "Candidats",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Recruteurs_UserId",
                table: "Recruteurs",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Recruteurs_UserId",
                table: "Recruteurs");

            migrationBuilder.AlterColumn<string>(
                name: "CV",
                table: "Candidats",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recruteurs_UserId",
                table: "Recruteurs",
                column: "UserId");
        }
    }
}
