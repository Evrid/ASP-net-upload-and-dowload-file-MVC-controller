using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FileUploadDownloadTest.Migrations
{
    /// <inheritdoc />
    public partial class InitialDatabaseSetup3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OriginalFileName",
                table: "FileTable",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OriginalFileName",
                table: "FileTable");
        }
    }
}
