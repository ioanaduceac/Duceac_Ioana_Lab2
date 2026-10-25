using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Duceac_Ioana_Lab2.Migrations
{
    /// <inheritdoc />
    public partial class RemoveOldAutorColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
    name: "Author",
    table: "Book");
            migrationBuilder.CreateIndex(
    name: "IX_Book_AuthorsID",
    table: "Book",
    column: "AuthorsID");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Authors_AuthorsID",
                table: "Book",
                column: "AuthorsID",
                principalTable: "Authors",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
    name: "Author",
    table: "Book",
    type: "nvarchar(max)",
    nullable: false,
    defaultValue: "");
        }
    }
}
