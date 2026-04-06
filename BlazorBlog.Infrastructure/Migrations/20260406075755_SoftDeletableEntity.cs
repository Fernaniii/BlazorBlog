using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorBlog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SoftDeletableEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Articles",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Articles");
        }
    }
}
