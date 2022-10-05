using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelListing.API.Migrations
{
    public partial class UpdateDbContextRegion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Region_Countries_CountryId",
                table: "Region");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Region",
                table: "Region");

            migrationBuilder.RenameTable(
                name: "Region",
                newName: "Regions");

            migrationBuilder.RenameIndex(
                name: "IX_Region_CountryId",
                table: "Regions",
                newName: "IX_Regions_CountryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Regions",
                table: "Regions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Regions_Countries_CountryId",
                table: "Regions",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Regions_Countries_CountryId",
                table: "Regions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Regions",
                table: "Regions");

            migrationBuilder.RenameTable(
                name: "Regions",
                newName: "Region");

            migrationBuilder.RenameIndex(
                name: "IX_Regions_CountryId",
                table: "Region",
                newName: "IX_Region_CountryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Region",
                table: "Region",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Region_Countries_CountryId",
                table: "Region",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
