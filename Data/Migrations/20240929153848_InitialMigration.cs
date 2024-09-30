using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Venda",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    NumeroVenda = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    DataVenda = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ClienteId = table.Column<Guid>(type: "TEXT", nullable: false),
                    NomeCliente = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    ValorTotal = table.Column<decimal>(type: "TEXT", nullable: false),
                    FilialId = table.Column<Guid>(type: "TEXT", nullable: false),
                    NomeFilial = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Cancelada = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venda", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemVenda",
                columns: table => new
                {
                    ProdutoId = table.Column<Guid>(type: "TEXT", nullable: false),
                    NomeProduto = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    ValorUnitario = table.Column<decimal>(type: "TEXT", nullable: false),
                    Quantidade = table.Column<int>(type: "INTEGER", nullable: false),
                    Desconto = table.Column<decimal>(type: "TEXT", nullable: false),
                    ValorTotalItem = table.Column<decimal>(type: "TEXT", nullable: false),
                    VendaId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemVenda", x => x.ProdutoId);
                    table.ForeignKey(
                        name: "FK_ItemVenda_Venda_VendaId",
                        column: x => x.VendaId,
                        principalTable: "Venda",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemVenda_VendaId",
                table: "ItemVenda",
                column: "VendaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemVenda");

            migrationBuilder.DropTable(
                name: "Venda");
        }
    }
}
