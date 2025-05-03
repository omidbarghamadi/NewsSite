using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsSite.Migrations
{
    /// <inheritdoc />
    public partial class imagefield : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "NewsItem",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "NewsItem");
        }
    }
}
