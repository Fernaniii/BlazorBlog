using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorBlog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDateCreatedAndDateUpdatedInArticle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Articles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateUpdated",
                table: "Articles",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "DateUpdated",
                table: "Articles");
        }
    }
}
