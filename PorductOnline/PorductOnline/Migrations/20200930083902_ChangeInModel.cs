using Microsoft.EntityFrameworkCore.Migrations;

namespace PorductOnline.Migrations
{
    public partial class ChangeInModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "imageUri",
                table: "productDb",
                newName: "file");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "file",
                table: "productDb",
                newName: "imageUri");
        }
    }
}
