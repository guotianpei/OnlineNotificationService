using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NotificationProcessor.API.Migrations
{
    public partial class InitialSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NotificationLog",
                columns: table => new
                {
                    TrackingID = table.Column<Guid>(nullable: false),
                    EntityID = table.Column<Guid>(nullable: false),
                    ComChannel = table.Column<string>(nullable: false),
                    Recipient = table.Column<string>(nullable: false),
                    TopicID = table.Column<int>(nullable: false),
                    MessageBody = table.Column<string>(nullable: false),
                    NotificationDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    NotificationStage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationLog", x => x.TrackingID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotificationLog");
        }
    }
}
