using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectChaptereight.BlogWeb.DataBaseLayer.Migrations
{
    /// <inheritdoc />
    public partial class SpecialPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSpecial",
                table: "posts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSpecial",
                table: "posts");
        }
    }
}
