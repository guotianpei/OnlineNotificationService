using Microsoft.EntityFrameworkCore.Migrations;

namespace OPM.Infrastructure.Migrations
{
    public partial class AddedForeignKeyOnTableProfileComChannel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfileComChannel_EntityProfile_EntityProfileId",
                table: "ProfileComChannel");

            migrationBuilder.DropIndex(
                name: "IX_ProfileComChannel_EntityProfileId",
                table: "ProfileComChannel");

            migrationBuilder.DropColumn(
                name: "EntityProfileId",
                table: "ProfileComChannel");

            migrationBuilder.AddColumn<int>(
                name: "EntityID",
                table: "ProfileComChannel",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProfileComChannel_EntityID",
                table: "ProfileComChannel",
                column: "EntityID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileComChannel_EntityProfile_EntityID",
                table: "ProfileComChannel",
                column: "EntityID",
                principalTable: "EntityProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfileComChannel_EntityProfile_EntityID",
                table: "ProfileComChannel");

            migrationBuilder.DropIndex(
                name: "IX_ProfileComChannel_EntityID",
                table: "ProfileComChannel");

            migrationBuilder.DropColumn(
                name: "EntityID",
                table: "ProfileComChannel");

            migrationBuilder.AddColumn<int>(
                name: "EntityProfileId",
                table: "ProfileComChannel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProfileComChannel_EntityProfileId",
                table: "ProfileComChannel",
                column: "EntityProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileComChannel_EntityProfile_EntityProfileId",
                table: "ProfileComChannel",
                column: "EntityProfileId",
                principalTable: "EntityProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
