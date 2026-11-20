using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectChaptereight.BlogWeb.DataBaseLayer.Migrations
{
    /// <inheritdoc />
    public partial class fixpost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubCategoryID",
                table: "posts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_posts_SubCategoryID",
                table: "posts",
                column: "SubCategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_posts_categories_SubCategoryID",
                table: "posts",
                column: "SubCategoryID",
                principalTable: "categories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_posts_categories_SubCategoryID",
                table: "posts");

            migrationBuilder.DropIndex(
                name: "IX_posts_SubCategoryID",
                table: "posts");

            migrationBuilder.DropColumn(
                name: "SubCategoryID",
                table: "posts");
        }
    }
}
