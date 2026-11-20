using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectChaptereight.BlogWeb.DataBaseLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TxtFileName",
                table: "posts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TxtFileName",
                table: "posts");
        }
    }
}
