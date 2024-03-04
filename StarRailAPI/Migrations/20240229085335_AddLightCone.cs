using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace starrailAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddLightCone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LightCones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DestinyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LightCones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LightCones_Destinies_DestinyId",
                        column: x => x.DestinyId,
                        principalTable: "Destinies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LightCones_DestinyId",
                table: "LightCones",
                column: "DestinyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LightCones");
        }
    }
}
