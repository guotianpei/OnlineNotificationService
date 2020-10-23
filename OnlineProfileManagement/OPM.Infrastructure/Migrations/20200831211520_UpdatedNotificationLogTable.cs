using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OPM.Infrastructure.Migrations
{
    public partial class UpdatedNotificationLogTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_NotificationHistory",
                table: "NotificationHistory");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "NotificationHistory");

            migrationBuilder.RenameTable(
                name: "NotificationHistory",
                newName: "NotificationLog");

            migrationBuilder.AddColumn<Guid>(
                name: "EntityID",
                table: "NotificationLog",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "NotificationStage",
                table: "NotificationLog",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TopicID",
                table: "NotificationLog",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotificationLog",
                table: "NotificationLog",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_NotificationLog",
                table: "NotificationLog");

            migrationBuilder.DropColumn(
                name: "EntityID",
                table: "NotificationLog");

            migrationBuilder.DropColumn(
                name: "NotificationStage",
                table: "NotificationLog");

            migrationBuilder.DropColumn(
                name: "TopicID",
                table: "NotificationLog");

            migrationBuilder.RenameTable(
                name: "NotificationLog",
                newName: "NotificationHistory");

            migrationBuilder.AddColumn<Guid>(
                name: "UserID",
                table: "NotificationHistory",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotificationHistory",
                table: "NotificationHistory",
                column: "Id");
        }
    }
}
