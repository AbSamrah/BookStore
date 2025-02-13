using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.API.Migrations
{
    public partial class NewMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Purchases_PurchaseId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_PurchaseId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "PurchaseId",
                table: "Books");

            migrationBuilder.CreateTable(
                name: "SinglePurchases",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PurchaseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SinglePurchases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SinglePurchases_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SinglePurchases_Purchases_PurchaseId",
                        column: x => x.PurchaseId,
                        principalTable: "Purchases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SinglePurchases_BookId",
                table: "SinglePurchases",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_SinglePurchases_PurchaseId",
                table: "SinglePurchases",
                column: "PurchaseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SinglePurchases");

            migrationBuilder.AddColumn<Guid>(
                name: "PurchaseId",
                table: "Books",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_PurchaseId",
                table: "Books",
                column: "PurchaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Purchases_PurchaseId",
                table: "Books",
                column: "PurchaseId",
                principalTable: "Purchases",
                principalColumn: "Id");
        }
    }
}
