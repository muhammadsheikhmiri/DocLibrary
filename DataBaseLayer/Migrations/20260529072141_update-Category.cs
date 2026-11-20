using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectChaptereight.BlogWeb.DataBaseLayer.Migrations
{
    /// <inheritdoc />
    public partial class updateCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentID",
                table: "categories",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentID",
                table: "categories");
        }
    }
}
