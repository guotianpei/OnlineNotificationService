using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OPM.Infrastructure.Migrations
{
    public partial class InitialSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DistributionGroup",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupName = table.Column<string>(nullable: false),
                    GroupDescription = table.Column<string>(nullable: false),
                    GroupMapping = table.Column<string>(nullable: false),
                    EffDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    TermDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistributionGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotificationHistory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrackingID = table.Column<Guid>(nullable: false),
                    UserID = table.Column<Guid>(nullable: false),
                    ComChannel = table.Column<string>(nullable: false),
                    Recipient = table.Column<string>(nullable: false),
                    MessageBody = table.Column<string>(nullable: false),
                    NotificationDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProfileResource",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResourceName = table.Column<string>(nullable: false),
                    EffDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    TermDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileResource", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProfileDistributionGroup",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupID = table.Column<int>(nullable: false),
                    UserID = table.Column<Guid>(nullable: false),
                    EffDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    TermDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileDistributionGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfileDistributionGroup_DistributionGroup_GroupID",
                        column: x => x.GroupID,
                        principalTable: "DistributionGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EntityProfile",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityID = table.Column<string>(nullable: false),
                    EntityName = table.Column<string>(nullable: true),
                    EntityType = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    EffDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    TermDate = table.Column<DateTime>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    ResourceID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityProfile", x => x.Id);
                    table.UniqueConstraint("AK_EntityProfile_EntityID", x => x.EntityID);
                    table.ForeignKey(
                        name: "FK_EntityProfile_ProfileResource_ResourceID",
                        column: x => x.ResourceID,
                        principalTable: "ProfileResource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProfileComChannel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityID = table.Column<string>(nullable: false),
                    ComChannel = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: false),
                    Enabled = table.Column<bool>(nullable: false),
                    Preference = table.Column<int>(nullable: false),
                    TermDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileComChannel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfileComChannel_EntityProfile_EntityID",
                        column: x => x.EntityID,
                        principalTable: "EntityProfile",
                        principalColumn: "EntityID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EntityProfile_ResourceID",
                table: "EntityProfile",
                column: "ResourceID");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileComChannel_EntityID",
                table: "ProfileComChannel",
                column: "EntityID");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileDistributionGroup_GroupID",
                table: "ProfileDistributionGroup",
                column: "GroupID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotificationHistory");

            migrationBuilder.DropTable(
                name: "ProfileComChannel");

            migrationBuilder.DropTable(
                name: "ProfileDistributionGroup");

            migrationBuilder.DropTable(
                name: "EntityProfile");

            migrationBuilder.DropTable(
                name: "DistributionGroup");

            migrationBuilder.DropTable(
                name: "ProfileResource");
        }
    }
}
