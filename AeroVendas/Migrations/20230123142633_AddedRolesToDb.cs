using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AeroVendas.ULF.Services.Migrations
{
    public partial class AddedRolesToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "UNIMEDLF");

            migrationBuilder.CreateTable(
                name: "VW_CONTRATO_SEM_AERO",
                schema: "UNIMEDLF",
                columns: table => new
                {
                    CODIGO_CONTRATO = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    CODIGO_BENEFICIARIO = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    NOME_BENEFICIARIO = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    EMAIL_BENEFICIARIO = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    PREMIO_ATUAL = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    CIDADE = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    NUMERO_DEPENDENTES = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8d4b8fca-84d0-4594-9d7c-f97ada2f8853", "47431582-c739-4636-948f-e7bac0c2df9a", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8fba809a-a485-4608-9914-080776f8a597", "ba1a6cae-c119-4acc-9350-c49dc397f159", "Manager", "MANAGER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VW_CONTRATO_SEM_AERO",
                schema: "UNIMEDLF");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8d4b8fca-84d0-4594-9d7c-f97ada2f8853");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8fba809a-a485-4608-9914-080776f8a597");
        }
    }
}
