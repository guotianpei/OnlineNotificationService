using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OPM.Infrastructure.Migrations
{
    public partial class InitialModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DistributionGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupName = table.Column<string>(nullable: false),
                    GroupDescription = table.Column<string>(nullable: false),
                    GroupMapping = table.Column<string>(nullable: false),
                    EffDate = table.Column<DateTime>(nullable: false),
                    TermDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistributionGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotificationHistories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrackingID = table.Column<Guid>(nullable: false),
                    UserID = table.Column<Guid>(nullable: false),
                    ComChannel = table.Column<string>(nullable: false),
                    Recipient = table.Column<string>(nullable: false),
                    MessageBody = table.Column<string>(nullable: false),
                    NotificationDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationHistories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProfileResources",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResourceName = table.Column<string>(nullable: false),
                    EffDate = table.Column<DateTime>(nullable: false),
                    TermDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileResources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProfileDistributionGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupID = table.Column<int>(nullable: false),
                    UserID = table.Column<Guid>(nullable: false),
                    EffDate = table.Column<DateTime>(nullable: false),
                    TermDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileDistributionGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfileDistributionGroups_DistributionGroups_GroupID",
                        column: x => x.GroupID,
                        principalTable: "DistributionGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EntityProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityID = table.Column<string>(nullable: false),
                    EntityName = table.Column<string>(nullable: true),
                    EntityType = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    EffDate = table.Column<DateTime>(nullable: false),
                    TermDate = table.Column<DateTime>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    ResourceID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntityProfiles_ProfileResources_ResourceID",
                        column: x => x.ResourceID,
                        principalTable: "ProfileResources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProfileComChannels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<Guid>(nullable: false),
                    ComChannel = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: false),
                    Enabled = table.Column<bool>(nullable: false),
                    Preference = table.Column<int>(nullable: false),
                    TermDate = table.Column<DateTime>(nullable: true),
                    EntityProfileId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileComChannels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfileComChannels_EntityProfiles_EntityProfileId",
                        column: x => x.EntityProfileId,
                        principalTable: "EntityProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EntityProfiles_ResourceID",
                table: "EntityProfiles",
                column: "ResourceID");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileComChannels_EntityProfileId",
                table: "ProfileComChannels",
                column: "EntityProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileDistributionGroups_GroupID",
                table: "ProfileDistributionGroups",
                column: "GroupID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotificationHistories");

            migrationBuilder.DropTable(
                name: "ProfileComChannels");

            migrationBuilder.DropTable(
                name: "ProfileDistributionGroups");

            migrationBuilder.DropTable(
                name: "EntityProfiles");

            migrationBuilder.DropTable(
                name: "DistributionGroups");

            migrationBuilder.DropTable(
                name: "ProfileResources");
        }
    }
}
