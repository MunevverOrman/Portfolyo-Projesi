using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portfolio.Web.Migrations
{
    /// <inheritdoc />
    public partial class UserMessageUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MessageBody",
                table: "UserMessages",
                newName: "MessageDetail");

            migrationBuilder.AddColumn<DateTime>(
                name: "SendDate",
                table: "UserMessages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SendDate",
                table: "UserMessages");

            migrationBuilder.RenameColumn(
                name: "MessageDetail",
                table: "UserMessages",
                newName: "MessageBody");
        }
    }
}
