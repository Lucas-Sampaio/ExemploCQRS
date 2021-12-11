using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class cpfUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Pessoas_Cpf_Numero",
                table: "Pessoas",
                column: "Cpf_Numero",
                unique: true,
                filter: "[Cpf_Numero] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Pessoas_Cpf_Numero",
                table: "Pessoas");
        }
    }
}
