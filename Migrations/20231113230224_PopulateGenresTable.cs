using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vidly.Migrations
{
    /// <inheritdoc />
    public partial class PopulateGenresTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Genre (Name) VALUES ('Comedy')");
            migrationBuilder.Sql("INSERT INTO Genre (Name) VALUES ('Action')");
            migrationBuilder.Sql("INSERT INTO Genre (Name) VALUES ('Family')");
            migrationBuilder.Sql("INSERT INTO Genre (Name) VALUES ('Romance')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
