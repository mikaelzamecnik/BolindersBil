using Microsoft.EntityFrameworkCore.Migrations;

namespace BolindersBil.Web.Migrations
{
    public partial class addedAttributes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Attributes",
                table: "Vehicles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attributes",
                table: "Vehicles");
        }
    }
}
