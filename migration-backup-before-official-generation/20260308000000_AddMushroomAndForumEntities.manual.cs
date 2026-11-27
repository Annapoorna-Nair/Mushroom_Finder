using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mushroom_Finder.Data.Migrations;

public partial class AddMushroomAndForumEntities : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Mushrooms",
            columns: table => new
            {
                MushroomId = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                Family = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                ToxicityClass = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                MinCapDiameter = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                MaxCapDiameter = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                CapShape = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                CapSurface = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                CapColor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                GillAttachment = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                MinStemHeight = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                MaxStemHeight = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                StemRoot = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                StemColor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                Habitat = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                Season = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                Region = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                ImagePath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                ImageAltText = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                ScientificName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                SourceReference = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Mushrooms", x => x.MushroomId);
            });

        migrationBuilder.CreateTable(
            name: "ForumPosts",
            columns: table => new
            {
                ForumPostId = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                Title = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                AuthorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                CreatedUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                PhotoUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                PhotoAltText = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                PhotoSourceAndRights = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                ShowSafetyDisclaimer = table.Column<bool>(type: "bit", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ForumPosts", x => x.ForumPostId);
                table.ForeignKey(
                    name: "FK_ForumPosts_AspNetUsers_AuthorId",
                    column: x => x.AuthorId,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_ForumPosts_AuthorId",
            table: "ForumPosts",
            column: "AuthorId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "ForumPosts");
        migrationBuilder.DropTable(name: "Mushrooms");
    }
}
