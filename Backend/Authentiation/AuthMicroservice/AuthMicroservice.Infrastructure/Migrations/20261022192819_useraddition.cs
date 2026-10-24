using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthMicroservice.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class useraddition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "LastTwoFactorTimeStep",
                table: "Users",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastTwoFactorTimeStep",
                table: "Users");
        }
    }
}
