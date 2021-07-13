using Microsoft.EntityFrameworkCore.Migrations;

namespace SportsXRepository.Migrations
{
    public partial class SportsXDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: false),
                    RazaoSocial = table.Column<string>(type: "varchar(100)", nullable: true),
                    Cpf = table.Column<string>(type: "varchar(11)", nullable: true),
                    Cnpj = table.Column<string>(type: "varchar(14)", nullable: true),
                    Cep = table.Column<string>(type: "varchar(8)", nullable: true),
                    Classificacao = table.Column<int>(nullable: false),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClienteTelefones",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numero = table.Column<string>(type: "varchar(14)", nullable: false),
                    ClienteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClienteTelefones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClienteTelefones_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClienteTelefones_ClienteId",
                table: "ClienteTelefones",
                column: "ClienteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClienteTelefones");

            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
