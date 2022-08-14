using Microsoft.EntityFrameworkCore.Migrations;

namespace webMobile.Migrations
{
    public partial class addcart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CartRecordId",
                table: "tblProducts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalAmount = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblProducts_CartRecordId",
                table: "tblProducts",
                column: "CartRecordId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblProducts_Carts_CartRecordId",
                table: "tblProducts",
                column: "CartRecordId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblProducts_Carts_CartRecordId",
                table: "tblProducts");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_tblProducts_CartRecordId",
                table: "tblProducts");

            migrationBuilder.DropColumn(
                name: "CartRecordId",
                table: "tblProducts");
        }
    }
}
