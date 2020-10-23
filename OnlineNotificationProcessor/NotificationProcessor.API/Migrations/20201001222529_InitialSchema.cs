using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NotificationProcessor.API.Migrations
{
    public partial class InitialSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EntityProfile",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityID = table.Column<string>(nullable: false),
                    EntityName = table.Column<string>(nullable: true),
                    EntityType = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    SMS = table.Column<string>(nullable: true),
                    SecureMassage = table.Column<string>(nullable: true),
                    LastUpdateDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityProfile", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FailureNotifyCodes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FailureCode = table.Column<string>(nullable: true),
                    NotificationReception = table.Column<string>(nullable: true),
                    TopicID = table.Column<int>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    LastUpdateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FailureNotifyCodes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "IntegrationEventLog",
                columns: table => new
                {
                    EventId = table.Column<Guid>(nullable: false),
                    EventTypeName = table.Column<string>(nullable: false),
                    State = table.Column<string>(nullable: false),
                    TimesSent = table.Column<int>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    Content = table.Column<string>(nullable: false),
                    TransactionId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntegrationEventLog", x => x.EventId);
                });

            migrationBuilder.CreateTable(
                name: "NotificationRequest",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityID = table.Column<string>(nullable: false),
                    ComChannel = table.Column<string>(nullable: false),
                    RequestMessageData = table.Column<string>(nullable: true),
                    TopicID = table.Column<int>(nullable: false),
                    NotificationStage = table.Column<string>(nullable: true),
                    RequestDatetime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationRequest", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NotificationTemplate",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TopicName = table.Column<string>(nullable: false),
                    ComChannel = table.Column<string>(nullable: false),
                    From = table.Column<string>(nullable: false),
                    Subject = table.Column<string>(nullable: true),
                    TemplateFile = table.Column<string>(nullable: false),
                    EffectiveDate = table.Column<DateTime>(nullable: false),
                    TerminateDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    UpdateBy = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationTemplate", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NotificationTransactionLog",
                columns: table => new
                {
                    TrackingID = table.Column<Guid>(nullable: false),
                    EntityID = table.Column<string>(nullable: false),
                    ComChannel = table.Column<string>(nullable: false),
                    Recipient = table.Column<string>(nullable: false),
                    TopicID = table.Column<int>(nullable: false),
                    MessageBody = table.Column<string>(nullable: false),
                    TransactionDateTime = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    RetryCounts = table.Column<int>(nullable: false),
                    NotificationStage = table.Column<string>(nullable: true),
                    ResponseCode = table.Column<int>(nullable: false),
                    ResponseDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationTransactionLog", x => x.TrackingID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntityProfile");

            migrationBuilder.DropTable(
                name: "FailureNotifyCodes");

            migrationBuilder.DropTable(
                name: "IntegrationEventLog");

            migrationBuilder.DropTable(
                name: "NotificationRequest");

            migrationBuilder.DropTable(
                name: "NotificationTemplate");

            migrationBuilder.DropTable(
                name: "NotificationTransactionLog");
        }
    }
}
