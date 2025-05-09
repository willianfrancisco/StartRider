using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataMySql.Migrations
{
    /// <inheritdoc />
    public partial class MigracaoLocacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Locacoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DataRetirada = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DataDevolucao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Plano = table.Column<int>(type: "int", nullable: false),
                    VeiculoId = table.Column<int>(type: "int", nullable: false),
                    LocatarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Locacoes_Locatarios_LocatarioId",
                        column: x => x.LocatarioId,
                        principalTable: "Locatarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Locacoes_Veiculos_VeiculoId",
                        column: x => x.VeiculoId,
                        principalTable: "Veiculos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Locacoes_LocatarioId",
                table: "Locacoes",
                column: "LocatarioId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Locacoes_VeiculoId",
                table: "Locacoes",
                column: "VeiculoId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Locacoes");
        }
    }
}
