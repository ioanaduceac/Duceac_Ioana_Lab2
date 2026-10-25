using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Duceac_Ioana_Lab2.Migrations
{
    /// <inheritdoc />
    public partial class Autori : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(
                        type: "nvarchar(max)",
                        nullable: false),
                    LastName = table.Column<string>(
                        type: "nvarchar(max)",
                        nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
