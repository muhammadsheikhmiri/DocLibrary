using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectChaptereight.BlogWeb.DataBaseLayer.Migrations
{
    /// <inheritdoc />
    public partial class fixpostTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WordName",
                table: "posts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ZipName",
                table: "posts",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WordName",
                table: "posts");

            migrationBuilder.DropColumn(
                name: "ZipName",
                table: "posts");
        }
    }
}
