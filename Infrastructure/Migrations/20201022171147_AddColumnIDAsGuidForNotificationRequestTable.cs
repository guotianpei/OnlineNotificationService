using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ONP.Infrastructure.Migrations
{
    public partial class AddColumnIDAsGuidForNotificationRequestTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ID",
                table: "NotificationRequest",
                nullable: false,
                defaultValueSql: "newid()");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotificationRequest",
                table: "NotificationRequest",
                column: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_NotificationRequest",
                table: "NotificationRequest");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "NotificationRequest");
        }
    }
}
