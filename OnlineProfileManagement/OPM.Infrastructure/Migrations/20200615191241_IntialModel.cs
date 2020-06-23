using Microsoft.EntityFrameworkCore.Migrations;

namespace OPM.Infrastructure.Migrations
{
    public partial class IntialModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ComChannelStatus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComChannelStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ComChannelTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComChannelTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EntityProfile",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        //.Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityProfile", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProfileComChannels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(nullable: true),
                    StatusId = table.Column<int>(nullable: true),
                    ChannelTypeId = table.Column<int>(nullable: true),
                    EntityProfileId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileComChannels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfileComChannels_ComChannelTypes_ChannelTypeId",
                        column: x => x.ChannelTypeId,
                        principalTable: "ComChannelTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProfileComChannels_EntityProfile_EntityProfileId",
                        column: x => x.EntityProfileId,
                        principalTable: "EntityProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProfileComChannels_ComChannelStatus_StatusId",
                        column: x => x.StatusId,
                        principalTable: "ComChannelStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProfileComChannels_ChannelTypeId",
                table: "ProfileComChannels",
                column: "ChannelTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileComChannels_EntityProfileId",
                table: "ProfileComChannels",
                column: "EntityProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileComChannels_StatusId",
                table: "ProfileComChannels",
                column: "StatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProfileComChannels");

            migrationBuilder.DropTable(
                name: "ComChannelTypes");

            migrationBuilder.DropTable(
                name: "EntityProfile");

            migrationBuilder.DropTable(
                name: "ComChannelStatus");
        }
    }
}
