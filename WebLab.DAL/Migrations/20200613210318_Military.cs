using Microsoft.EntityFrameworkCore.Migrations;

namespace WebLab.DAL.Migrations
{
    public partial class Military : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MilitaryGroups",
                columns: table => new
                {
                    MilitaryGroupId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MilitaryGroups", x => x.MilitaryGroupId);
                });

            migrationBuilder.CreateTable(
                name: "Militaries",
                columns: table => new
                {
                    MilitaryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MilitaryName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Force = table.Column<int>(nullable: false),
                    Image = table.Column<string>(nullable: true),
                    MilitaryGroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Militaries", x => x.MilitaryId);
                    table.ForeignKey(
                        name: "FK_Militaries_MilitaryGroups_MilitaryGroupId",
                        column: x => x.MilitaryGroupId,
                        principalTable: "MilitaryGroups",
                        principalColumn: "MilitaryGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Militaries_MilitaryGroupId",
                table: "Militaries",
                column: "MilitaryGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Militaries");

            migrationBuilder.DropTable(
                name: "MilitaryGroups");
        }
    }
}
