using Microsoft.EntityFrameworkCore.Migrations;

namespace cmsdasp.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GetPasses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Phonenumber = table.Column<int>(nullable: false),
                    Destination = table.Column<string>(nullable: true),
                    Purpose = table.Column<string>(nullable: true),
                    Vcardno = table.Column<string>(nullable: true),
                    Cardtype = table.Column<string>(nullable: true),
                    Createdby = table.Column<string>(nullable: true),
                    Cardissuer = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GetPasses", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GetPasses");
        }
    }
}
