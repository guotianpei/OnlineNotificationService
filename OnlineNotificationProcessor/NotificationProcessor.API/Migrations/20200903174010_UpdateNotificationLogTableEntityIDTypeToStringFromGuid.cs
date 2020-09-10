using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NotificationProcessor.API.Migrations
{
    public partial class UpdateNotificationLogTableEntityIDTypeToStringFromGuid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "EntityID",
                table: "NotificationLog",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "EntityID",
                table: "NotificationLog",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
