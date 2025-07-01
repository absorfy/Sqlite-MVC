using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyMVCApp.Migrations
{
    /// <inheritdoc />
    public partial class RemovingColumnInHeroesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Heroes_Classes_ClassEntityId",
                table: "Heroes");

            migrationBuilder.DropIndex(
                name: "IX_Heroes_ClassEntityId",
                table: "Heroes");

            migrationBuilder.DropColumn(
                name: "ClassEntityId",
                table: "Heroes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClassEntityId",
                table: "Heroes",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Heroes_ClassEntityId",
                table: "Heroes",
                column: "ClassEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Heroes_Classes_ClassEntityId",
                table: "Heroes",
                column: "ClassEntityId",
                principalTable: "Classes",
                principalColumn: "Id");
        }
    }
}
