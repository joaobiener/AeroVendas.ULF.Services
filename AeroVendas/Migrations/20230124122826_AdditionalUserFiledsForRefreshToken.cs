using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AeroVendas.ULF.Services.Migrations
{
    public partial class AdditionalUserFiledsForRefreshToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8d4b8fca-84d0-4594-9d7c-f97ada2f8853");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8fba809a-a485-4608-9914-080776f8a597");

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                newName: "AspNetUserTokens",
                newSchema: "UNIMEDLF");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                newName: "AspNetUsers",
                newSchema: "UNIMEDLF");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                newName: "AspNetUserRoles",
                newSchema: "UNIMEDLF");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                newName: "AspNetUserLogins",
                newSchema: "UNIMEDLF");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                newName: "AspNetUserClaims",
                newSchema: "UNIMEDLF");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                newName: "AspNetRoles",
                newSchema: "UNIMEDLF");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                newName: "AspNetRoleClaims",
                newSchema: "UNIMEDLF");

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                schema: "UNIMEDLF",
                table: "AspNetUsers",
                type: "NVARCHAR2(2000)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                schema: "UNIMEDLF",
                table: "AspNetUsers",
                type: "TIMESTAMP(7)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                schema: "UNIMEDLF",
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "545e1c7f-7da6-4415-9f4e-ef1ed4bea00a", "3f08bdad-49c5-439f-a181-d7ff8c2aa4d6", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                schema: "UNIMEDLF",
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d1ab536b-a2d2-4b30-9169-8303b4c5d150", "890c84a7-4625-48ec-928e-4381384055b0", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "UNIMEDLF",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "545e1c7f-7da6-4415-9f4e-ef1ed4bea00a");

            migrationBuilder.DeleteData(
                schema: "UNIMEDLF",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d1ab536b-a2d2-4b30-9169-8303b4c5d150");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                schema: "UNIMEDLF",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                schema: "UNIMEDLF",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                schema: "UNIMEDLF",
                newName: "AspNetUserTokens");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                schema: "UNIMEDLF",
                newName: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                schema: "UNIMEDLF",
                newName: "AspNetUserRoles");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                schema: "UNIMEDLF",
                newName: "AspNetUserLogins");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                schema: "UNIMEDLF",
                newName: "AspNetUserClaims");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                schema: "UNIMEDLF",
                newName: "AspNetRoles");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                schema: "UNIMEDLF",
                newName: "AspNetRoleClaims");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8d4b8fca-84d0-4594-9d7c-f97ada2f8853", "47431582-c739-4636-948f-e7bac0c2df9a", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8fba809a-a485-4608-9914-080776f8a597", "ba1a6cae-c119-4acc-9350-c49dc397f159", "Manager", "MANAGER" });
        }
    }
}
