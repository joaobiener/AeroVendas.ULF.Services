using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AeroVendas.ULF.Services.Migrations
{
    public partial class DatabaseCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "UNIMEDLF",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ac8240a-8498-4869-bc86-60e5dc982d27");

            migrationBuilder.DeleteData(
                schema: "UNIMEDLF",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "562419f5-eed1-473b-bcc1-9f2dbab182b4");

            migrationBuilder.InsertData(
                schema: "UNIMEDLF",
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3abcc917-b23c-4acf-90c1-c664fab13610", "41336648-a0ed-427e-9581-96645b51d82a", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                schema: "UNIMEDLF",
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "692ff651-1820-4f5f-ab0a-28a19edc4b28", "2412c754-5151-4be5-b26d-9346a8ec9b71", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "UNIMEDLF",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3abcc917-b23c-4acf-90c1-c664fab13610");

            migrationBuilder.DeleteData(
                schema: "UNIMEDLF",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "692ff651-1820-4f5f-ab0a-28a19edc4b28");

            migrationBuilder.InsertData(
                schema: "UNIMEDLF",
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4ac8240a-8498-4869-bc86-60e5dc982d27", "3f08bdad-49c5-439f-a181-d7ff8c2aa4d6", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                schema: "UNIMEDLF",
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "562419f5-eed1-473b-bcc1-9f2dbab182b4", "890c84a7-4625-48ec-928e-4381384055b0", "Administrator", "ADMINISTRATOR" });
        }
    }
}
