using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Duceac_Ioana_Lab2.Migrations
{
    /// <inheritdoc />
    public partial class coloanaBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
    name: "AuthorsID",
    table: "Book",
    type: "int",
    nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
