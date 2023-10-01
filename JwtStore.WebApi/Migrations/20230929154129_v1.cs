using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JwtStore.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR(120)", maxLength: 120, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email_Verfication_Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email_Verification_Expires_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Email_Verification_Verified_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Password_Hash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password_Reset_Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
