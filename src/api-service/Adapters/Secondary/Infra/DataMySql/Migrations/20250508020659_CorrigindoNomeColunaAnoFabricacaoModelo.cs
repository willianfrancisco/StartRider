using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataMySql.Migrations
{
    /// <inheritdoc />
    public partial class CorrigindoNomeColunaAnoFabricacaoModelo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AnoFrabricacaoModelo",
                table: "Veiculos",
                newName: "AnoFabricacaoModelo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AnoFabricacaoModelo",
                table: "Veiculos",
                newName: "AnoFrabricacaoModelo");
        }
    }
}
