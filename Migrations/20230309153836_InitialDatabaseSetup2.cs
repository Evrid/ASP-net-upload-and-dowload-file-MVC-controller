using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FileUploadDownloadTest.Migrations
{
    /// <inheritdoc />
    public partial class InitialDatabaseSetup2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryTable",
                table: "CategoryTable");

            migrationBuilder.RenameTable(
                name: "CategoryTable",
                newName: "FileTable");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FileTable",
                table: "FileTable",
                column: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FileTable",
                table: "FileTable");

            migrationBuilder.RenameTable(
                name: "FileTable",
                newName: "CategoryTable");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryTable",
                table: "CategoryTable",
                column: "ID");
        }
    }
}
