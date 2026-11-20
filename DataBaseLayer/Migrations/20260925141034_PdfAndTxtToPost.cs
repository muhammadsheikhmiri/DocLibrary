using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectChaptereight.BlogWeb.DataBaseLayer.Migrations
{
    /// <inheritdoc />
    public partial class PdfAndTxtToPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageName",
                table: "posts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "PdfName",
                table: "posts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TxtName",
                table: "posts",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PdfName",
                table: "posts");

            migrationBuilder.DropColumn(
                name: "TxtName",
                table: "posts");

            migrationBuilder.AlterColumn<string>(
                name: "ImageName",
                table: "posts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TxtFileName",
                table: "posts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
