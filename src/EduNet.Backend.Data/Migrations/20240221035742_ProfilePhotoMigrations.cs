using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduNet.Backend.Data.Migrations
{
    /// <inheritdoc />
    public partial class ProfilePhotoMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Branches",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Branches");
        }
    }
}
